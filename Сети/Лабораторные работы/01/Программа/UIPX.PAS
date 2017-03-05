UNIT UIPX;

{-----------------------------------------------------------------}
							  INTERFACE
{-----------------------------------------------------------------}

	type
		TFullAddress = record
			Network		: array [0..3] of byte;
			Node		: array [0..5] of byte;
			Socket		: word;
		end;

		PFullAddress = ^TFullAddress;

		TIPXHeader = record
			Checksum			: word;
			Length				: word;
			TransportControl	: byte;
			PacketType			: byte;
			Destination			: TFullAddress;
			Source				: TFullAddress;
		end;

		PIPXHeader = ^TIPXHeader;

		TECB = record
			Link			: pointer;
			ESRAddress 		: pointer;
			InUse			: byte;
			CCode 			: byte;
			Socket 			: word;
			IPXWorkspace 	: array [0..3] of byte;
			DriverWorkspace : array [0..11] of byte;
			ImmAddress 		: array [0..5] of byte;
			FragmentCount 	: word;
			F1Address 		: pointer;
			F1Size			: word;
			F2Address 		: pointer;
			F2Size			: word;
		end;

		PECB = ^TECB;

		{ I/O type}
		TIO = record 
			IPXHeader	: TIPXHeader;
			ECB			: TECB;
			Data 		: array [0..511] of byte;
		end;

		PIO = ^TIO;

var
	fullAddress : TFullAddress;


function AllocIO: boolean;
{ Called automatically on startup and allocated memory of IO }

function FreeIO: boolean;
{ Free memory of IO (it needs to call it in the end) }

function IsIPXInstalled: boolean;
{ Detects IPX }

procedure IPXGetLocalTarget(var addr: TFullAddress);
{ Returns full network address }

function IPXOpenSocket(var socket: word): byte;
{ Opens the `socket` }

procedure IPXCloseSocket(socket: word);
{ Closes the `socket` }

procedure IPXRelinquishControl;
{ Allocate CPU time for IPX. It needs to call it in waiting loop }

procedure IPXSendData(var data);
{ Sends broadcasting 512-bytes packets }

procedure IPXPrepareForReceiving;
{ Prepears receiving one data block }

function IPXReceiveData(var data): boolean;
{ Returns true when reveives data }

{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

uses Dos;

var
	IOBuf	: PIO;

function AllocIO: boolean;
begin

	getmem(IOBuf, sizeof(TIO));
	
	AllocIO := true;

end;

function FreeIO: boolean;
begin

	freemem(IOBuf, sizeof(TIO));
	
	FreeIO := true;

end;

function IsIPXInstalled: boolean; assembler;
asm

	MOV	AX, 7A00h
	INT 2Fh
	AND AL, 1

end;

procedure IPXGetLocalTarget(var addr: TFullAddress);
var
	r: registers;

begin

	r.BX := $09;
	r.ES := seg(addr);
	r.SI := ofs(addr);
	intr($7A, r);

end;

function IPXOpenSocket(var socket: word): byte;
var
	r: registers;

begin

	IPXCloseSocket(socket);

	r.BX := $00;
	r.AL := $FF;
	r.DX := swap(socket);

	intr($7A, r);
	
	socket := swap(r.DX);

	IPXOpenSocket := r.AL;

end;

procedure IPXCloseSocket(socket: word);
var
	r: registers;

begin

	r.BX := $01;
	r.DX := swap(socket);
	intr($7A, r);

end;

function IPXListenForPacket(var ECB: TECB): byte;
{ Not for interface. It needs for start of receiving packets }
var
	r: registers;

begin

	r.BX := $04;
	r.ES := seg(ECB);
	r.SI := ofs(ECB);
	intr($7A, r);

	IPXListenForPacket := r.AL;

end;

function IPXSendPacket(var ECB: TECB): byte;
{ Not for interdace. It needs for preparing of ECB for sending }
var
	r: registers;

begin

	r.BX := $03;
	r.ES := seg(ECB);
	r.SI := ofs(ECB);
	intr($7A, r);

end;

procedure IPXRelinquishControl;
var
	r: registers;

begin

	r.BX := $A;
	intr($7A, r);

end;

procedure IPXSendData(var data);
var
	p: PIO;
	node: array [0..5] of byte; 

begin

	p := IOBuf;

	fillchar(node, 6, $FF); { broadcast }

	with p^.IPXHeader do 
	begin
		Checksum 			:= 0;
		Length 				:= sizeof(TIPXHeader) + 512;
		TransportControl 	:= 0;
		PacketType 			:= 4;
		Destination.Network := fullAddress.Network;
		Destination.Socket 	:= fullAddress.Socket;
		move(node, Destination.Node, sizeof(fullAddress.Node));
		move(fullAddress, Source, sizeof(TFullAddress));
	end;

	with p^.ECB do 
	begin
		Link 			:= nil;
		ESRAddress		:= nil;
		Socket 			:= fullAddress.Socket;
		FragmentCount 	:= 2;
		F1Address		:= @p^.IPXHeader;
		F1Size			:= sizeof(TIPXHeader);
		F2Address		:= @p^.Data;
		F2Size 			:= 512;
		fillchar(IPXWorkspace, 4, 0);
		fillchar(DriverWorkspace, 12, 0);
		move(node, ImmAddress, sizeof(fullAddress.Node));
	end;

	move(data, p^.Data, 512);
	IPXSendPacket(p^.ECB);

end;

procedure IPXPrepareForReceiving;
var
	p: PIO;

begin

	p := IOBuf;

	with p^.ECB do
	begin
		Link 			:= nil;
		ESRAddress		:= nil;
		Socket 			:= fullAddress.Socket;
		FragmentCount	:= 2;
		F1Address		:= @p^.IPXHeader;
		F1Size			:= sizeof(TIPXHeader);
		F2Address		:= @p^.Data;
		F2Size 			:= 512;
		fillchar(IPXWorkspace, 4, 0);
		fillchar(DriverWorkspace, 12, 0);
		fillchar(ImmAddress, 6, $FF);
	end;

	IPXListenForPacket(p^.ECB);

end;

function IPXReceiveData(var data): boolean;
var
	p: PIO;

begin

	p := IOBuf;

	if (p^.ECB.InUse = 0) then
	begin
		move(p^.Data, data, 512);
		IPXPrepareForReceiving;

		IPXReceiveData := true;
	end
	else
		IPXReceiveData := false;

end;

begin

	AllocIO;

end.
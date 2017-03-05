program IPXClient;

uses UIPX, CRT, DOS;

const 
	DEFAULT_SOCKET = $5000;
	PACKAGES = 503;

function GetTimestamp: longint;
var
	hour, min, sec, hund: word;

begin

	GetTime(hour, min, sec, hund);
	GetTimestamp := hour * 360000 + min * 6000 + sec * 100 + hund;

end;

var
	socket 	: word;
	tmp 	: string;
	error 	: integer;
	key  	: char;
	f 		: file;
	fname   : string;
	block 	: array [0..511] of byte;
	fsize 	: longint;
	result  : real;
	arsize  : array [0..7] of byte absolute fsize;
	i 		: longint;
	j  		: byte;
	counted : boolean;
	timer 	: longint;

label theEnd;

begin

	writeln('Hello!');
	write('Testing IPX.. ');

	if (not IsIPXInstalled) then
	begin
		writeln('IPX is not installed. Bye!');
		goto theEnd;
	end
	else
		writeln('IPX is installed.');

	writeln('Default socket is 20480.');
	writeln('If you need another then enter it. Leave the string empty if you don''t.');
	write('New socket: ');
	readln(tmp);

	if (length(tmp) = 0) then
		socket := DEFAULT_SOCKET
	else 
	begin
		val(tmp, socket, error);
		if (error <> 0) then
		begin
			writeln('You didn''t enter a number');
			goto theEnd;
		end;
	end;

	writeln('Trying to open the ', socket, 'th socket..');
	if (IPXOpenSocket(socket) <> 0) then
	begin
		writeln('Coundn''t open the ', socket, 'th socket!');
		goto theEnd;
	end
	else
		writeln('Great! ', socket, 'th socket is open.');

	fullAddress.Socket := swap(socket);

	IPXPrepareForReceiving;

	write('Enter file name [out.jpg]: ');
	readln(tmp);

	if (length(tmp) = 0) then
		fname := 'out.jpg'
	else 
		fname := tmp;	

	assign(f, fname);
	rewrite(f, 1);

	writeln('Waiting for new messages..');

	counted := false;
	i := 0;
	fsize := 1;
	key := #$FF;

	repeat 

		IPXRelinquishControl;
		if (IPXReceiveData(block)) then
		begin
			if (not counted) then 
				timer := GetTimestamp;

			if (block[511] = 0) then break;

			if (counted and ((i + PACKAGES) >= fsize)) then
				blockwrite(f, block, fsize - i)
			else
				blockwrite(f, block, PACKAGES);
			inc(i, PACKAGES);

			if (not counted) then 
			begin
				for j := 0 to 7 do
					arsize[j] := block[PACKAGES + j];

				counted := true;
			end;
		end;

		if keypressed then
			key := readkey;

		if (key = #27) then break;

	until (i >= fsize);

	timer := GetTimestamp - timer;

	result := filesize(f);

	writeln('Expected ', fsize, ' bytes. Received ', trunc(result), ' bytes.');
	writeln(((fsize - result) / fsize * 100):0:2, '% losted.');
	writeln('Time: ', (timer / 100):0:2, ' s');

	IPXRelinquishControl;

	IPXCloseSocket(socket);

	close(f);

theEnd:
	FreeIO;

end.
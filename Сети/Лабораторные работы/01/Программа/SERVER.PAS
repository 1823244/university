program IPXServer;

uses UIPX, CRT;

const 
	DEFAULT_SOCKET = $5000;
	PACKAGES = 503;

var
	key 	: char;
	tmp 	: string;
	socket 	: word;
	error 	: integer;
	f 	 	: file;
	fname   : string;
	block 	: array [0..511] of byte;
	fsize 	: longint;
	arsize  : array [0..7] of byte absolute fsize;
	i 		: longint;
	j 		: byte;

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

	write('Enter file name [in.jpg]: ');
	readln(tmp);

	if (length(tmp) = 0) then
		fname := 'in.jpg'
	else 
		fname := tmp;	

	assign(f, fname);
	reset(f, 1);

	fsize := filesize(f);

	i := 0;

	write('Press any key to start sending..');
	readln;

	writeln('Start sending..');
	key := #$FF;
	
	repeat 

		IPXRelinquishControl;

		if ((i + PACKAGES) >= fsize) then
			blockread(f, block, fsize - i)
		else
			blockread(f, block, PACKAGES);

		for j := 0 to 7 do
		begin

			block[PACKAGES + j] := arsize[j];

		end;

		block[511] := 1;

		IPXSendData(block);
		delay(1);

		if keypressed then
			key := readkey;
		
		if (key = #27) then break;
		
		inc(i, PACKAGES);
		seek(f, i);

	until (i >= fsize);

	for i := 1 to 100 do
	begin

		block[511] := 0;
		IPXSendData(block);
		delay(5);

	end;

	IPXRelinquishControl;

	IPXCloseSocket(socket);

	close(f);

theEnd:
	FreeIO;

end.
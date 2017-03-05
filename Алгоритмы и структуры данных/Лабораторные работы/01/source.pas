program lab1;

uses SD;

var
	first: integer;
	second: real;
	third: colors;
	s: string;

begin

	write('Enter the integer-number, please: ');
	readln(first);
	writeln('Binary code of it: ');
	PrintVar(first, sizeof(first));
	writeln;
	writeln;

	write('Enter the real-number, please: ');
	readln(second);
	writeln('Binary code of it: ');
	PrintVar(second, sizeof(second));
	writeln;

	write('Enter the color, please: ');
	readln(s);
	first := 1;
	case (s) of
		'red': third := red;
		'green': third := green;
		'yellow': third := yellow;
		else
			first := 0;
	end;
	if (first = 1) then
		begin
			writeln('Binary code of it: ');
			PrintVar(third, sizeof(third));
			writeln;
		end
	else
		writeln('Error! You enter not color!');	
	writeln;

	writeln('Now, enter the binary code of integer: ');
	readln(s);
	first := BinToInt(s);
	writeln('Integer: ', first);
	writeln;

	writeln('Enter the binary code of real: ');
	readln(s);
	second := BinToReal(s);
	writeln('Real: ', second);
	writeln;

	writeln('Now, enter the binary code of color: ');
	readln(s);
	case (BinToColor(s)) of
		0: writeln('Color: red');
		1: writeln('Color: yellow');
		2: writeln('Color: green');
		else
			writeln('Error! Incorrect binary code for color!');
	end;

	readln;

end.
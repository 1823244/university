program lab_2_1;

uses Dos;

const
	n = 7;
	k = 3;

type
	arr_bool = array [1..n] of boolean;
	arr_byte = array [1..k] of byte;

var
	d: arr_bool;
	c: arr_byte;
	time: longint;
	output: text;
	task: byte;

function get_time: longint;

	var
		h, m, s, ms: word;

	begin

		Dos.GetTime(h, m, s, ms);
		get_time := ms + 100 * (s + 60 * (m + 60 * h));;

	end;

procedure print_bool;

	var
		i: integer;

	begin

		write(output, '( ');
		for i := 1 to n do
			if d[i] then write(output, i, ' ');
		writeln(output, ')');
	end;

procedure print_byte_k;

	var
		i: integer;

	begin

		write(output, '( ');
		for i := 1 to k do
			write(output, c[i], ' ');
		writeln(output, ')');
	end;

procedure print_byte_n;

	var
		i: integer;

	begin

		write(output, '( ');
		for i := 1 to n do
			write(output, c[i], ' ');
		writeln(output, ')');
	end;

procedure generation(i: byte);

	var
		x: byte;

	begin

		for x := 0 to 1 do
			begin
				d[i] := boolean(x);
				if i = n then
					print_bool
				else
					generation(i + 1);
			end;

	end;

procedure combination(i, b: byte);

	var
		x: byte;

	begin

		for x := b to n - k + i do
			begin
				c[i] := x;

				if i = k then
					print_byte_k
				else
					combination(i + 1, x + 1);
			end;

	end;

function subtraction(p: arr_byte; kp: byte; del: byte): arr_byte;
	
	var
		i, j: byte;
		res: arr_byte;

	begin
	
		j := 1;

		for i := 1 to kp do
			if p[i] <> del then
				begin
					res[j] := p[i];
					inc(j);
				end;

		subtraction := res;
	
	end;

function insertion(p: arr_byte; kp: byte; el: byte): boolean;

	var
		i: byte;

	begin
		
		insertion := FALSE;

		for i := 1 to kp do
			if c[i] = el then
				begin
					insertion := TRUE;
					break;
				end;
	end;

procedure transposition(p: arr_byte; kp: byte; i: byte);
	
	var	
		x: byte;

	begin

		for x := 1 to kp do
			begin
				c[i] := p[x];

				if i = k then
					print_byte_k
				else
					transposition(subtraction(p, kp, c[i]), kp - 1, i + 1);

			end;
	
	end;

procedure placement(p: arr_byte; kp: byte; i: byte);
	
	var	
		x: byte;

	begin

		for x := 1 to kp do
			begin
				c[i] := p[x];

				if i = n then
					print_byte_n
				else
					placement(subtraction(p, kp, c[i]), kp - 1, i + 1);

			end;
	
	end;

var
	p: arr_byte;

begin

	writeln('1, 6, 8 or 13?');
	readln(task);

	assign(output, 'sets.txt');
	rewrite(output);

	case (task) of
			1: 	begin
					time := get_time;
					generation(1);
					writeln('time: ', get_time - time, ' ms');
					readln;
				end;
			6:	begin
					combination(1, 1);
				end;
			8:	begin
					p[1] := 1; p[2] := 2; p[3] := 3; p[4] := 4; p[5] := 5; p[6] := 6; p[7] := 7; p[8] := 8; p[9] := 9; p[10] := 10; p[11] := 11; 
 					time := get_time;
					transposition(p, k, 1);
					writeln('time: ', get_time - time, ' ms');
					readln;
				end;
			13:	begin
					p[1] := 1; p[2] := 3; p[3] := 5; p[4] := 7; p[5] := 9; p[6] := 13; p[7] := 15; p[8] := 17; p[9] := 19; p[10] := 21; p[11] := 23; 
					time := get_time;
					placement(p, k, 1);
					writeln('time: ', get_time - time, ' ms');
					readln;
				end;
			end;	
	
	close(output);

end.
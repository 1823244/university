program lab_2_2;

const
	n = 3;

type
	arr_bool = array [1..n] of boolean;
	matr_bool = array [1..n] of arr_bool;

var
	dm: matr_bool;
	d: arr_bool;


procedure print_bool;

	var
		i: integer;

	begin

		write('( ');
		for i := 1 to n do
			if d[i] then write(i, ' ');
		writeln(')');
	end;

procedure input_set;
	var
		i, k, tmp, j: byte;
	begin

		for i := 1 to n do
			for j := 1 to n do
				dm[i][j] := FALSE;
		for i := 1 to n do
			begin
				write('Enter cardinality of ', i, ' set: ');
					readln(k);
				write('Enter elements of this set: ');
				for j := 1 to k do
					begin
						read(tmp);
						dm[i][tmp] := TRUE;
					end;
			end;
		
	end;

procedure union(j: byte; var m: arr_bool);
	var
		i: byte;
	begin

		for i := 1 to n do
			m[i] := m[i] or dm[j][i];

	end;

function equality(m: arr_bool): Boolean;
	var
		i: byte;
	begin

		i := 1;

		while ((i <= n) and m[i]) do
			inc(i);

		equality := i = (n + 1);

	end;

procedure generation(i: byte; m: arr_bool);

	var
		x: byte;

	begin

		for x := 0 to 1 do
			begin
				d[i] := boolean(x);
				if d[i] then
					union(i, m);
				if equality(m) and (i = n) then
					print_bool;
				if i <> n then
					generation(i + 1, m);
			end;

	end;

var
    m: arr_bool;

begin

	input_set;
	generation(1, m);
	readln;
	readln;

end.
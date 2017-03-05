UNIT SD_STRING; 

{-----------------------------------------------------------------------------}
								INTERFACE
{-----------------------------------------------------------------------------}	

	uses CRT;

	const 
		errors: array [1..3] of string = ('Source string is less than needed', 'Destination string is overflow', 'Destination index out of range values');
		N = 255;

	type 
		t_string = array [1..N + 1] of char;

	function Comp(st1, st2: t_string; var fl: shortint): boolean;
	function Length(st: t_string): byte;
	function Pos(st2, st1: t_string): byte;

	procedure WriteToStr(var st: t_string; s: string);
	procedure WriteFromStr(var s: string; st: t_string);
	procedure InputStr(var st: t_string);
	procedure OutputStr(const st: t_string);
	procedure Insert(st2: t_string; var st1: t_string; index: byte);
	procedure Concat(const st1, st2: t_string; var st3: t_string);
	procedure Copy(st1: t_string; index, count: byte; var st2: t_string);
	procedure CheckErrors;

	var 
		StrError: byte;

{-----------------------------------------------------------------------------}
								IMPLEMENTATION
{-----------------------------------------------------------------------------}

	function Comp(st1, st2: t_string; var fl: shortint): boolean;
		var
			i: byte;

		begin

			i := 1;

			while (boolean(st1[i]) and boolean(st2[i]) and (st1[i] = st2[i])) do 
				inc(i);

			if (st1[i] = st2[i]) then
				begin
					Comp := TRUE;
					fl := 0;
				end
			else 
				begin

					Comp := FALSE;
					if (st1[i] > st2[i]) then
						fl := 1
					else
						fl := 2;
				end;

		end;

	function Length(st: t_string): byte;
		var
			i: byte;

		begin

			i := 1;
			while (boolean(st[i])) do
				inc(i);

			Length := i - 1;

		end;

	function Pos(st2, st1: t_string): byte;
		var
			i, len1, len2: byte;
			fl: shortint;
			finded: boolean;
			tmp_st: t_string;

		begin

			finded := FALSE;
			i := 1;
			len1 := Length(st1);
			len2 := Length(st2);

			while (not finded and boolean(st1[i]) and (len1 - i >= len2)) do
				if (st2[1] = st1[i]) then
					begin
						Copy(st1, i, len2, tmp_st);
						if (StrError = 0) then
							finded := Comp(st2, tmp_st, fl)
						else
							StrError := 0;
						inc(i);
					end
				else
					inc(i);

			if (finded) then
				Pos := i - 1
			else
				Pos := 0;

		end;

	procedure WriteToStr(var st: t_string; s: string);
		var
			i, len, j: byte;

		begin

			len := length(s);

			if (len + Length(st) > N) then
				begin
					StrError := 2;
					exit;
				end
			else
				begin
					i := 1;
					while (boolean(st[i])) do inc(i);

					j := 1;
					while (j <= len) do
						begin
							st[i] := s[j];
							inc(i);
							inc(j);
						end;
				end;

		end;

	procedure WriteFromStr(var s: string; st: t_string);
		var
			i, len: byte;

		begin

			len := length(s);

			if (len + Length(st) > N) then
				begin
					StrError := 2;
					exit;
				end
			else
				begin
					i := 1;
					while (boolean(st[i])) do
						begin
							s := s + st[i];
							inc(i);
						end;
				end;

		end;

	procedure InputStr(var st: t_string);
		var
			tmp_st: t_string;
			len: byte;
			c: char;

		begin

			len := Length(st);
			if (len = N) then
				begin
					StrError := 2;
					exit;
				end
			else
				begin
					Copy(st, 1, len, tmp_st);

					read(c);
					inc(len);
					while ((c <> #10) or (c <> #13)) do
						begin
							if (len <= N) then
								tmp_st[len] := c
							else
								begin
									StrError := 2;
									exit;
								end;

							inc(len);
						end;
				end;

			tmp_st[len] := #0;
			st := tmp_st;

		end;

	procedure OutputStr(const st: t_string);
		var
			i, len: byte;

		begin

			i := 1;
			len := Length(st);

			while (i <= len) do
				begin
					write(st[i]);
					inc(i);
				end;

		end;

	procedure Insert(st2: t_string; var st1: t_string; index: byte);
		var
			len, len2: byte;
			tmp_st: t_string;

		begin

			len := Length(st1);
			len2 := Length(st2);

			if (len + len2 > N) then
				begin
					StrError := 2;
					exit;
				end
			else if ((index > N) or (index < 1)) then
				begin
					StrError := 3;
					exit;
				end
			else
				begin
					Copy(st1, index, len2, tmp_st);
					st1[index] := #0;
					Concat(st1, st2, st1);
					Concat(st1, tmp_st, st1);
				end;

		end;

	procedure Concat(const st1, st2: t_string; var st3: t_string);
		var
			len, len2, len3, i: byte;
			tmp_st: t_string;

		begin

			len := Length(st1);
			len2 := Length(st2);
			len3 := Length(st3);

			if (len + len2 + len3 > N) then
				begin
					StrError := 2;
					exit;
				end
			else 
				begin
					Copy(st3, 1, len3, tmp_st);
					inc(len3);

					i := 1;
					while (i <= len) do
						begin
							tmp_st[len3] := st1[i];
							inc(i);
							inc(len3);
						end;

					i := 1;
					while (i <= len2) do
						begin
							tmp_st[len3] := st2[i];
							inc(i);
							inc(len3);
						end;					
				end;

			tmp_st[i] := #0;
			st3 := tmp_st;

		end;

	procedure Copy(st1: t_string; index, count: byte; var st2: t_string);
		var
			len, last, i: byte;

		begin

			len := Length(st1);
			last := index + count;
			if (len < (last - 1)) then
				begin
					StrError := 1;
					exit;
				end
			else
				begin
					i := 1;
					while (index <= last) do
						begin
							st2[i] := st1[index];
							inc(i);
							inc(index);
						end;
					st2[i] := #0;
				end;

		end;

	procedure CheckErrors;
		begin
		
			if (StrError <> 0) then
				begin
					textcolor(14);
					writeln('Error! ', errors[StrError]);
					readln;
					halt;
				end;

		end;

begin

	StrError := 0;

end.
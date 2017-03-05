UNIT SD;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	uses CRT;

	type
		colors = (red, yellow, green);

	procedure PrintByte(a: byte);
	procedure PrintVar(var a; size: word);
	function BinToInt(const s: string): integer;
	function BinToReal(const s: string): real;
	function BinToColor(const s: string): word;


{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

	procedure PrintByte(a: byte);
	var 
		i, x: byte;

	begin

		for i := 7 downto 0 do 
			begin
				x := (a shr i) and 1;
				write(x);
				if (i mod 4 = 0) then
					begin
						case (i) of
							4: textcolor(8);
							0: textcolor(7);
						end;
						write(' ');
					end;
			end;

	end;

	procedure PrintVar(var a; size: word);
	var 
		i: byte;
		byteArray: array [1..65520] of byte absolute a;

	begin

		for i := size downto 1 do
			PrintByte(byteArray[i]);

	end;

	procedure AddOne(var b: byte; a: word);
	begin
		b := b or (1 shl (a mod 8));
	end;

	function BinToInt(const s: string): integer;
	var
		byteArray: array [1..2] of byte;
		i, j: word;
		x: integer absolute byteArray;

	begin

		for i := 1 to 2 do 
			byteArray[i] := 0;

		j := 0;
		for i := length(s) downto 1 do
			begin
				if s[i] = '1' then
					AddOne(byteArray[(j div 8) + 1], j);

				inc(j);
			end;

		BinToInt := x;

	end;

	function BinToReal(const s: string): real;
	var
		byteArray: array [1..8] of byte;
		i, j: word;
		x: real absolute byteArray;

	begin

		for i := 1 to 8 do
			byteArray[i] := 0;

		j := 0;
		for i := length(s) downto 1 do
			begin
				if s[i] = '1' then
					AddOne(byteArray[(j div 8) + 1], j);

				inc(j);
			end;

		BinToReal := x;

	end;

	function BinToColor(const s: string): word;
	var
		byteArray: array [1..4] of byte;
		i, j: word;
		x: word absolute byteArray;

	begin

		for i := 1 to 4 do
			byteArray[i] := 0;

		j := 0;
		for i := length(s) downto 1 do
			begin
				if s[i] = '1' then
					AddOne(byteArray[(j div 8) + 1], j);

				inc(j);
			end;

		BinToColor := x;

	end;

begin
		
end.
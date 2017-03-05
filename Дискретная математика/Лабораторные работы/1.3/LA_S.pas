UNIT LA_S;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	const
		KU = 10;
	type
		variety = array [1..KU] of Integer;

	function equality(A, B: variety; KA, KB: Word): Boolean;
	function insertion(A, B: variety; KA, KB: Word): Boolean;
	procedure union(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	procedure intersection(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	procedure subtraction(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	procedure symmetric_difference(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	procedure complement(A: variety; KA: Word; var C: variety; var KC: Word);
	procedure input_set(var A: variety; var KA: Word; number: Word);
	procedure output_set(A: variety; KA: Word);

{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

	function exist_in_array(Element: Integer; B: variety; KB: Word): Boolean;
	var
		i: Word;
		exist: Boolean;
	begin

		i := 1;
		exist := FALSE;
		while ((i <= KB) and (not exist)) do
			begin
				if (Element = B[i]) then
					exist := TRUE;
				inc(i);
			end;
		exist_in_array := exist;

	end;


	procedure sort(var A: variety; var KA: Word);
	var
		i, j, k: Word;
		x: Integer;
	begin
		
		for i := 1 to KA do
			begin
				k := i;
				x := A[i];

				for j := i + 1 to KA do
					if (A[j] < x) then
						begin
							k := j;
							x := A[j];
						end;

				A[k] := A[i];
				A[i] := x;

			end;

	end;

	function equality(A, B: variety; KA, KB: Word): Boolean;
	var
		i: Word;
		F: Boolean;
	begin
		
		i := 1;
		F := KA = KB;
		while ((i <= KA) and F) do
			begin
				F := A[i] = B[i];
				inc(i);
			end;

		equality := F;

	end;

	function insertion(A, B: variety; KA, KB: Word): Boolean;
	var
		i, j: Word;
		F: Boolean;
	begin
		
		F := KA <= KB;
		i := 1;
		j := 1;

		while ((i <= KA) and F) do
			if (A[i] = B[i]) then
				begin
					inc(i);
					inc(j);
				end
			else if (A[i] > B[j]) then
				inc(j)
			else
				F := FALSE;

		insertion := F;

	end;

	procedure union(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i, j: Word;
	begin
		
		i := 1;
		j := 1;
		KC := 0;

		while ((i <= KA) and (j <= KB)) do
			begin
				inc(KC);
				if (A[i] = B[j]) then
					begin
						C[KC] := A[i];
						inc(i);
						inc(j);
					end
				else if (A[i] > B[j]) then
					begin
						C[KC] := B[j];
						inc(j);
					end
				else
					begin
						C[KC] := A[i];
						inc(i);
					end;
			end;

		while (i <= KA) do
			begin
				inc(KC);
				C[KC] := A[i];
				inc(i);
			end;
		while (j <= KB) do
			begin
				inc(KC);
				C[KC] := B[j];
				inc(j);
			end;

	end;

	procedure intersection(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i, j: Word;
	begin
			
		i := 1;
		j := 1;
		KC := 0;

		while ((i <= KA) and (j <= KB)) do
			if (A[i] = B[j]) then
				begin
					inc(KC);
					C[KC] := A[i];
					inc(i);
					inc(j);
				end
			else if (A[i] > B[j]) then
				inc(j)
			else
				inc(i);

	end;	

	procedure subtraction(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i, j: Word;
	begin
		
		i := 1;
		j := 1;
		KC := 0;

		while ((i <= KA) and (j <= KB)) do
			if (A[i] = B[j]) then
				begin
					inc(i);
					inc(j);
				end
			else if (A[i] > B[j]) then
				inc(j)
			else
				begin
					inc(KC);
					C[KC] := A[i];
					inc(i);
				end;

		while (i <= KA) do
			begin
				inc(KC);
				C[KC] := A[i];
				inc(i);
			end;

	end;

	procedure symmetric_difference(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i, j: Word;
	begin
		
		i := 1;
		j := 1;
		KC := 0;

		while ((i <= KA) and (j <= KB)) do
			if (A[i] = B[j]) then
				begin
					inc(i);
					inc(j);
				end
			else if (A[i] > B[j]) then
				begin
					inc(KC);
					C[KC] := B[j];
					inc(j);
				end
			else
				begin
					inc(KC);
					C[KC] := A[i];
					inc(i);
				end;

		while (i <= KA) do
			begin
				inc(KC);
				C[KC] := A[i];
				inc(i);
			end;

		while (j <= KB) do
			begin
				inc(KC);
				C[KC] := B[j];
				inc(j);
			end;
	end;

	procedure complement(A: variety; KA: Word; var C: variety; var KC: Word);
	var
		u, i: Word;
	begin

		u := 1;
		i := 1;
		KC := 0;

		while ((u <= KU) and (i <= KA)) do
			if (u = A[i]) then
				begin
					inc(u);
					inc(i);
				end
			else if (u > A[i]) then
				inc(i)
			else
				begin
					inc(KC);
					C[KC] := u;
					inc(u);
				end;
		
		while (u <= KU) do
			begin
				inc(KC);
				C[KC] := u;
				inc(u);
			end;

	end;

	procedure input_set(var A: variety; var KA: Word; number: Word);
	var
		i: Word;
	begin
		
		write('Enter cardinality of ', number, ' set: ');
			readln(KA);
		write('Enter elements of this set: ');

		for i := 1 to KA do
			read(A[i]);

		sort(A, KA);

	end;

	procedure output_set(A: variety; KA: Word);
	var
		i: Word;
	begin
		
		writeln('Result: ');
		for i := 1 to KA do
			writeln('':5, i, ': ', A[i]);

	end;

begin
	
end.
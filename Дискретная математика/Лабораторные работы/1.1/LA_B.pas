UNIT LA_B;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	const
		KU = 1000;
	type
		boolVariety = array [1..KU] of Boolean;

	function equality(A, B: boolVariety): Boolean;
	function insertion(A, B: boolVariety): Boolean;
	procedure union(A, B: boolVariety; var C: boolVariety);
	procedure intersection(A, B: boolVariety; var C: boolVariety);
	procedure subtraction(A, B: boolVariety; var C: boolVariety);
	procedure symmetric_difference(A, B: boolVariety; var C: boolVariety);
	procedure complement(A: boolVariety; var C: boolVariety);
	procedure input_set(var A: boolVariety; number: Word);
	procedure output_set(A: boolVariety);

{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}
	
	function equality(A, B: boolVariety): Boolean;
	var
		i: Word;
		F: Boolean;
	begin
		
		i := 1;
		F := TRUE;

		while ((i <= KU) and F) do
			begin
				F := A[i] = B[i];
				inc(i);
			end;

		equality := F;

	end;

	function insertion(A, B: boolVariety): Boolean;
	var
		i: Word;
		F: Boolean;
	begin
		
		i := 1;
		F := TRUE;
		while ((i <= KU) and F) do
			begin
				F := A[i] <= B[i];
				inc(i);
			end;

		insertion := F;

	end;

	procedure union(A, B: boolVariety; var C: boolVariety);
	var
		i: Word;
	begin

		for i := 1 to KU do
			C[i] := A[i] or B[i];

	end;

	procedure intersection(A, B: boolVariety; var C: boolVariety);
	var
		i: Word;
	begin

		for i := 1 to KU do
			C[i] := A[i] and B[i];
		
	end;

	procedure subtraction(A, B: boolVariety; var C: boolVariety);
	var
		i: Word;
	begin
		
		for i := 1 to KU do
			C[i] := A[i] > B[i];

	end;

	procedure symmetric_difference(A, B: boolVariety; var C: boolVariety);
	var
		i: Word;
	begin
		
		for i := 1 to KU do
			C[i] := A[i] <> B[i];

	end;

	procedure complement(A: boolVariety; var C: boolVariety);
	var
		i: Word;
	begin
		
		for i := 1 to KU do
			C[i] := not A[i];

	end;

	procedure input_set(var A: boolVariety; number: Word);
	var
		i, n, K: Word;
	begin

		for i := 1 to KU do
			A[i] := FALSE;
		write('Enter cardinality of ', number, ' set: ');
			readln(K);
		write('Enter elements of this set: ');

		for i := 1 to K do
			begin
				read(n);
				A[n] := TRUE;
			end;
		
	end;

	procedure output_set(A: boolVariety);
	var
		i, j: Word;
	begin
		j := 1;
		writeln('Result: ');
		for i := 1 to KU do
			if A[i] then
				begin
					writeln('':5, j, ': ', i);
					inc(j);
				end;

	end;

begin
		
end.
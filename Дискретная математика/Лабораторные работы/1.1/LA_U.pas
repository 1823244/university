UNIT LA_U;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}							
	
	const
		KU = 1000;
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

	function insertion(A, B: variety; KA, KB: Word): Boolean;
	var
		i: Word;
		F: Boolean;
	begin

		F := KA <= KB;
		i := 1;
		while ((i <= KA) and F) do 
			begin
				F := exist_in_array(A[i], B, KB);
				inc(i);
			end;
		
		insertion := F;
		
	end;

	function equality(A, B: variety; KA, KB: Word): Boolean;
	var
		i: Word;
		F: Boolean;
	begin

		F := KA = KB;
		i := 1;
		while ((i <= KA) and F) do 
			begin
				F := exist_in_array(A[i], B, KB);
				inc(i);
			end;
		
		equality := F;
		
	end;

	procedure union(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i: Word;
	begin

		C := A;
		KC := KA;

		for i := 1 to KB do
			if (not(exist_in_array(B[i], C, KC))) then
				begin
					inc(KC);
					C[KC] := B[i];
				end;

	end;

	procedure intersection(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i: Word;
	begin
		
		KC := 0;
		if (KA <= KB) then
			begin
				for i := 1 to KA do
					if (exist_in_array(A[i], B, KB)) then
						begin
							inc(KC);
							C[KC] := A[i];
						end;
			end
		else
			begin
				for i := 1 to KB do
					if (exist_in_array(B[i], A, KA)) then
						begin
							inc(KC);
							C[KC] := B[i];
					end;
			end;

	end;

	procedure subtraction(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		i: Word;
	begin
		
		KC := 0;
		for i := 1 to KA do
			if (not(exist_in_array(A[i], B, KB))) then
				begin
					inc(KC);
					C[KC] := A[i];
				end;
	end;


	procedure symmetric_difference(A, B: variety; KA, KB: Word; var C: variety; var KC: Word);
	var
		C1, C2: variety;
		KC1, KC2: Word;
	begin

		KC1 := 0;
		subtraction(A, B, KA, KB, C1, KC1);
		KC2 := 0;
		subtraction(B, A, KB, KA, C2, KC2);
		KC := 0;
		union(C1, C2, KC1, KC2, C, KC);

	end;

	procedure complement(A: variety; KA: Word; var C: variety; var KC: Word);
	var
		u: Word;
	begin

		KC := 0;
		for u := 1 to KU do
			if (not(exist_in_array(u, A, KA))) then
				begin
					inc(KC);
					C[KC] := u;
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
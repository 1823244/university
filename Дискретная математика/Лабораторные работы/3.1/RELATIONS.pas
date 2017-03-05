UNIT RELATIONS;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	uses CRT;

	const
		N = 10;
		moreN = N + 2;
	type
		boolVariety = array [1..N, 1..N] of Boolean;

	procedure union(A, B: boolVariety; var C: boolVariety);
	procedure intersection(A, B: boolVariety; var C: boolVariety);
	procedure subtraction(A, B: boolVariety; var C: boolVariety);
	procedure symmetric_difference(A, B: boolVariety; var C: boolVariety);
	procedure complement(A: boolVariety; var C: boolVariety);
	procedure reverse(A: boolVariety; var C: boolVariety);
	procedure composition(A, B: boolVariety; var C: boolVariety);
	function isReflexivity(A: boolVariety): byte;
	function isSymmetry(A: boolVariety): byte;
	function isTransitivity(A: boolVariety): byte;
	procedure transitivityClosure(A: boolVariety; var C: boolVariety);
	procedure printProperties(A: boolVariety; nameOf: string);
	procedure output_relations(A: boolVariety; nameOf: string);

{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

	procedure union(A, B: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
				C[x, y] := A[x, y] or B[x, y];

	end;

	procedure intersection(A, B: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
				C[x, y] := A[x, y] and B[x, y];

	end;

	procedure subtraction(A, B: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
				C[x, y] := A[x, y] > B[x, y];

	end;

	procedure symmetric_difference(A, B: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
				C[x, y] := A[x, y] <> B[x, y];

	end;

	procedure complement(A: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
			C[x, y] := not A[x, y];

	end;

	procedure reverse(A: boolVariety; var C: boolVariety);
	var
		x, y: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
			if (A[x, y]) then
				C[y, x] := TRUE;

	end;

	procedure composition(A, B: boolVariety; var C: boolVariety);
	var
		x, y, z: Byte;
	begin

		for x := 1 to N do
			for y := 1 to N do
				begin
					C[x, y] := FALSE;
					z := 1;

					while ((z < 11) and (not C[x, y])) do
						begin
							C[x, y] := C[x, y] or A[x, z] and B[z, y];
							inc(z);
						end;
				end;

	end;

	function isReflexivity(A: boolVariety): byte;
	var
		x, ref: byte;
	begin

		ref := byte(a[1, 1]) + 1;
		x := 2;

		while ((x <= N) and (a[x, x] = a[1, 1])) do
			inc(x);

		if (x = (N + 1)) then
			isReflexivity := ref
		else
			isReflexivity := 0;

	end;

	function isSymmetry(A: boolVariety): byte;
	var
		x, y, sym: byte;
		boolSym: boolean;

	begin

		sym := 0;
		x := 1;
		y := 2;

		while (x < N) do
			begin
				while (y <= N) do
					begin
						if ((sym = 0) and (a[x, y] or a[y, x])) then
							sym := byte(a[x, y]) + byte(a[y, x])
						else if ((sym <> 0) and (a[x, y] or a[y, x]) and (sym <> byte(a[x, y]) + byte(a[y, x]))) then
							begin
								x := moreN;
								y := moreN;
							end;
						inc(y);
					end;
						
				inc(x);
				y := x + 1;
			end;

		if (x < moreN) then
			isSymmetry := sym
		else
			isSymmetry := 0;

	end;

	function isTransitivity(A: boolVariety): byte;
	var
		x, y, z, tran: byte;

	begin

		tran := 0;
		x := 1;
		y := 1;
		z := 1;

		while (x <= N) do
			begin
				while (z <= N) do
					begin
						while (y <= N) do
							begin
								if (a[x, z] and a[z, y]) then
									if (tran = 0) then
										tran := byte(a[x, y]) + 1
									else if (tran <> byte(a[x, y]) + 1) then
										begin
											x := moreN;
											y := moreN;
											z := moreN;
										end;
								inc(y);
							end;
						inc(z);
						y := 1;
					end;
				inc(x);
				z := 1;
			end;

		if (x < moreN) then
			isTransitivity := tran
		else
			isTransitivity := 0;

	end;

	function isCompleteness(A: boolVariety): byte;
	var
		x, y: byte;

	begin

		x := 1;
		y := 2;
		isCompleteness := 2;

		while (x <= N) do
			begin
				while (y <= N) do
					begin
						if (not (a[x, y] or a[y, x])) then
							begin
								x := moreN;
								y := moreN;
								isCompleteness := 0;
							end;
						inc(y);
					end;
				inc(x);
				y := x + 1;
			end;

	end;

	procedure transitivityClosure(A: boolVariety; var C: boolVariety);
	var
		powerA: boolVariety;
		i: byte;

	begin

		C := A;
		powerA := A;

		for i := 2 to (N - 1) do
			begin
				composition(A, powerA, powerA);
				union(C, powerA, C);
			end;

	end;

	procedure transitivityClosureWarshall(A: boolVariety; var C: boolVariety);
	var
		x, y, z: byte;

	begin

		C := A;

		for z := 1 to N do
			for x := 1 to N do
				for y := 1 to N do
					C[x, y] := C[x, y] or C[x, z] and C[z, y];

	end;

	procedure printProperty(valueOf: byte; nameOf: string; var propertiesResult: string);
	begin

		if (valueOf = 1) then
			propertiesResult := propertiesResult + 'anti-'
		else if (valueOf = 0) then
			propertiesResult := propertiesResult + 'not ';
		propertiesResult := propertiesResult + nameOf;

	end;

	procedure printProperties(A: boolVariety; nameOf: string);
	var
		isCompletenessLocal, isTransitivityLocal, isSymmetryLocal, isReflexivityLocal,
			isToleranceLocal, isEquivalenceLocal, isOrderingLocal: byte;
		propertiesResult: string;

	begin

		isCompletenessLocal := isCompleteness(A);
		isTransitivityLocal := isTransitivity(A);
		isSymmetryLocal := isSymmetry(A);
		isReflexivityLocal := isReflexivity(A);
		if ((isReflexivityLocal + isSymmetryLocal) = 4) then
			isToleranceLocal := 2
		else
			isToleranceLocal := 0;
		if ((isToleranceLocal + isTransitivityLocal) = 4) then
			isEquivalenceLocal := 2
		else
			isEquivalenceLocal := 0;
		
		if ((isSymmetryLocal = 1) and (isTransitivityLocal = 2)) then
			begin
				isOrderingLocal := 1;
				if (isReflexivityLocal = 2) then
					isOrderingLocal := 2
				else if (isReflexivityLocal = 1) then
					isOrderingLocal := 3;
				if (isCompletenessLocal = 2) then
					if (isOrderingLocal = 2) then
						isOrderingLocal := 5
					else if (isOrderingLocal = 1) then
						isOrderingLocal := 6
					else if (isOrderingLocal = 0) then
						isOrderingLocal := 4;
			end
		else
			isOrderingLocal := 0;

		propertiesResult := 'Properties of ' + nameOf + ': ';
		printProperty(isReflexivityLocal, 'reflexivity, ', propertiesResult);
		printProperty(isSymmetryLocal, 'symmetry, ', propertiesResult);
		printProperty(isTransitivityLocal, 'transitivity, ', propertiesResult);
		printProperty(isCompletenessLocal, 'completeness, ', propertiesResult);
		printProperty(isToleranceLocal, 'tolerance, ', propertiesResult);
		printProperty(isEquivalenceLocal, 'equivalence, ', propertiesResult);

		case (isOrderingLocal) of
			0: printProperty(0, 'ordering.', propertiesResult);
			1: printProperty(2, 'ordering.', propertiesResult);
			2: printProperty(2, 'unstrict ordering.', propertiesResult);
			3: printProperty(2, 'strict ordering.', propertiesResult);
			4: printProperty(2, 'linear ordering.', propertiesResult);
			5: printProperty(2, 'linear unstrict ordering.', propertiesResult);
			6: printProperty(2, 'linear strict ordering.', propertiesResult);
		end;

		writeln(propertiesResult);

	end;

	procedure output_relations(A: boolVariety; nameOf: string);
	var
		x, y: Byte;
	begin

		textColor(7);
		writeln;
		write(' Output of ');
		textColor(2);
		writeln(nameOf);
		textColor(8);
		write('    ');
		for x := 1 to N do
			write(' ', x);
		writeln;
		write('    ');
		for x := 1 to N do
			write('--');
		writeln;
		for x := 1 to N do
			begin
				textColor(8);
				if (x <> 10) then
					write(' ');
				write(x, ' |');

				for y := 1 to N do
					begin
						textColor(15);
						if (A[x, y]) then
							begin
								textColor(2);
								write(' ', 1);
							end
						else
							write(' ', 0);
					end;
				writeln;
			end;

	end;

begin

end.
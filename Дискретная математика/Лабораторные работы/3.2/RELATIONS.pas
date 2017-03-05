UNIT RELATIONS;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	uses CRT;

	const
		N = 15;
		moreN = N + 2;

	type
		boolVariety = array [1..N, 1..N] of Boolean;

	procedure union(A, B: boolVariety; var C: boolVariety; var checker: boolean);
	procedure intersection(A, B: boolVariety; var C: boolVariety);
	procedure subtraction(A, B: boolVariety; var C: boolVariety);
	procedure symmetric_difference(A, B: boolVariety; var C: boolVariety);
	procedure complement(A: boolVariety; var C: boolVariety);
	procedure reverse(A: boolVariety; var C: boolVariety);
	procedure composition(A, B: boolVariety; var C: boolVariety; var counter: longint);

	function isInsert(A, B: boolVariety): boolean;
	function isReflexivity(A: boolVariety): byte;
	function isSymmetry(A: boolVariety): byte;
	function isTransitivity(A: boolVariety): byte;
	
	procedure generate(amount: word; var A: boolVariety);

	procedure transitivityClosureSlow(A: boolVariety; var C: boolVariety; var counter: longint);
	procedure transitivityClosure(A: boolVariety; var C: boolVariety; var counter: longint);
	procedure transitivityClosureImproved(A: boolVariety; var C: boolVariety; var counter: longint);
	procedure transitivityClosureWarshall(A: boolVariety; var C: boolVariety; var counter: longint);
	procedure transitivityClosureWarshallImproved(A: boolVariety; var C: boolVariety; var counter: longint);
	
	procedure printProperties(A: boolVariety; nameOf: string);
	procedure output_relations(A: boolVariety; nameOf: string);

{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

	function isInsert(A, B: boolVariety): boolean;
	var
		x, y: byte;

	begin

		isInsert := TRUE;
		for x := 1 to N do
			for y := 1 to N do
				if (A[x, y] and (not B[x, y])) then
					begin
						isInsert := FALSE;
						exit;
					end;

	end;
	
	procedure union(A, B: boolVariety; var C: boolVariety; var checker: boolean);
	var
		x, y: Byte;
	begin

		checker := FALSE;
		for x := 1 to N do
			for y := 1 to N do
				begin
					C[x, y] := A[x, y] or B[x, y];
					if (A[x, y] xor B[x, y]) then
						checker := TRUE;
				end;

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

	procedure composition(A, B: boolVariety; var C: boolVariety; var counter: longint);
	var
		x, y, z: Byte;
		checker: boolean;
	begin

		checker := FALSE;
		for x := 1 to N do
			for y := 1 to N do
				begin
					C[x, y] := FALSE;
					z := 1;

					while ((z <= N) and (not C[x, y])) do
						begin
							C[x, y] := A[x, z] and B[z, y];
							inc(z);
							inc(counter);
						end;
					checker := checker or C[x, y];
				end;

		if (not checker) then
			counter := -counter;

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

	procedure transitivityClosureSlow(A: boolVariety; var C: boolVariety; var counter: longint);
	var
		powerC: boolVariety;
		checker: boolean;

	begin

		C := A;
		powerC := C;
		counter := 0;
		composition(C, C, powerC, counter);
		if (counter < 0) then
			counter := -counter;

		while (not isInsert(powerC, C)) do
			begin
				union(C, powerC, C, checker);
				composition(C, C, powerC, counter);
				if (counter < 0) then
					counter := -counter;
			end;

	end;

	procedure transitivityClosure(A: boolVariety; var C: boolVariety; var counter: longint);
	var
		powerA: boolVariety;
		i: byte;
		checker: boolean;

	begin

		C := A;
		powerA := A;
		counter := 0;
		i := 2;
		checker := TRUE;

		while (i < N) do
			begin
				composition(A, powerA, powerA, counter);
				if (counter < 0) then
					begin
						counter := -counter;
					end;
				union(C, powerA, C, checker);
				inc(i);
			end;

	end;

	procedure transitivityClosureImproved(A: boolVariety; var C: boolVariety; var counter: longint);
	var
		powerA: boolVariety;
		i: byte;
		checker: boolean;

	begin

		C := A;
		powerA := A;
		counter := 0;
		i := 2;
		checker := TRUE;

		while ((i < N) and checker) do
			begin
				composition(A, powerA, powerA, counter);
				if (counter < 0) then
					begin
						counter := -counter;
						exit;
					end;
				union(C, powerA, C, checker);
				inc(i);
			end;

	end;

	procedure transitivityClosureWarshall(A: boolVariety; var C: boolVariety; var counter: longint);
	var
		x, y, z: byte;

	begin

		C := A;
		counter := 0;
		for z := 1 to N do
			for x := 1 to N do
				for y := 1 to N do
					begin
						C[x, y] := C[x, y] or C[x, z] and C[z, y];
						inc(counter);
					end;

	end;

	procedure transitivityClosureWarshallImproved(A: boolVariety; var C: boolVariety; var counter: longint);
	var
		x, y, z: byte;

	begin

		C := A;
		counter := 0;
		for z := 1 to N do
			for x := 1 to N do
				if (C[x, z]) then
					begin
						for y := 1 to N do
							begin
								C[x, y] := C[x, y] or C[z, y];
								inc(counter);
							end;
					end
				else
					inc(counter);

	end;

	procedure generate(amount: word; var A: boolVariety);
	var
		x, y, randX, randY: byte;
		i: word;
	begin

		if (amount = N * N) then	
			begin
				for x := 1 to N do
					for y := 1 to N do
						A[x ,y] := TRUE;
			end
		else
			begin
				for x := 1 to N do
					for y := 1 to N do
						A[x ,y] := FALSE;

				i := 0;
				randomize;
				while (i <> amount) do
					begin
						randX := random(N) + 1;
						randY := random(N) + 1;
						if (A[randX, randY]) then
							continue
						else
							begin
								A[randX, randY] := TRUE;
								inc(i);
							end;
					end;
			end;

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
		textColor(7);
	end;

begin

end.
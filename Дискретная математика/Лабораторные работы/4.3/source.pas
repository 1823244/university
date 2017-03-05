program lab_4_3;

const 
	N = 10;
	M = 20;

type 
	booleanMatrix = array [1..N, 1..N] of boolean;
	pairs = array [1..2, 1..M] of byte;

function generateGraphWithBridgeFirst(var A: booleanMatrix): boolean;
	var
		i, j: byte;

	begin

		for i := 1 to N do 
			for j := 1 to N do
				A[i, j] := FALSE;

		A[1, 2] := TRUE; A[2, 1] := TRUE;
		A[1, 3] := TRUE; A[3, 1] := TRUE;
		A[2, 4] := TRUE; A[4, 2] := TRUE;
		A[4, 5] := TRUE; A[5, 4] := TRUE;
		A[3, 5] := TRUE; A[5, 3] := TRUE;
		A[5, 6] := TRUE; A[6, 5] := TRUE;
		A[6, 7] := TRUE; A[7, 6] := TRUE;
		A[7, 8] := TRUE; A[8, 7] := TRUE;
		A[8, 9] := TRUE; A[9, 8] := TRUE;
		A[9, 10] := TRUE; A[10, 9] := TRUE;
		A[10, 6] := TRUE; A[6, 10] := TRUE;

		generateGraphWithBridgeFirst := TRUE;

	end;

function generateGraphWithBridgeSecond(var A: booleanMatrix): boolean;
	var
		i, j: byte;

	begin

		for i := 1 to N do 
			for j := 1 to N do
				A[i, j] := FALSE;

		A[1, 2] := TRUE; A[2, 1] := TRUE;
		A[1, 3] := TRUE; A[3, 1] := TRUE;
		A[2, 4] := TRUE; A[4, 2] := TRUE;
		A[3, 4] := TRUE; A[4, 3] := TRUE;
		A[4, 5] := TRUE; A[5, 4] := TRUE;
		A[5, 6] := TRUE; A[6, 5] := TRUE;
		A[5, 7] := TRUE; A[7, 5] := TRUE;
		A[6, 7] := TRUE; A[7, 6] := TRUE;
		A[7, 8] := TRUE; A[8, 7] := TRUE;
		A[8, 9] := TRUE; A[9, 8] := TRUE;
		A[9, 10] := TRUE; A[10, 9] := TRUE;
		A[10, 8] := TRUE; A[8, 10] := TRUE;

		generateGraphWithBridgeSecond := TRUE;

	end;

function generateGraph(var A: booleanMatrix): boolean;
	var
		counter: integer;
		i, j, x, y: byte;
		linker: array [1..N] of boolean;

	begin

		generateGraph := TRUE;
		if ((M + 1) < N) then
			begin
				generateGraph := FALSE;
				exit;
			end
		else 
			begin
				if (M >= ((N * N - N) div 2)) then
					begin	
						for i := 1 to N do
							for j := i + 1 to N do
								begin
									A[i, j] := TRUE;
									A[j, i] := TRUE;
								end;
					end
				else
					begin
						counter := 0;

						for i := 1 to N do
							begin
								linker[i] := FALSE;
								for j := 1 to N do
									A[i, j] := FALSE;
							end;

						linker[1] := TRUE;
						for i := 1 to N - 1 do
							begin
								x := random(N) + 1;
								while ((linker[x] = TRUE) or (x = i)) do
									x := random(N) + 1;
								linker[x] := TRUE;
								A[i, x] := TRUE;
								A[x, i] := TRUE;
								inc(counter);
							end;

						while (counter < M) do
							begin
								x := random(N) + 1;
								y := random(N) + 1;

								if ((not A[x, y]) and (x <> y)) then
									begin
										A[x, y] := TRUE;
										A[y, x] := TRUE;
										inc(counter);
									end;
							end;
					end;
			end;
	end;

function kruskal(A: booleanMatrix): byte;
	var
		B: array [1..N] of byte;
		i, j, k, counter: byte;

	begin

		counter := N;
		for i := 1 to N do
			B[i] := i;

		for i := 1 to N do
			for j := i + 1 to N do
				if (A[i, j] AND (B[i] <> B[j])) then
					begin
						for k := 1 to N do
							if (B[k] = B[j]) then B[k] := B[i];
						dec(counter);
					end;

		kruskal := counter;

	end;

function findBridges(A: booleanMatrix; var bridges: pairs): byte;
	var
		index, i, j: byte;

	begin

		index := 0;
		for i := 1 to N do
			for j := 1 to N do
				if (A[i, j]) then
					begin
						A[i, j] := FALSE;
						if (kruskal(A) = 2) then
							begin
								inc(index);
								bridges[1, index] := i;
								bridges[2, index] := j;
							end;
						A[i, j] := TRUE;
					end;

		findBridges := index;

	end;

procedure matrixToCSV(A: booleanMatrix);
	var 
		i, j: byte;
		f: text;

	begin

		assign(f, 'graph.csv');
		rewrite(f);

		write(f, ';');
		for i := 1 to N do
			write(f, ';', chr(ord('A') + i - 1));
		writeln(f);
		for i := 1 to N do
			begin
				write(f, chr(ord('A') + i - 1));
				for j := 1 to N do
					write(f, ';', byte(A[i, j]));
				writeln(f);
			end;

		close(f);

	end;
`
var
	A: booleanMatrix;
	counter, i: byte;
	bridges: pairs;

begin

	if (generateGraphWithBridgeSecond(A)) then
		begin
			counter := findBridges(A, bridges);
			if (counter > 0) then
				begin
					for i := 1 to counter do
						writeln(i, ': ', chr(ord('A') + bridges[1, i] - 1), '-', chr(ord('A') + bridges[2, i] - 1));
				end
			else
				writeln('Bridges not found.');
		end
	else
		writeln('Connected graph with ', N, 'vertices must have at least ', N - 1, ' edges.');

	matrixToCSV(A);
`
end.
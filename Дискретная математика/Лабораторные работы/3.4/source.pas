program lab_3_4;

const 
	N1 = 9;
	N2 = 13;
	N = N2;

type setM = record
		x: integer;
		y: integer;
		isHave: boolean;
		weight: byte;
	end;
	arrayOfSetM = array [1..N] of setM;
	booleanMatrix = array [1..N, 1..N] of boolean;

var
	M1, M2: arrayOfSetM;
	A1, A2, dominateA1, dominateA2: booleanMatrix;

procedure generationM1();
	var
		i: integer;

	begin

		for i := 1 to N do
			begin
				M1[i].isHave := FALSE;
				M1[i].weight := 0;
			end;


		for i := 1 to N1 do
			M1[i].isHave := TRUE;

		M1[1].x := -1;
		M1[1].y := -1;
		M1[2].x := 0;
		M1[2].y := -1;
		M1[3].x := 1;
		M1[3].y := -1;
		M1[4].x := -1;
		M1[4].y := 0;
		M1[5].x := 0;
		M1[5].y := 0;
		M1[6].x := 1;
		M1[6].y := 0;
		M1[7].x := -1;
		M1[7].y := 1;
		M1[8].x := 0;
		M1[8].y := 1;
		M1[9].x := 1;
		M1[9].y := 1;

	end;

procedure generationM2();
	var
		i: integer;

	begin

		for i := 1 to N do
			begin
				M2[i].isHave := FALSE;
				M2[i].weight := 0;
			end;

		for i := 1 to N1 do
			begin
				M2[i].x := M1[i].x;
				M2[i].y := M1[i].y;
				M2[i].isHave := TRUE;
			end;

		for i := 10 to N2 do
			M2[i].isHave := TRUE;

		M2[10].x := 0;
		M2[10].y := -2;
		M2[11].x := -2;
		M2[11].y := 0;
		M2[12].x := 0;
		M2[12].y := 2;
		M2[13].x := 2;
		M2[13].y := 0;

	end;

procedure generationM(M: arrayOfSetM; number: integer; var A: booleanMatrix);
	var
		i, j: integer;

	begin

		for i := 1 to N do
			begin
				M[i].isHave := FALSE;
				for j := 1 to N do
					A[i, j] := FALSE;
			end;

		for i := 1 to number do
			for j := 1 to number do
				if ((M[i].x <= M[j].x) AND (M[i].y <= M[j].y)) then
					A[i, j] := TRUE;

	end;

procedure outputM(M: arrayOfSetM; number: integer; A: booleanMatrix);
	var
		i, j: integer;

	begin

		write('':5);
		for i := 1 to number do
			begin
				if (M[i].x >= 0) then write(' ');
				write(M[i].x, ';');
				if (M[i].y >= 0) then write(' ');
				write(M[i].y, ' ');
			end;
		writeln;

		for i := 1 to number do
			begin
				if (M[i].x >= 0) then write(' ');
				write(M[i].x, ';');
				if (M[i].y >= 0) then write(' ');
				write(M[i].y);
				
				for j := 1 to number do
					write('':2, byte(A[i, j]), '':3);
				writeln;
			end;

		readln;
	end;

procedure dominationM(var A: booleanMatrix; number: integer);
	var
		i, j, k: integer;
	begin

		for i := 1 to number do
			A[i, i] := FALSE;

		for i := 1 to number do
			for j := 1 to number do
				if (A[i, j]) then
					for k := 1 to number do
						if (A[i, k] AND A[k, j]) then
							A[i, j] := FALSE;

	end;

procedure outputWeight(M: arrayOfSetM; number: integer);
	var
		i: byte;

	begin


		for i := 1 to number do
			begin
				if (M[i].x >= 0) then write(' ');
				write(M[i].x, '; ');
				if (M[i].y >= 0) then write(' ');
				write(M[i].y, ' : ', M[i].weight);
				writeln;
			end;

		readln;

	end;

procedure topologySort(A: booleanMatrix; var M: arrayOfSetM; number: integer);
	procedure topologySortR(i: integer; weight: byte);
		var
			j: integer;

		begin

			inc(weight);
			for j := 1 to number do
				if (A[i, j] and (weight > M[j].weight)) then
					begin
						M[j].weight := weight;
						topologySortR(j, weight);
					end;

		end;

	var
		j, i: integer;
		flag: boolean;

	begin

		for j := 1 to number do
			begin
				flag := TRUE;
				for i := 1 to number do
					if (A[i, j]) then
						begin
							flag := FALSE;
							break;
						end;
				if (flag) then
					topologySortR(j, 0);
			end;

	end;

var
	i: integer;

begin

	generationM1;
	generationM2;
	generationM(M1, N1, A1);
	generationM(M1, N1, dominateA1);
	outputM(M1, N1, A1);
	generationM(M2, N2, A2);
	generationM(M2, N2, dominateA2);
	outputM(M2, N2, A2);
	dominationM(dominateA1, N1);
	outputM(M1, N1, dominateA1);
	dominationM(dominateA2, N2);
	outputM(M2, N2, dominateA2);
	topologySort(dominateA1, M1, N1);	
	outputWeight(M1, N1);
	topologySort(dominateA2, M2, N2);	
	outputWeight(M2, N2);

end.
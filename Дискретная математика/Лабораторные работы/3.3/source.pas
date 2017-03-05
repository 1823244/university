program dm_lab_3_3;

uses RELATIONS;

procedure generateA(var A: boolVariety);
	var
		i, j: byte;

	begin

		for i := 1 to N do
			for j := 1 to N do
				if ((((i mod 2) = 0) and ((j mod 2) = 0)) or (i = j)) then
					A[i, j] := TRUE
				else
					A[i, j] := FALSE;

	end;

var
	A: boolVariety;
	B: byteArray;

begin

	generateA(A);
	classEquivalence(A, B);
	printClassEquivalence(B);
	readln;

end.
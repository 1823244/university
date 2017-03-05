program lab_3_1;

uses RELATIONS;

procedure generateA(var a: boolVariety);
var
  i, j: byte;

begin
	for i := 1 to N do
		for j := 1 to N do
			if ((i mod 2 = 0) and (j mod 2 = 0)) then
				a[i][j] := true
			else
				a[i][j] := false;
end;

procedure generateB(var b: boolVariety);
var
	i, j: byte;

begin
	for i := 1 to N do
		for j := 1 to N do
			if (abs(i - j) < 5) then
				b[i][j] := true
			else
				b[i][j] := false;
end;

procedure generateC(var c: boolVariety);
var
	i, j: byte;

begin
	for i := 1 to N do
		for j := 1 to N do
			if ((sqr(j) mod i) = 0) then
				c[i][j] := true
			else
				c[i][j] := false;
end;

var
  A, B, C, AorB, revAorB, compA, revAorBcompC, result:boolVariety;

begin

  generateA(A);
  output_relations(A, 'A');
  readln;
  printProperties(A, 'A');
  readln;

  generateB(B);
  output_relations(B, 'B');
  readln;
  printProperties(B, 'B');
  readln;

  generateC(C);
  output_relations(C, 'C');
  readln;
  printProperties(C, 'C');
  readln;

  union(A, B, AorB);
  output_relations(AorB, '(A or B)');
  readln;

  reverse(AorB, revAorB);
  output_relations(revAorB, 'rev(A or B)');
  readln;

  composition(A, A, compA);
  output_relations(compA, 'compA');
  readln;

  composition(revAorB, C, revAorBcompC);
  output_relations(revAorBcompC, 'rev(A or B) comp C');
  readln;

  symmetric_difference(revAorBcompC, compA, result);
  output_relations(result, 'Result');
  readln;

end.

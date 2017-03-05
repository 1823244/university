program ex_3_and_6;

uses LA_S;

procedure ex3(var empty, univer: variety; var e, u: Word);

var
	A, B, C, notC, BnC, D: variety;
	KA, KB, KC, KnC, KBnC, KD: Word;

begin

	A[1] := 3; A[2] := 4; A[3] := 5; A[4] := 6; A[5] := 8; A[6] := 10; KA := 6;
	B[1] := 1; B[2] := 2; B[3] := 7; B[4] := 8; B[5] := 9; B[6] := 10; KB := 6;
	C[1] := 1; C[2] := 2; C[3] := 3; C[4] := 4; C[5] := 5; C[6] := 10; KC := 6;
	complement(B, KB, empty, e);
	complement(C, KC, notC, KnC);
	intersection(B, notC, KB, KnC, BnC, KBnC);
	symmetric_difference(A, BnC, KA, KBnC, D, KD);
	symmetric_difference(D, empty, KD, e, univer, u);

end;

procedure ex6;

var
	empty, univer, solution, iniver: variety;
	e, u, s, i: Word;

begin
	
	ex3(empty, univer, e, u);
	complement(univer, u, iniver, i);
	input_set(solution, s, 0);
	if(insertion(empty, solution, e, s) and insertion(solution, iniver, s, i)) then
		writeln('This is right solution!')
	else
		writeln('This is incorrect solution!');

end;

var
	empty, univer: variety;
	e, u, n: Word;

begin

	writeln('3 or 6?');
  	readln(n);
	
	if (n = 3) then 
		begin
			ex3(empty, univer, e, u);
			output_set(empty, e);
			output_set(univer, u);
		end
	else if (n = 6) then
		begin
			ex6;
		end;

	readln;
	readln;

end.
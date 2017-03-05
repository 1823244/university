program lab_01;

uses LA_S, LA_U, LA_B;

procedure Aa_answer;
var
	A, B, C, D1, D2, D3, D4, D: LA_U.variety;
	KA, KB, KC, KD1, KD2, KD3, KD4, KD: Word;
begin
	
	KA := 0;
	KB := 0;
	KC := 0;
	LA_U.input_set(A, KA, 1);
	LA_U.input_set(B, KB, 2);
	LA_U.input_set(C, KC, 3);

	LA_U.union(B, C, KB, KC, D1, KD1);
	LA_U.subtraction(A, D1, KA, KD1, D2, KD2);
	LA_U.subtraction(B, A, KB, KA, D1, KD1);
	LA_U.subtraction(C, A, KC, KA, D3, KD3);
	LA_U.union(D2, D1, KD2, KD1, D4, KD4);
	LA_U.union(D3, D4, KD3, KD4, D, KD);

	LA_U.output_set(D, KD);

end;

procedure Ab_answer;
var
	A, B, C, D1, D2, D3, D4, D: LA_S.variety;
	KA, KB, KC, KD1, KD2, KD3, KD4, KD: Word;
begin
	
	KA := 0;
	KB := 0;
	KC := 0;
	LA_S.input_set(A, KA, 1);
	LA_S.input_set(B, KB, 2);
	LA_S.input_set(C, KC, 3);

	LA_S.union(B, C, KB, KC, D1, KD1);
	LA_S.subtraction(A, D1, KA, KD1, D2, KD2);
	LA_S.subtraction(B, A, KB, KA, D1, KD1);
	LA_S.subtraction(C, A, KC, KA, D3, KD3);
	LA_S.union(D2, D1, KD2, KD1, D4, KD4);
	LA_S.union(D3, D4, KD3, KD4, D, KD);

	LA_S.output_set(D, KD);

end;

procedure Ac_answer;
var
	A, B, C, D1, D2, D3, D4, D: boolVariety;
begin
	
	LA_B.input_set(A, 1);
	LA_B.input_set(B, 2);
	LA_B.input_set(C, 3);

	LA_B.union(B, C, D1);
	LA_B.subtraction(A, D1, D2);
	LA_B.subtraction(B, A, D1);
	LA_B.subtraction(C, A, D3);
	LA_B.union(D2, D1, D4);
	LA_B.union(D3, D4, D);

	LA_B.output_set(D);

end;

procedure Ba_answer;
var
	A, B, C, D1, D: LA_U.variety;
	KA, KB, KC, KD1, KD: Word;
begin
	
	KA := 0;
	KB := 0;
	KC := 0;
	LA_U.input_set(A, KA, 1);
	LA_U.input_set(B, KB, 2);
	LA_U.input_set(C, KC, 3);

	LA_U.subtraction(C, A, KC, KA, D1, KD1);
	LA_U.subtraction(D1, B, KD1, KB, D, KD);

	LA_U.output_set(D, KD);

end;

procedure Bb_answer;
var
	A, B, C, D1, D: LA_S.variety;
	KA, KB, KC, KD1, KD: Word;
begin
	
	KA := 0;
	KB := 0;
	KC := 0;
	LA_S.input_set(A, KA, 1);
	LA_S.input_set(B, KB, 2);
	LA_S.input_set(C, KC, 3);

	LA_S.subtraction(C, A, KC, KA, D1, KD1);
	LA_S.subtraction(D1, B, KD1, KB, D, KD);

	LA_S.output_set(D, KD);

end;

procedure Bc_answer;
var
	A, B, C, D1, D: boolVariety;
begin
	
	LA_B.input_set(A, 1);
	LA_B.input_set(B, 2);
	LA_B.input_set(C, 3);

	LA_B.subtraction(C, A, D1);
	LA_B.subtraction(D1, B, D);

	LA_B.output_set(D);

end;

var
	exercise, subtask: char;

begin

	write('Choose your exercise (A or B): ');
		readln(exercise);

	if ((exercise = 'A') or (exercise = 'a')) then
		begin
			write('OK. Choose your subtask (A, B or C): ');
				readln(subtask);

			case (subtask) of
				'A', 'a': Aa_answer;
				'B', 'b': Ab_answer;
				'C', 'c': Ac_answer;
				else
					writeln('Subtask not found.');
			end;
		end
	else if ((exercise = 'B') or (exercise = 'b')) then
		begin
			write('OK. Choose your subtask (A, B or C): ');
				readln(subtask);

			case (subtask) of
				'A', 'a': Ba_answer;
				'B', 'b': Bb_answer;
				'C', 'c': Bc_answer;
				else
					writeln('Subtask not found.');
			end;
		end
	else
		writeln('Exercise not found.');

	readln;
	readln;
end.
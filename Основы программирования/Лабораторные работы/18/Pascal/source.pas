program lab_18;

{$MODE TP}
uses U_SORT;

const
	marks_number = 3;

type
	student = record
		name: string[50];
		marks: array [1..marks_number] of byte;
		avg: real;
	end;

	pstudent = ^student;
	students = array[1..65520 div sizeof(student)] of student;
	pstudents = ^students;

{$F+}
function lexicographical(var a, b): boolean; far;

	begin

		lexicographical := pstudent(@a)^.name < pstudent(@b)^.name;

	end;

function nonincreasing(var a, b): boolean; far;

	begin

		nonincreasing := pstudent(@a)^.avg <= pstudent(@b)^.avg;

	end;
{$F-}

function readFile(var arr: pstudents): boolean;

	var
		i, j, sum, counter: byte;
		input: text;
		c: char;

	begin

		readFile := FALSE;

		assign(input, 'input.txt');
		reset(input);

		ReturnNilIfGrowHeapFails := TRUE;

		getmem(arr, sizeof(student) * n);
		i := 1;

		if arr <> nil then begin
			read(input, n);
			while (i <= n) do
				begin
					arr^[i].name := '';
					counter := 0;

					while counter < 3 do
						begin
							read(input, c);
							arr^[i].name := arr^[i].name + c;
							if c = ' ' then
								inc(counter);
						end;

					j := 1;
					sum := 0;

					while j <= marks_number do
						begin
							read(input, arr^[i].marks[j]);
							sum := sum + arr^[i].marks[j];
							inc(j);
						end;

					arr^[i].avg := sum / marks_number;
					readln(input);
					inc(i);
				end;

			readFile := TRUE;
		end;

		close(input);

	end;

procedure writeFile(arr: pstudents);

	var
		i, j: byte;
		output: text;

	begin

		assign(output, 'output.txt');
		rewrite(output);

		for i := 1 to n do
			begin
				write(output, arr^[i].name);
				for j := 1 to marks_number do
					write(output, arr^[i].marks[j], ' ');
				writeln(output, arr^[i].avg:3);
			end;

		close(output);

	end;

var
	arr: pstudents;
	mode: char;

begin

	if (readFile(arr)) then
		begin
			writeln('[L]exicographical or [N]onincreasing?');
			readln(mode);
			case (mode) of
				'L': sort(arr, n, sizeof(student), lexicographical);
				'N': sort(arr, n, sizeof(student), nonincreasing);
				else
					writeln('Error! Input of the initial data!');
			end;

			writeFile(arr);
		end
	else
		writeln('Not enough memory!');

	readln;

end.
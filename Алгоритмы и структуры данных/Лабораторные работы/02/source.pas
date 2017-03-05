program sd_lab_2;

uses SD_STRING, CRT;

procedure Copies(st1: t_string; var st2: t_string; number: byte);
	var
		len, i, j, k: byte;

	begin

		len := Length(st1);

		if ((len * number) > N) then
			begin
				StrError := 2;
				exit;
			end
		else
			begin
				k := 1;
				i := 1;
				while (i <= number) do
					begin
						j := 1;
						while (j <= len) do
							begin
								st2[k] := st1[j];
								inc(j);
								inc(k);
							end;
						inc(i);
					end;
			end;

		st2[k] := #0;

	end;

var
	st1, st2: t_string;

begin

	WriteToStr(st1, 'This is the first string!');
	CheckErrors;
	WriteToStr(st2, 'This is the second string!');
	CheckErrors;

	textcolor(White);
	write('First string: < ');
	textcolor(Green);
	OutputStr(st1);
	textcolor(White);
	writeln(' >');

	write('Second string: < ');
	textcolor(Green);
	OutputStr(st2);
	textcolor(White);
	writeln(' >');

	Copies('Deleted! ', st1, 10);
	CheckErrors;

	write('First string, after Copies(10): < ');
	textcolor(Green);
	OutputStr(st1);
	textcolor(White);
	writeln('>');
	readln;

	Copies('Deleted! ', st2, 100);
	CheckErrors;

	write('First string, after Copies(100): < ');
	textcolor(Green);
	OutputStr(st1);
	textcolor(White);
	writeln('>');
	readln;

end.
program lab_9_b;

var
	f: file of Integer;
	filename: String;
	a: Integer;
begin

	writeln('�� �ணࠬ�� ���⠥� ᮤ�ন��� ⨯���஢������ 䠩�� �� �࠭.');
  	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('������ ��� ⨯���஢������ 䠩�� (����� ��ப� ࠢ��ᨫ쭠 "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	reset(f);

	if EOF(f) then
		writeln('����� 䠩� ����!')
	else
		begin 
			writeln('����ন��� �⮣� 䠩��: ');

			while not(EOF(f)) do 
				begin
					read(f, a);
					write(a, ' ');
				end;
		end;
	close(f);
	
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  	readln;

end.
end.
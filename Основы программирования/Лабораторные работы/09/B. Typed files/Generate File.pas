program lab_9_b;

const
	n = 10;

var
	f: file of Integer;
	filename: String;
	i, j: Integer;
begin

	writeln('�� �ணࠬ�� �������� ⨯���஢���� 䠩�,');
	writeln('�������� ��� ��砩�묨 �᫠�� �� ��������� �� 0 �� ', high(Integer));
	writeln('��᫥����⥫쭮��ﬨ �� ����� ����⮢.');
	writeln;
	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('������ ��� ������ 䠩�� (����� ��ப� ࠢ��ᨫ쭠 "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	rewrite(f);

	randomize;
	for i := 1 to random(50) do 
		for j := 1 to n do
			write(f, random(high(Integer)));
	
	close(f);
	
	writeln('������஢���� 䠩� �ᯥ譮 ᣥ���஢��!');
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  	readln;
end.
program lab_9_b;

const 
	n = 10;

var
	f: file of Integer;
	a, i, j, max: integer;
	filename: string;
begin

	writeln('�����:');
	writeln('��� 䠩�, ��������⠬� ���ண� ����� ��᫥����⥫쭮��': 64);
	writeln('楫�� �ᥫ ����� n (n - ����⠭�).': 41);
	writeln('������ ��᫥����⥫쭮��� � 䠩�� �������� ���ᨬ���� 童���.': 68);
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('������ ��� ⨯���஢������ 䠩�� (����� ��ப� ࠢ��ᨫ쭠 "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	reset(f);
	
	for i := 0 to ((filesize(f) div n) - 1) do  
		begin
			read(f, max);
			for j := 2 to n do
				begin
					read(f, a);
					if a > max then
						max := a;		
				end;
			seek(f, i);
			write(f, max);
			seek(f, i*n + n);
		end;
	seek(f, i + 1);

	truncate(f);
	close(f);

	writeln('������ �ணࠬ�� �ᯥ譮 �����襭�!');
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  	readln;
end.
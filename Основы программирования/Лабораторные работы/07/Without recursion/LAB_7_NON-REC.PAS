program lab_7_without_recursion;

procedure ReadArray(var s: string);

var
  c: char;

begin

  read(c);
  while c <> '.' do
    begin
      if c in ['0'..'9'] then
        s := s + c;
      read(c);
    end;

end;

procedure Reverse(s: string);

var
  i: byte;

begin

  for i := length(s) downto 1 do
    write(s[i]);

end;

var
  s: string;

begin

  writeln('�������: ');
  writeln('� ���������� �������� ��᫥����⥫쭮��� ᨬ�����. �ਧ��� ���� ����� �窠.': 78);
  writeln('�뢥�� ���� �� ��������� ��᫥����⥫쭮�� ᭠砫� � ���浪� �����,': 73);
  writeln('� ��⥬ � ���⭮� ���浪�.': 30);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('������ ��᫥����⥫쭮���: ');
  s := '';
  ReadArray(s);
  writeln;

  if s = '' then
    write('��� �ᥫ � �⮩ ��᫥����⥫쭮��!')
  else
    begin
	    writeln(s, ' - �᫠ � ��אַ� ���浪�.');
      Reverse(s);
      write(' - �᫠ � ���⭮� ���浪�.');
    end;
    
  writeln;
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
  readln;
end.
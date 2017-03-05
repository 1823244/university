program lab_7_rec;

procedure Rec(c: char; var flag: Boolean);

begin
  
  read(c);

  if c <> '.' then
    begin
      if (c in ['0'..'9']) then 
        begin
          flag := true;
          write(c);
          Rec(c, flag);
          write(c);
        end
      else 
          Rec(c, flag);
    end
  else
    begin
      if flag then
        write(' - �᫠ � ��אַ� ���浪�.');
      writeln;
    end;
end;

var
  c: char;
  flag: boolean;

begin

  writeln('�������: ');
  writeln('� ���������� �������� ��᫥����⥫쭮��� ᨬ�����. �ਧ��� ���� ����� �窠.': 78);
  writeln('�뢥�� ���� �� ��������� ��᫥����⥫쭮�� ᭠砫� � ���浪� �����,': 73);
  writeln('� ��⥬ � ���⭮� ���浪�.': 30);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln; 

  flag := false;
  write('������ ��᫥����⥫쭮���: ');
  Rec(c, flag);
  
  if flag then
    writeln(' - �᫠ � ���⭮� ���浪�.')
  else
    writeln('��� �ᥫ � �⮩ ��᫥����⥫쭮��!');
  
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
  readln;
end.
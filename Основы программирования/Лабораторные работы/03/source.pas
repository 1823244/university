program lab_3;

var
	n: Longint;
  i, j: Integer;
	simple: Boolean;
	
begin

  writeln('�������:');
  writeln('���� �� ����� ����⥫� ������� ����ࠫ쭮�� �᫠:':57);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('������ ����ࠫ쭮� �᫮: ');
  readln(n);

  write('����� ����⥫� ������� ����ࠫ쭮�� �᫠: 1');

  for i := 2 to n do
    if (n mod i = 0) then 
      begin
        j := 2;
        simple := true;
        while j < i do 
          if i mod j = 0 then 
            begin
              j := i;
              simple := false;
		        end
          else
             inc(j);
          
        if simple then
          write(', ', i);
      end;

  writeln;
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
end.
program lab_4;

const
  n = 10;

var
  Z, needful: array [1..n] of integer;
  i, j, k, num, imin, min: integer;

begin

  writeln('�������:');
  writeln('���� ��᫥����⥫쭮��� 楫�� �ᥫ.': 39);
  writeln('�뢥�� 㯮�冷祭��� �� �����⠭�� ��᫥����⥫쭮���,': 59);
  writeln('�������  �� �ᥫ ������ ��᫥����⥫쭮��, ����� �� ������ �����,': 77);
  writeln('� ���������� � ��� ⮫쪮 ���� ࠧ.': 41);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('������ ��᫥����⥫쭮��� �� ', n, ' 楫�� �ᥫ: ');
  for i := 1 to n do
    read(Z[i]);

  j := 0;

  for i := 1 to n do
    begin
      num := 0;
      for k := 1 to n do
        if Z[i] = Z[k] then
          inc(num);
      if ((num = 1) and (i mod 2 <> 0)) then
        begin
          inc(j);
          needful[j] := Z[i];
        end;
    end;

  writeln;
  write('������� ࠡ��� �ணࠬ��: ');

  for i := 1 to j do
    begin
      imin := i;
      min := needful[i];

      for k := i + 1 to j do
        if (needful[k] < min) then
          begin
            imin := k;
            min := needful[k];
          end;
      
      needful[imin] := needful[i];
      needful[i] := min;
      write(min, ' ');
    end;

  writeln;
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
  readln;
end.
program lab_8_b;

type
  char_set = set of char;

procedure Read_Set(s: string; l: byte; var i: byte; var chs: char_set; var w: string);

begin

  chs := [];
  w := '';

  while (s[i] <= ' ') and (i <= l) do
    inc(i);

  while (s[i] > ' ') and (i <= l) do
    begin
      include(chs, s[i]);
      w := w + s[i];
      inc(i);
    end;

end;

procedure Unique_Set(s: string; l: byte; var i: byte; var chs: char_set; var w:string);

begin

  chs := [];
  w := '';

  while (s[i] <= ' ') and (i <= l) do
    inc(i);

  while (s[i] > ' ') and (i <= l) do
    begin
      if s[i] in chs then
        exclude(chs, s[i])
      else
        include(chs, s[i]);
      w := w + s[i];
      inc(i);
    end;

end;

var
  s1, s2, w1, w2: string;
  chs1, chs2: char_set;
  i, j, l1, l2: byte;
  flag: boolean;

begin
  writeln('�������:');
  writeln('���� ��� ��ப� S1 � S2.': 27);
  writeln('��� ������� ᫮�� W ��ப� S1 �뢥�� ᫮�� ��ப� S2,': 57);
  writeln('������ ������������� �㪢� ���ண� ���� � ᫮�� W.': 56);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  
  write('������ ��ப� S1: ');
  readln(s1);
  write('������ ��ப� S2: ');
  readln(s2);

  l1 := length(s1);
  l2 := length(s2);

  i := 1;

  writeln;
  writeln('�������:');
  writeln;
  
  while i < l1 do
    begin
      Read_Set(s1, l1, i, chs1, w1);
      j := 1;
      write(w1: length(w1)+3, ' - ');
      flag := false;
      while j < l2 do
        begin
          Unique_Set(s2, l2, j, chs2, w2);
          if chs2 <= chs1 then
            begin
              write(w2);
              flag := true;
              j := l2;
            end;
        end;
      if not(flag) then
        write('��� ᮮ⢥�����饣� ᫮��.');

      writeln;
    end;

  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
end.
program lab_6;

function Read_Word(s: string; l: integer; var i: integer): string;

var
  w: string;

begin

  w := '';

  while (i <= l) and (s[i] <= ' ') do
    inc(i);

  while (i <= l) and (s[i] > ' ') do
    begin
      w := w + s[i];
      inc(i);
    end;

  read_word := w;

end;

function Check(w, s: string): boolean;

var
  i, j: integer;
  flag: Boolean;

begin

  flag := false;
  Check := flag;

  i := 1;

  if length(s) >= length(w) then
    while i <= length(s) - length(w) + 1 do
      begin
        if (s[i] = w[1]) then
          begin
            flag := true;
            for j := 2 to length(w) do
              if (s[i+j-1] <> w[j]) then
                flag := false;
            if flag then
              begin
                  i := length(s) - length(w) + 1;
                  Check := flag;
              end;
          end;
        inc(i);
      end;
end;

var
  s1, s2, last, now: string;
  l, i: integer;
  flag: Boolean;
begin

  writeln('Задание:');
  writeln('Даны две строки S1 и S2.': 27);
  writeln('Пусть W - первое из слов строки S1, которое есть и в строке S2.': 66);
  writeln('Найти слово, предшествующее первому вхождению W в S1.': 56);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('Введите строку S1: ');
  readln(s1);

  write('Введите строку S2: ');
  readln(s2);

  i := 1;
  l := length(s1);

  last := Read_Word(s1, l, i);

  flag := false;

  while (i <= l) and not(flag) do
    begin
      now := Read_Word(s1, l, i);
      if check(now, s2) then
        begin
          i := l + 1;
          writeln('Искомое слово: ', last);
          flag := true;
        end
      else
        last := now;
    end;
  
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
end.    
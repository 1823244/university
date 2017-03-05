program lab_4;

const
  n = 10;

var
  Z, needful: array [1..n] of integer;
  i, j, k, num, imin, min: integer;

begin

  writeln('Задание:');
  writeln('Дана последовательность целых чисел.': 39);
  writeln('Вывести упорядоченную по возрастанию последовательность,': 59);
  writeln('состоящую  из чисел данной последовательности, стоящих на нечетных местах,': 77);
  writeln('и встречающихся в ней только один раз.': 41);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('Введите последовательность из ', n, ' целых чисел: ');
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
  write('Результат работы программы: ');

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
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
  readln;
end.
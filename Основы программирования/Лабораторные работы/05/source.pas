program lab_5;

const
  n = 2;
  m = 6;

type
  matrix = array [1..n, 1..m] of integer;

procedure Reader(var massive: matrix);

var
  i, j: integer;
begin

  write('Введите элементы матрицы (', n, 'x', m, '): ');
  for i := 1 to n do
    for j := 1 to m do
      read(massive[i, j]);

end;

function Check(massive: matrix; x, y, z: integer): boolean;

var
  i: integer;
  flag: boolean;
begin

  flag := false;
  i := 1;
  while ((i < n+1) and not(flag)) do
    begin
      if (massive[x, y] = massive[i, z]) then
        flag := true;
      inc(i);
    end;

  Check := flag;

end;

function Counter(massive: matrix): integer;

var
  i, j, k, count: integer;
  memory: array [1..m] of boolean;
  flag, checker: boolean;
begin

  for i := 1 to m do
    memory[i] := false;

  count := 0;

  for i := 1 to m do
    begin
      flag := false;
      for k := i+1 to m do
        if not(memory[k]) then
          begin
            j := 1;
            checker := true;
            while ((j < n+1) and checker) do
              begin
                if (not(Check(massive, j, i, k))) or (not(Check(massive, j, k, i))) then
                  checker := false;
                inc(j);
              end;
            if checker then
              begin
                memory[k] := true;
                flag := true;
              end;
          end;
      if flag then
        inc(count);
    end;

    Counter := count;
end;

var

  square: matrix;

begin

  writeln('Задание:');
  writeln('Определить количество классов эквивалетных столбцов прямоугольной матрицы.': 77);
  writeln('Столбцы считать эквивалентными, если равны множества их элементов.': 69);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  Reader(square);
  writeln;
  writeln('Классов эквивалентных столбцов в этой матрице: ', Counter(square));

  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
  readln;
end.
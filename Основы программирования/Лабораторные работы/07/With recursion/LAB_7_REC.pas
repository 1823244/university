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
        write(' - числа в прямом порядке.');
      writeln;
    end;
end;

var
  c: char;
  flag: boolean;

begin

  writeln('Задание: ');
  writeln('С клавиатуры вводится последовательность символов. Признак конца ввода точка.': 78);
  writeln('Вывести цифры из введенной последовательности сначала в порядке ввода,': 73);
  writeln('а затем в обратном порядке.': 30);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln; 

  flag := false;
  write('Введите последовательность: ');
  Rec(c, flag);
  
  if flag then
    writeln(' - числа в обратном порядке.')
  else
    writeln('Нет чисел в этой последовательности!');
  
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
  readln;
end.
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

  writeln('Задание: ');
  writeln('С клавиатуры вводится последовательность символов. Признак конца ввода точка.': 78);
  writeln('Вывести цифры из введенной последовательности сначала в порядке ввода,': 73);
  writeln('а затем в обратном порядке.': 30);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('Введите последовательность: ');
  s := '';
  ReadArray(s);
  writeln;

  if s = '' then
    write('Нет чисел в этой последовательности!')
  else
    begin
	    writeln(s, ' - числа в прямом порядке.');
      Reverse(s);
      write(' - числа в обратном порядке.');
    end;
    
  writeln;
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
  readln;
end.
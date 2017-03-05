program lab_3;

var
	n: Longint;
  i, j: Integer;
	simple: Boolean;
	
begin

  writeln('Задание:');
  writeln('Найти все простые делители данного натурального числа:':57);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('Введите натуральное число: ');
  readln(n);

  write('Простые делители данного натурального числа: 1');

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
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
end.
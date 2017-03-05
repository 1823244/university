program lab_9_b;

const 
	n = 10;

var
	f: file of Integer;
	a, i, j, max: integer;
	filename: string;
begin

	writeln('Задача:');
	writeln('Дан файл, компонентами которого являются последовательности': 64);
	writeln('целых чисел длины n (n - константа).': 41);
	writeln('Каждую последовательность в файле заменить максимальным членом.': 68);
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('Введите имя типизированного файла (пустая строка равносильна "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	reset(f);
	
	for i := 0 to ((filesize(f) div n) - 1) do  
		begin
			read(f, max);
			for j := 2 to n do
				begin
					read(f, a);
					if a > max then
						max := a;		
				end;
			seek(f, i);
			write(f, max);
			seek(f, i*n + n);
		end;
	seek(f, i + 1);

	truncate(f);
	close(f);

	writeln('Работы программы успешно завершена!');
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('Нажмите [Enter] для завершения работы программы!');
  	readln;
end.
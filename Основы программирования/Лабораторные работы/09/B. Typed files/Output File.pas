program lab_9_b;

var
	f: file of Integer;
	filename: String;
	a: Integer;
begin

	writeln('Эта программа печатает содержимое типизированного файла на экран.');
  	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('Введите имя типизированного файла (пустая строка равносильна "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	reset(f);

	if EOF(f) then
		writeln('Данный файл пуст!')
	else
		begin 
			writeln('Содержимое этого файла: ');

			while not(EOF(f)) do 
				begin
					read(f, a);
					write(a, ' ');
				end;
		end;
	close(f);
	
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('Нажмите [Enter] для завершения работы программы!');
  	readln;

end.
end.
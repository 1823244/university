program lab_9_b;

const
	n = 10;

var
	f: file of Integer;
	filename: String;
	i, j: Integer;
begin

	writeln('Эта программа генерирует типизированный файл,');
	writeln('заполняя его случайными числами из диапазона от 0 до ', high(Integer));
	writeln('последовательностями из десяти элементов.');
	writeln;
	writeln('--------------------------------------------------------------------------------');
  	writeln;
	
	write('Введите имя нового файла (пустая строка равносильна "fread"): ');
	readln(filename);

	if filename = '' then
		filename := 'fread';

	assign(f, filename);
	rewrite(f);

	randomize;
	for i := 1 to random(50) do 
		for j := 1 to n do
			write(f, random(high(Integer)));
	
	close(f);
	
	writeln('Типизированный файл успешно сгенерирован!');
	writeln;
  	writeln('--------------------------------------------------------------------------------');
  	writeln;
  	writeln('Нажмите [Enter] для завершения работы программы!');
  	readln;
end.
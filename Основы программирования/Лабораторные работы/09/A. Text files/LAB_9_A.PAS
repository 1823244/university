program lab_9_a;

var
  fread, fwrite: text;
  tmp, tmp2: char;
  freadname: string;
  
begin

  writeln('Задание:');
  writeln('Дан текстовый файл.': 22);
  writeln('Сохранить в файле только те символы, за которыми следует буква "а".': 70);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('Введите имя входного файла (пустая строка равносильна "fread.txt"): ');
  readln(freadname);
  if freadname = '' then
    freadname := 'fread.txt';
  assign(fread, freadname);

  assign(fwrite, 'tmp.txt');

  reset(fread);
  rewrite(fwrite);

  writeln;
  
  if eof(fread) then
    writeln('Входной файл пуст!')
  else
    begin
      read(fread, tmp);
      while not(eof(fread)) do
        begin
          read(fread, tmp2);
          if (tmp2 = 'a') or (tmp2 = 'A') then
            write(fwrite, tmp);
          tmp := tmp2;
        end;
      writeln('Исходный файл перезаписан.');
    end;

  close(fread);
  close(fwrite);
  erase(fread);
  rename(fwrite, freadname);

  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
end.
program lab_9_a;

var
  fread, fwrite: text;
  tmp, tmp2: char;
  freadname: string;
  
begin

  writeln('�������:');
  writeln('��� ⥪�⮢� 䠩�.': 22);
  writeln('���࠭��� � 䠩�� ⮫쪮 � ᨬ����, �� ����묨 ᫥��� �㪢� "�".': 70);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  write('������ ��� �室���� 䠩�� (����� ��ப� ࠢ��ᨫ쭠 "fread.txt"): ');
  readln(freadname);
  if freadname = '' then
    freadname := 'fread.txt';
  assign(fread, freadname);

  assign(fwrite, 'tmp.txt');

  reset(fread);
  rewrite(fwrite);

  writeln;
  
  if eof(fread) then
    writeln('�室��� 䠩� ����!')
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
      writeln('��室�� 䠩� ��१���ᠭ.');
    end;

  close(fread);
  close(fwrite);
  erase(fread);
  rename(fwrite, freadname);

  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('������ [Enter] ��� �����襭�� ࠡ��� �ணࠬ��!');
  readln;
end.
program lab_8_a;

const
  dayweek: array [0..6] of string[11] = ('понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота', 'воскресенье');

type
  date = record
    year: word;
    month: 1..12;
    day: 1..31;
    week: 0..6;
  end;

function LeapYear(year: integer): boolean;
begin
  if(((year mod 400) = 0) or (((year mod 4) = 0) and ((year mod 100) <> 0))) then
    LeapYear := true
  else
    LeapYear := false;
end;

function CheckDayOfMonth(day, month, year: integer): boolean;
begin
  if ((day > 30) and (month in [2, 4, 6, 9, 11])) or
     ((day > 29) and (month = 2) or
     ((day > 28) and (month = 2) and (not LeapYear(year)))) then
       CheckDayOfMonth := false
  else
    CheckDayOfMonth := true;
end;

procedure EnterDate(var d: date);

var
  n: integer;
  dayOk: boolean;

begin
  write('Год: ': 8);
  readln(d.year);

  repeat
    write('Месяц: ': 10);
    readln(n);
    if (n < 1) or (n > 12) then
      writeln('Ошибка! Введенный месяц не в диапазоне от 1 до 12!': 53);
  until (n > 0) and (n < 13);
  d.month := n;

  repeat
    dayOk := true;
    write('День: ': 9);
    readln(n);
    if (n < 1) or (n > 31) or (not CheckDayOfMonth(n, d.month, d.year)) then
      begin
        writeln('Ошибка! День не может быть таким!': 40);
        dayOk := false;
      end;
  until dayOk;
  d.day := n;

end;

function DaysFromZero(d, m, y: integer): longint;

var
  dofy: word;
  i: byte;

begin

   dofy := 0;

   for i := 1 to m-1 do
     case i of
       4, 6, 9, 11: inc(dofy, 30);
       2: inc(dofy, 28)
       else
        inc(dofy, 31);
     end;
   
   if LeapYear(y) and (m > 2) then
     inc(dofy);

  DaysFromZero := d + dofy + ((y - 1) * 365) + ((y - 1) div 4) + ((y - 1) div 400) - ((y - 1) div 100);

end;

function FindDay(ctrlDate, curDate: date): byte;

var
  n: longint;
  week: shortint;

begin

  n := (DaysFromZero(curDate.day, curDate.month, curDate.year) -
        DaysFromZero(ctrlDate.day, ctrlDate.month, ctrlDate.year));

  week := ctrlDate.week + (n mod 7) + 7;

  FindDay := abs(week) mod 7;

end;

var
  ctrlDate, curDate: date;
  week: string;
  i: integer;

begin

  writeln('Задание: ');
  writeln('Определить день недели для данной даты, если день недели какой-либо даты.': 76);
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;

  writeln('Введите дату, день недели которой необходимо узнать: ');
  EnterDate(curDate);
  writeln;
  writeln('Введите дату с известным днем недели: ');
  EnterDate(ctrlDate);
  write('День недели: ': 16);
  readln(week);

  for i := 0 to 6 do
    if week = dayweek[i] then
      ctrlDate.week := i;
  writeln;
  write(curDate.day, '.');
  if curDate.month < 10 then 
    write('0');
  writeln(curDate.month, '.', curDate.year, ' это ', dayweek[FindDay(ctrlDate, curDate)]);
  
  writeln;
  writeln('--------------------------------------------------------------------------------');
  writeln;
  writeln('Нажмите [Enter] для завершения работы программы!');
  readln;
end.
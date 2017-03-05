program exs_7_and_8;

uses LA_S;

procedure ex7;

var
    left, right, flag, a, b, c: boolean;
    i: byte;
begin

  writeln('A':3, 'B':3, 'C':3, '<':3, '>':3, '=':3);
  a := true;
  b := true;
  c := true;
  flag := true;
  for i := 0 to 7 do
    begin
      c := not c;
      if(i mod 2 = 0) then b := not b;
      if(i mod 4 = 0) then a := not a;
      left := (a or b) and not c;
      right := not(not a or c) or not(not b or c);
      write(Byte(a):3, Byte(b):3, Byte(c):3, Byte(left):3, Byte(right):3);
      if left = right then
        writeln('+': 3)
      else
        begin
          writeln('-': 3);
          flag := false;
        end;
    end;
  writeln;
  if flag then
    writeln('Left = Right')
  else
    writeln('Left <> Right');

end;

procedure ex8;
var
  a, notA, b, notB, c, notC, d, d1, d2, d3, d4: variety;
  KNA, KNB, KNC, KD, KD1, KD2, KD3, KD4: Word;
begin

  a[1] := 1;
  a[2] := 3;
  a[3] := 5;
  a[4] := 7;
  b[1] := 2;
  b[2] := 3;
  b[3] := 6;
  b[4] := 7;
  c[1] := 4;
  c[2] := 5;
  c[3] := 6;
  c[4] := 7;

  complement(c, 4, notC, KNC);
  intersection(a, notC, 4, KNC, d1, KD1);
  complement(d1, KD1, d2, KD2);
  intersection(b, notC, 4, KNC, d1, KD1);
  complement(d1, KD1, d3, KD3);
  intersection(d2, d3, KD2, KD3, d, KD);
  complement(d, KD, d3, KD3);

  complement(a, 4, notA, KNA);
  complement(b, 4, notB, KNB);
  union(notA, c, KNA, 4, d1, KD1);
  complement(d1, KD1, d, KD);
  union(notB, c, KNB, 4, d1, KD1);
  complement(d1, KD1, d2, KD2);
  union(d, d2, KD, KD2, d4, KD4);


  writeln(equality(d3, d4, KD3, KD4));

end;

var
   n: byte;

begin

  writeln('7 or 8?');
  readln(n);
  if n = 7 then
    ex7
  else if n = 8 then
    ex8
  else
    writeln('Error');

  readln;

end.
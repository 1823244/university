function str2real(s: String): Real;
var
	i, j, k: Integer;
	r1, r2, tmp: Real;
begin
	i := 1;
	r1 := 0;
	r2 := 0;
	j := 1;
	if s[i] = '-' then j := 2;
	while(s[i] <> '.') do 
		inc(i);
	
	for j := j to i - 1 do
		r1 := r1 * 10 + (ord(s[j]) - ord('0'));

	for j := i + 1 to length(s) do
		begin
			tmp := (ord(s[j]) - ord('0'));
			for k := 1 to j - i do
				tmp := tmp / 10;
			r2 := r2 + tmp;
		end;	

	str2real := r1 + r2;
end;

function int2str(i: Integer): String;
var
	s: string;
	tmp_s: Char;
	tmp, l: Integer;
begin
	s := '';
	while(i > 0) do
		begin
			tmp := i mod 10;
			i := i div 10;
			s := s + chr(tmp + ord('0'));
		end;
	l := length(s);
	for i := 1 to (l div 2) do
		begin
			tmp_s := s[i];
			s[i] := s[l + i - 1];
			s[l + i - 1] := tmp_s;
		end;

	int2str := s;
end;

var
	s: String;
begin
	readln(s);
	writeln(s, ' ', int2str(trunc(sqr(str2real(s)))));
	readln;
end.
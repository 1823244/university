program lab_10;

const
	n = 2;
	m = 3;

type 
	t_max_vect = array [1..65520 div sizeof(Integer)] of Integer;

procedure input_matrix(var a; n: Word);

var
	i: Integer;

begin
	
	for i := 1 to n*n do 
		read(t_max_vect(a)[i]);

end;

function IsReverse(var a, b; n: Word): Boolean;

var
	i, j, k: Word;
	sum, sum2: Integer;
	flag: Boolean;
begin
	
	flag := true;
	i := 1;
	j := 1;	
	while (i <= n) and flag do
		begin
			while (j <= n) and flag do
				begin
					sum := 0;
					sum2 := 0;
					for k := 1 to n do
						begin
							sum := sum + t_max_vect(a)[(i - 1)*n + k] * t_max_vect(b)[(k - 1)*n + j];
							sum2 := sum2 + t_max_vect(b)[(i - 1)*n + k] * t_max_vect(a)[(k - 1)*n + j];
						end;
					if (((sum <> 1) and (((i*j div n) + 1 = (i*j mod n)) or (i*j = n*n))) or 
						((sum <> 0) and ((i*j div n) + 1 <> (i*j mod n)))) or (sum <> sum2) then
							flag := false;
					inc(j);
				end;
			inc(i);
		end;
				
	IsReverse := flag;
end;

var
	a, b: array [1..n, 1..n] of Integer;
	c, d: array [1..m, 1..m] of Integer;

begin
	
	input_matrix(a, n);
	input_matrix(b, n);
	input_matrix(c, m);
	input_matrix(d, m);
	if (IsReverse(a, b, n) or IsReverse(c, d, m)) then
		write('Yes!')
	else
		write('No!');
end.
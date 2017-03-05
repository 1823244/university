program dm_lab_4_1;

const 
	N = 7;


type 
	booleanMatrix = array [1..N, 1..N] of boolean;
	vertices = array [1..100] of byte;
	byteMatrix = array [1..N, 1..N] of byte;
	setOfByte = set of byte;

procedure generateG1(var g: booleanMatrix);
	var
		i, j: byte;

	begin

		for i := 1 to N do
			for j := 1 to N do
				g[i, j] := FALSE;

		g[1, 2] := TRUE;
		g[1, 7] := TRUE;
		g[2, 1] := TRUE;
		g[2, 6] := TRUE;
		g[2, 7] := TRUE;
		g[3, 4] := TRUE;
		g[3, 5] := TRUE;
		g[3, 6] := TRUE;
		g[4, 3] := TRUE;
		g[4, 5] := TRUE;
		g[5, 3] := TRUE;
		g[5, 4] := TRUE;
		g[5, 6] := TRUE;
		g[6, 2] := TRUE;
		g[6, 3] := TRUE;
		g[6, 5] := TRUE;
		g[6, 7] := TRUE;
		g[7, 1] := TRUE;
		g[7, 2] := TRUE;
		g[7, 6] := TRUE;

	end;

procedure generateG2(var g: booleanMatrix);
	var
		i, j: byte;

	begin

		for i := 1 to N do
			for j := 1 to N do
				g[i, j] := FALSE;

		g[1, 2] := TRUE;
		g[1, 4] := TRUE;
		g[1, 6] := TRUE;
		g[1, 7] := TRUE;
		g[2, 1] := TRUE;
		g[2, 3] := TRUE;
		g[2, 4] := TRUE;
		g[3, 2] := TRUE;
		g[3, 4] := TRUE;
		g[3, 5] := TRUE;
		g[3, 6] := TRUE;
		g[4, 1] := TRUE;
		g[4, 2] := TRUE;
		g[4, 3] := TRUE;
		g[5, 3] := TRUE;
		g[5, 6] := TRUE;
		g[6, 1] := TRUE;
		g[6, 3] := TRUE;
		g[6, 5] := TRUE;
		g[6, 7] := TRUE;
		g[7, 1] := TRUE;
		g[7, 6] := TRUE;

	end;

procedure inputVertices(var values: vertices; number: byte);
	var
		i: byte;

	begin

		for i := 1 to number do
			begin
				write('':5, i, ': ');
				readln(values[i]);
			end;

	end;

function whatIsIt(a: booleanMatrix; values: vertices; number: byte): string;
	var
		route, path, simplePath, loop, simpleLoop: boolean;
		memory: byteMatrix;
		i, j: byte; 

	begin

		route := TRUE;
		path := TRUE;
		simplePath := TRUE;
		loop := FALSE;
		simpleLoop := FALSE;

		for i := 1 to N do
			memory[1, i] := 0;

		for i := 1 to number - 1 do
			if (a[values[i], values[i + 1]]) then
				begin
					if (path) then
						begin
							if (memory[1, values[i]] <> 0) then
								begin
									simplePath := FALSE;	
									for j := 2 to (memory[1, values[i]] + 1) do
										if (values[memory[j, values[i]] + 1] = values[i + 1]) then
											begin
												path := FALSE;
												break;
											end;
								end;
							inc(memory[1, values[i]]);
							memory[memory[1, values[i]] + 1, values[i]] := i;
						end;
				end
			else
				begin
					path := FALSE;
					simplePath := FALSE;
					route := FALSE;
					break;
				end;

		if (path and (values[1] = values[number])) then
			begin
				if (simplePath) then
					simpleLoop := TRUE;

				loop := TRUE;
			end;

		if (simpleLoop) then
			whatIsIt := 'simple loop'
		else if (loop) then
			whatIsIt := 'loop'
		else if (simplePath) then
			whatIsIt := 'simple path'
		else if (path) then
			whatIsIt := 'path'
		else if (route) then
			whatIsIt := 'route'
		else 
			whatIsIt := 'not route'; 

	end;

procedure printElement(W: vertices; number: byte);
	var
		i: byte;

	begin

		for i := 1 to number do
			write(W[i], ' ');

		writeln;

	end;

procedure allRoutesLFromV(a: booleanMatrix; l: byte; v: byte);
	var
		W: vertices;

	procedure findRoute(val: byte; i: byte);
		var
			j: byte;

		begin

			for j := 1 to N do
				if (a[val, j]) then
					begin
						W[i] := j;
						if (i = l + 1) then
							printElement(W, i)
						else
							findRoute(j, i + 1);
					end;

		end;

	begin

		W[1] := v;
		writeln('Routes: ');
		findRoute(v, 2);

	end;

procedure allRoutesL(a: booleanMatrix; l: byte);
	var
		r: byteMatrix;
		v: byte;

	procedure countRoutes(val: byte; i: byte);
		var
			j: byte;

		begin

			for j := 1 to N do
				if (a[val, j]) then
					begin
						if (i = l + 1) then
							inc(r[v, j])
						else
							countRoutes(j, i + 1);
					end;

		end;

	var
		i, j: byte;

	begin

		for i := 1 to N do
			for j := 1 to N do
				r[i, j] := 0;

		for v := 1 to N do
			countRoutes(v, 2);

		for i := 1 to N do
			for j := 1 to N do
				writeln('From ', i, ' to ', j, ' ', r[i, j], ' routes.');

	end;

procedure allRoutesLFromVToX(a: booleanMatrix; l: byte; v: byte; x: byte);
	var
		W: vertices;

	procedure findRoute(val: byte; i: byte);
		var
			j: byte;

		begin

			for j := 1 to N do
				if (a[val, j]) then
					begin
						W[i] := j;
						if (i = l + 1) then
							begin
								if (j = x) then 
									printElement(W, i);
							end
						else
							findRoute(j, i + 1);
					end;

		end;

	begin

		W[1] := v;
		writeln('Routes: ');
		findRoute(v, 2);

	end;

procedure allMaxPathFromV(a: booleanMatrix; v: byte);
	var
		W: vertices;

	procedure findPath(val: byte; setV: setOfByte; i: byte);
		var
			j, k: byte;
			checker: boolean;

		begin

			for j := 1 to N do
				if ((a[val, j]) and not (j in setV)) then
					begin
						W[i] := j;
						include(setV, j);
						checker := true;
						for k := 1 to N do
							begin
								if ((a[j, k]) and not (k in setV)) then
									begin
										checker := FALSE;
										break;
									end;
							end;

						if (checker) then
							printElement(W, i)
						else
							findPath(j, setV, i + 1);

						exclude(setV, j);
					end;

		end;

	var
		setV: setOfByte;

	begin

		W[1] := v;
		setV := [v];
		writeln('Pathes: ');
		findPath(v, setV, 2);

	end;

var
	values: vertices;
	number: byte;
	G1, G2: booleanMatrix;

begin

	generateG1(G1);
	generateG2(G2);

	(* task 3 *)
	
	write('Input number of vertices: ');
	readln(number);
	inputVertices(values, number);
	writeln(whatIsIt(G1, values, number));
	

	(* task 4*)
	{
	allRoutesLFromV(G1, 5, 7);
	}

	(* task 5 *)
	{
	allRoutesL(G1, 5);
	}

	(* task 6 *)
	{
	allRoutesLFromVToX(G1, 5, 1, 7);
	}

	(* task 7 *)
	{
	allMaxPathFromV(G1, 1);
	}
	
	readln;

end.
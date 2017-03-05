program lab_4_2;

uses crt, baseunix, unix, termio, linux, sysutils, dateutils, unixutil;

const 
	N = 10;
	V = 8;
	M = 20;

type 
	booleanMatrix = array [1..N, 1..N] of boolean;
	vertices = array [1..100] of byte;
	intV = array [1..V] of byte;
	setOfByte = set of byte;

var
	eulerian, hamiltonian: boolean;
	f: text;

function getTimeInSecs: real;
	var
		time: real;
		
	begin

		reset(f);
		read(f, time);
		getTimeInSecs := time;
	
	end;

procedure generateGraph(var A: booleanMatrix);
	var
		counter: integer;
		i, j, x, y: byte;

	begin

		counter := -1;

		for i := 1 to V do
			for j := 1 to V do
				A[i, j] := FALSE;
		if (M >= ((V * V - V) div 2)) then
			begin	
				for i := 1 to V do
					for j := i + 1 to V do
						begin
							A[i, j] := TRUE;
							A[j, i] := TRUE;
						end;
			end
		else
			begin
				while (counter < M) do
					begin
						x := random(V) + 1;
						y := random(V) + 1;

						if ((not A[x, y]) and (x <> y)) then
							begin
								A[x, y] := TRUE;
								A[y, x] := TRUE;
								inc(counter);
							end;
					end;
			end;

	end;

procedure isHamiltonian(A: booleanMatrix);

	procedure findHamiltonian(val: byte; setV: setOfByte; index: byte);
		var
			j: byte;

		begin

			for j := 1 to V do
				if ((A[val, j]) and not (j in setV)) then
					begin
						if (hamiltonian) then
							break;

						include(setV, j);

						if ((j = 1) and (index = (V + 1))) then
							hamiltonian := TRUE
						else
							findHamiltonian(j, setV, index + 1);

						exclude(setV, j);
					end;

		end;

	begin

		findHamiltonian(1, [], 2);

	end;

procedure isEulerian(A: booleanMatrix);
	var
		E: intV;

	procedure checkConnect(i: byte);
		var
			j: byte;
		begin

			for j := 1 to V do
				if (A[i, j]) then
					begin	
						inc(E[j]);
						if (E[j] = 1) then 
							checkConnect(j);
					end;

		end;

	var
		i: byte;

	begin

		eulerian := TRUE;
		for i := 1 to V do
			E[i] := 0;
		E[1] := 1;
		checkConnect(1);
		dec(E[1]);

		for i := 1 to V do
			begin
				if(A[i, i] and (E[i] <> 0)) then inc(E[i]);
				if((E[i] mod 2 <> 0) or (E[i] = 0)) then 
					begin
						eulerian := FALSE;
						break;
					end;
			end;
	end;

var
	startTime, endTime: real;
	counter, counterEulerian, counterHamiltonian: longint;
	A: booleanMatrix;

begin

	assign(f, '/proc/uptime');

	counter := 0;
	counterEulerian := 0;
	counterHamiltonian := 0;
	eulerian := FALSE;
	hamiltonian := FALSE;
	randomize;

	startTime := getTimeInSecs;
	endTime := startTime;
	writeln(startTime:3:3);
	while ((endTime - startTime) < 10) do
		begin
			generateGraph(A);
			isEulerian(A);
			isHamiltonian(A);
			if (eulerian) then
				begin
					inc(counterEulerian);
					eulerian := FALSE;
				end;
			if (hamiltonian) then
				begin
					inc(counterHamiltonian);
					hamiltonian := FALSE;
				end;
			inc(counter);
			endTime := getTimeInSecs;
		end;

	writeln('All: ', counter);
	writeln('Eulerian: ', counterEulerian);
	writeln('Hamiltonian: ', counterHamiltonian);
	writeln(endTime:3:3);

	close(f);

end.

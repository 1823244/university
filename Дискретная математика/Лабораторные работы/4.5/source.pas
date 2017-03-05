program lab_4_4;

const 
	V = 6; 
	inf = 100000;

type	
	vector = array[1..V] of integer;
	array_vector = array [1..V] of vector;

const
	GR: array_vector = ((0, 10, 4, 0, 2, 0),
                    	(0, 0, 0, 9, 0, 0),
                    	(4, 0, 0, 7, 0, 0),
                    	(0, 9, 7, 0, 0, 2),
                    	(0, 0, 0, 0, 0, 8),
                    	(0, 0, 0, 0, 0, 0));

procedure dijkstra(GR: array_vector);
	var
		count, index, i, j, u, min: integer;
		distance: vector;
		visited: array[1..V] of boolean;
		W: array_vector;

	begin

		for j := 1 to V do
			begin
				for i := 1 to V do
					begin
						distance[i] := inf;
						visited[i] := false;
					end;

				distance[j] := 0;
				
				for count := 1 to V - 1 do
					begin
						min := inf;
						for i := 1 to V do
							if ((not visited[i]) and (distance[i] <= min)) then
								begin
									min := distance[i];
									index := i;
								end;
						u := index;
						visited[u] := true;
						
						for i := 1 to V do
							if ((not visited[i]) and (GR[u, i] <> 0) and (distance[u] <> inf) and ((distance[u] + GR[u, i]) < distance[i])) then
								distance[i] := distance[u] + GR[u, i];
					end;

				for i := 1 to V do
					W[j, i] := distance[i];
					
			end;	

		for i := 1 to V do
			for j := i + 1 to V do
				if ((W[i, j] = W[j, i]) and (W[i, j] <> 0) and (W[i, j] <> inf)) then
					writeln(i, '-', j, ': ', W[i, j]);

	end;

begin
	
	dijkstra(GR);
	readln;
	
end.
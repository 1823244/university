UNIT SORT; 

{-----------------------------------------------------------------------------}
								INTERFACE
{-----------------------------------------------------------------------------}	

	uses Windows, CRT;

	const
		RANDLEFT = 0;
		RANDRIGHT = 65000;

	type 
		TBase = word;
		TSize = word;
		TArray = array of TBase; 
		TProcedure = procedure(var a: TArray; size: TSize); 

	function countTime(var a: TArray; size: TSize; sort: TProcedure): cardinal;
	function checkSort(var a: TArray; size: TSize): boolean;

	procedure generateUnsort(var a: TArray; size: TSize);
	procedure generateSort(var a: TArray; size: TSize);
	procedure generateSortReverse(var a: TArray; size: TSize);
	procedure insertSort(var a: TArray; size: TSize);
	procedure selectSort(var a: TArray; size: TSize);
	procedure bubbleSort(var a: TArray; size: TSize);
	procedure bubbleSortImprovedOne(var a: TArray; size: TSize);
	procedure bubbleSortImprovedTwo(var a: TArray; size: TSize);
	procedure ShellSort(var a: TArray; size: TSize);
	procedure quickSort(var a: TArray; size: TSize);
	procedure heapSort(var a: TArray; size: TSize);
	procedure outputArray(var a: TArray; size: TSize; nameOfFile: string);

{-----------------------------------------------------------------------------}
								IMPLEMENTATION
{-----------------------------------------------------------------------------}

	function countTime(var a: TArray; size: TSize; sort: TProcedure): cardinal;
		var
			start: cardinal;

		begin

			start := GetCurrentTime;
			sort(a, size);
			countTime := GetCurrentTime - start;

		end;

	function checkSort(var a: TArray; size: TSize): boolean;
		var
			i: TSize;
			flag: boolean;

		begin

			flag := TRUE;
			i := 1;

			while ((i <= size) and flag) do
				begin
					flag :=	(a[i] >= a[i - 1]);
					inc(i);
				end;
			
			checkSort := flag;

		end;

	procedure generateUnsort(var a: TArray; size: TSize);
		var
			i: TSize;

		begin

			randomize;
			for i := 0 to size do
				a[i] := random(RANDRIGHT - RANDLEFT) + RANDLEFT;

		end;

	procedure generateSort(var a: TArray; size: TSize);
		var
			i: TSize;

		begin

			for i := 0 to size do
				a[i] := i;

		end;

	procedure generateSortReverse(var a: TArray; size: TSize);
		var
			i: TSize;

		begin

			for i := 0 to size do
				a[i] := size - i;

		end;

	procedure exchange(var a, b: TBase);
		var
			tmp: TBase;

		begin

			tmp := a;
			a := b;
			b := tmp;

		end;

	procedure compareExchange(var a, b: TBase);
		begin

			if (b < a) then
				exchange(a, b);

		end;

	function bCompareExchange(var a, b: TBase): boolean;
		begin

			bCompareExchange := FALSE;
			if (b < a) then
				begin
					exchange(a, b);
					bCompareExchange := TRUE;
				end;

		end;

	procedure insertSort(var a: TArray; size: TSize);
		var
			i, j: TSize;
			tmp: TBase;

		begin

			for i := size downto 1 do
				if (a[i] < a[i - 1]) then
					compareExchange(a[i - 1], a[i]); 
				
			for i := 2 to size do
				begin
					j := i;
					tmp := a[i];

					while (tmp < a[j - 1]) do
						begin
							a[j] := a[j - 1];
							dec(j);
						end;

					a[j] := tmp;
				end;

		end;

	procedure selectSort(var a: TArray; size: TSize);
		var
			i, min, j: TSize;

		begin

			for i := 0 to size - 1 do
				begin
					min := i;

					for j := i + 1 to size do
						begin
							if (a[j] < a[min]) then
								min := j;

							exchange(a[i], a[min]);
						end;
				end;

		end;

	procedure bubbleSort(var a: TArray; size: TSize);
		var
			i, j: TSize;

		begin

			for i := 0 to size - 1 do 
				for j := size downto 1 do
					compareExchange(a[j - 1], a[j]);

		end;

	procedure bubbleSortImprovedOne(var a: TArray; size: TSize);
		var
			flag: boolean;
			i, j: TSize;

		begin

			for i := 0 to size - 1 do 
				begin
					flag := FALSE;

					for j := size downto 1 do
						if (bCompareExchange(a[j - 1], a[j])) then
							flag := TRUE;

					if (not flag) then break;
				end;

		end;

	procedure bubbleSortImprovedTwo(var a: TArray; size: TSize);
		var
			flag: boolean;
			i, j, last: TSize;

		begin

			last := 1;
			for i := 0 to size - 1 do 
				begin
					flag := FALSE;

					for j := size downto last do
						if (bCompareExchange(a[j - 1], a[j])) then
							begin
								last := j - 1;
								flag := TRUE;
							end;

					if (not flag) then break;
				end;

		end;

	procedure ShellSort(var a: TArray; size: TSize);
		var
			h, i, j: TSize;
			tmp: TBase;

		begin

			h := 1;
			while (h <= (size div 9)) do
				h := 3 * h + 1;

			while (h > 0) do
				begin
					h := h div 3;

					for i := h to size do
						begin
							j := i;
							tmp := a[i];
							while ((j >= h) and (tmp < a[j - h])) do
								begin
									a[j] := a[j - h];
									dec(j, h);
								end;

							a[j] := tmp;
						end;
				end;

		end;

	procedure quickSort(var a: TArray; size: TSize);
		procedure quickSortR(left, right: longint);
			var
				i, j: longint;
				pivot: TBase;

			begin

				i := left;
				j := right;
				pivot := a[(left + right) div 2];

				repeat
				
					while (a[i] < pivot) do inc(i);
					while (pivot < a[j]) do dec(j);

					if (i <= j) then
						begin
							exchange(a[i], a[j]);
							inc(i);
							dec(j);
						end;
				until (i > j);

				if (left < j) then quickSortR(left, j);
				if (i < right) then quickSortR(i, right);
			
			end;

		begin

			randomize;
			quickSortR(0, high(a));

		end;

	procedure heapSort(var a: TArray; size: TSize);
		procedure downHeap(left, right: TSize);
			var
				newEl: TBase;
				child: TSize;

			begin

				newEl := a[left];

				while (left <= (right div 2)) do 
					begin
						child := 2 * left;

						if ((child < right) and (a[child] < a[child + 1])) then
							inc(child);

						if (newEl >= a[child]) then break;

						a[left] := a[child];
						left := child;
					end;
				a[left] := newEl;

			end;

		var
			i: TBase;

		begin

			for i := (size div 2 - 1) downto 0 do
				downHeap(i, size);

			for i := size downto 1 do
				begin
					exchange(a[i], a[0]);
					downHeap(0, i - 1);
				end;
	
		end;

	procedure outputArray(var a: TArray; size: word; nameOfFile: string);
		var
			f: text;
			i: word;

		begin

			assign(f, nameOfFile);
			rewrite(f);

			for i := 0 to size do
				write(f, a[i], ' ');

			writeln('Array is written to ', nameOfFile);

			close(f);

		end;

end.
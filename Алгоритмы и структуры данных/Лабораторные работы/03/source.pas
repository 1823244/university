program sd_lab_3;

uses SORT, CRT;

var
	a: TArray;
	size: TSize;

begin

	size := 4500;
	setlength(a, size);
	dec(size);

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Insert time: ', countTime(a, size, @insertSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Select time: ', countTime(a, size, @selectSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Bubble time: ', countTime(a, size, @bubbleSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Bubble time (1): ', countTime(a, size, @bubbleSortImprovedOne));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Bubble time (2): ', countTime(a, size, @bubbleSortImprovedTwo));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Shell time: ', countTime(a, size, @ShellSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Quick time: ', countTime(a, size, @quickSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

	generateSortReverse(a, size);

	outputArray(a, size, 'beforeSort.txt');
	writeln('Heap time: ', countTime(a, size, @heapSort));

	writeln('Sorted: ', checkSort(a, size));
	outputArray(a, size, 'afterSort.txt');
	readln;

end.
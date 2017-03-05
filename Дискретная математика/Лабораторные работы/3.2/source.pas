program lab_3_1;

uses RELATIONS, DOS, SYSUTILS;

var
    fileOfResults: text;

type
  minAndMax = (min, max);
  algorithms = (TCS, TC, TCI, TCW, TCWI);
  pairs = array [1..5] of byte;
  t_arrays = array [minAndMax, algorithms] of boolVariety;
  t_iterations = array [minAndMax, algorithms] of longint;

procedure getNumberOfPairs(var arr: pairs);
  begin

    arr[1] := 1;
    arr[2] := N * N div 4;
    arr[3] := N * N div 2;
    arr[4] := N * N * 2 div 3;
    arr[5] := N * N;

  end;

procedure getIterations(var iterations: t_iterations);
  begin

    iterations[min, TCS] := maxLongInt;
    iterations[min, TC] := maxLongInt;
    iterations[min, TCI] := maxLongInt;
    iterations[min, TCW] := maxLongInt;
    iterations[min, TCWI] := maxLongInt;

    iterations[max, TCS] := 0;
    iterations[max, TC] := 0;
    iterations[max, TCI] := 0;
    iterations[max, TCW] := 0;
    iterations[max, TCWI] := 0;

  end;

procedure arrayClosure(A: boolVariety; var arrays: t_arrays; var iterations: t_iterations);
  var
    tcsA, tcA, tciA, tcwA, tcwiA: boolVariety;
    countIteration: longint;

  begin

    transitivityClosureSlow(A, tcsA, countIteration);
    if (countIteration < iterations[min, TCS]) then
      begin
        iterations[min, TCS] := countIteration;
        arrays[min, TCS] := A;
      end;
    if (countIteration > iterations[max, TCS]) then
      begin
        iterations[max, TCS] := countIteration;
        arrays[max, TCS] := A;
      end;

    transitivityClosure(A, tcA, countIteration);
    if (countIteration < iterations[min, TC]) then
      begin
        iterations[min, TC] := countIteration;
        arrays[min, TC] := A;
      end;
    if (countIteration > iterations[max, TC]) then
      begin
        iterations[max, TC] := countIteration;
        arrays[max, TC] := A;
      end;

    transitivityClosureImproved(A, tciA, countIteration);
    if (countIteration < iterations[min, TCI]) then
      begin
        iterations[min, TCI]:= countIteration;
        arrays[min, TCI] := A;
      end;
    if (countIteration > iterations[max, TCI]) then
      begin
        iterations[max, TCI]:= countIteration;
        arrays[max, TCI] := A;
      end;

    transitivityClosureWarshall(A, tcwA, countIteration);
    if (countIteration < iterations[min, TCW]) then
      begin
        iterations[min, TCW]:= countIteration;
        arrays[min, TCW] := A;
      end;
    if (countIteration > iterations[max, TCW]) then
      begin
        iterations[max, TCW]:= countIteration;
        arrays[max, TCW] := A;
      end;

    transitivityClosureWarshallImproved(A, tcwiA, countIteration);
    if (countIteration < iterations[min, TCWI]) then
      begin
        iterations[min, TCWI]:= countIteration;
        arrays[min, TCWI] := A;
      end;
    if (countIteration > iterations[max, TCWI]) then
      begin
        iterations[max, TCWI]:= countIteration;
        arrays[max, TCWI] := A;
      end;

  end;

procedure outputRelation(A: boolVariety);
  var
    x, y, i: Byte;
  begin

    writeln(fileOfResults);
    write(fileOfResults, '        ');

    i := 0;
    for x := 1 to N do
      begin
        if (x < 10) then
          write(fileOfResults, ' ');

        write(fileOfResults, x, ' ');
      end;

    writeln(fileOfResults);
    write(fileOfResults, '       ');

    for x := 1 to N do
      write(fileOfResults, '---');

    writeln(fileOfResults);
    for x := 1 to N do
      begin
        if (x < 10) then
          write(fileOfResults, '     ')
        else
          write(fileOfResults, '    ');
        write(fileOfResults, x, ' |');

        for y := 1 to N do
          begin
            if (A[x, y]) then
              write(fileOfResults, ' ', 1, ' ')
            else
              write(fileOfResults, ' ', 0, ' ');
          end;

        writeln(fileOfResults);
      end;

      writeln(fileOfResults);

  end;

procedure saveResult(numberOfPairs: byte; arrays: t_arrays; iterations: t_iterations);
  begin

    writeln(fileOfResults, '  Number of pairs: ', numberOfPairs);

    writeln(fileOfResults);
    writeln(fileOfResults, '  Slow transitivity closure:');
    writeln(fileOfResults);
    writeln(fileOfResults, '    Minimum: ', iterations[min, TCS]);
    outputRelation(arrays[min, TCS]);
    writeln(fileOfResults, '    Maximum: ', iterations[max, TCS]);
    outputRelation(arrays[max, TCS]);

    writeln(fileOfResults);
    writeln(fileOfResults, '  Transitivity closure:');
    writeln(fileOfResults);
    writeln(fileOfResults, '    Minimum: ', iterations[min, TC]);
    outputRelation(arrays[min, TC]);
    writeln(fileOfResults, '    Maximum: ', iterations[max, TC]);
    outputRelation(arrays[max, TC]);

    writeln(fileOfResults);
    writeln(fileOfResults, '  Transitivity closure improved:');
    writeln(fileOfResults);
    writeln(fileOfResults, '    Minimum: ', iterations[min, TCI]);
    outputRelation(arrays[min, TCI]);
    writeln(fileOfResults, '    Maximum: ', iterations[max, TCI]);
    outputRelation(arrays[max, TCI]);

    writeln(fileOfResults);
    writeln(fileOfResults, '  Transitivity closure by Warshall:');
    writeln(fileOfResults);
    writeln(fileOfResults, '    Minimum: ', iterations[min, TCW]);
    outputRelation(arrays[min, TCW]);
    writeln(fileOfResults, '    Maximum: ', iterations[max, TCW]);
    outputRelation(arrays[max, TCW]);

    writeln(fileOfResults);
    writeln(fileOfResults, '  Transitivity closure improved by Warshall:');
    writeln(fileOfResults);
    writeln(fileOfResults, '    Minimum: ', iterations[min, TCWI]);
    outputRelation(arrays[min, TCWI]);
    writeln(fileOfResults, '    Maximum: ', iterations[max, TCWI]);
    outputRelation(arrays[max, TCWI]);

  end;

var
  arrays: t_arrays;
  A: boolVariety;
  i: byte;
  j: word;
  iterations: t_iterations;
  numberOfPairs: pairs;
  nameOfFile: string;

begin

  nameOfFile := 'results_' + IntToStr(N) + '.txt';
  assign(fileOfResults, nameOfFile);
  rewrite(fileOfResults);
  writeln(fileOfResults, 'N = ', N);
  writeln(fileOfResults);

  getNumberOfPairs(numberOfPairs);

  for i := 1 to 5 do
    begin
      getIterations(iterations);

      for j := 1 to 1000 do
        begin
          generate(numberOfPairs[i], A);
          arrayClosure(A, arrays, iterations);
        end;

      writeln(i, ' iteration has been completed.');

      saveResult(numberOfPairs[i], arrays, iterations);
    end;

  close(fileOfResults);

end.

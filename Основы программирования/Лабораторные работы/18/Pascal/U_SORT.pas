UNIT U_SORT;

{-----------------------------------------------------------------}
							INTERFACE
{-----------------------------------------------------------------}	

	type
		t_func = function(var a, b): boolean;
	
	procedure swap(var a, b; size: byte);
	procedure sort(var a; a_length, el_size: byte; func: t_func);


{-----------------------------------------------------------------}
							IMPLEMENTATION
{-----------------------------------------------------------------}

	procedure swap(var a, b; size: byte);

		var 
			p: pointer;
    
    	begin
        	
        	getmem(p, size);
        	move(a, p^, size);
        	move(b, a, size);
        	move(p^, b, size);
        	freemem(p, size);

		end;


	procedure sort(var arr; a_length, el_size: byte; func: t_func);

		type	
			pbyte = ^byte;

		var
			start, first, second, max: pbyte;
			i, j: byte;

		begin
			
			start := pbyte(arr);

			for i := 0 to a_length - 2 do
				begin
					first := pbyte(longword(start) + i*el_size);
					max := first;
					for j := i + 1 to a_length - 1 do
						begin
							second := pbyte(longword(start) + j*el_size);
							if (func(second^, max^)) then
								max := second;
						end;
					swap(first^, max^, el_size);
				end;

		end;

begin
	
	//nothing to do here

end.
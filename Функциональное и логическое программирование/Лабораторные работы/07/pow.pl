pow(X, Y, Z) :-
	justpow(X, Y, 1, Z), !.
justpow(_, 0, F, Z) :- Z is F.
justpow(X, Y, F, Z) :-
	Y1 is Y - 1,
	F1 is F * X,
	justpow(X, Y1, F1, Z).

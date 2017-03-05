append(E, [], [E]).
append(E, [Head|Tail], [Head|F]) :-
	append(E, Tail, F).

remove(E, [E|Tail], [Tail]).
remove(E, [Head|Tail], [Head|F]) :-
	remove(E, Tail, F).

concat([], [Y], [Y]).
concat([X|XTail], [Y], [X|F]) :-
	concat(XTail, [Y], F).

llength([], 0).
llength([_|Tail], N) :-
	llength(Tail, N1),
	N is N1 + 1.

reverse([], []).
reverse([Head|Tail], F) :-
	reverse(Tail, F1),
	concat(F1, [Head], F).

palindrome(X) :-
	reverse(X, X).

insert_sort([], []).
insert_sort([Head|Tail], Sorted) :-
	insert_sort(Tail, SortedTail),
	insert(Head, SortedTail, Sorted).

insert(E, [STHead|STTail], [STHead|STail]) :-
	E @> STHead, !,
	insert(E, STTail, STail).
insert(E, STTail, [E|STTail]).

add_one([], []).
add_one([Head|Tail], [PlusOne|Rest]) :-
	PlusOne is Head + 1,
	add_one(Tail, Rest).

split_by_sign([], [], []).
split_by_sign([Head|Tail], [Head|Tail2], List) :-
	Head >= 0, !,
	split_by_sign(Tail, Tail2, List).
split_by_sign([Head|Tail], List, [Head|Tail2]) :-
	split_by_sign(Tail, List, Tail2).









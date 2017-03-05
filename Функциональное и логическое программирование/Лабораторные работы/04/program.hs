{-
data Expr =   Const Integer
			| Var String
			| Add Expr Expr
			| Mult Expr Expr deriving (Eq, Show)

-- First task
diff :: Expr -> String -> Expr 
diff (Const _) _ = Const 0
diff (Var x) a 	| x == a = Const 1
				| otherwise = Const 0
diff (Add x y) a = Add (diff x a) (diff y a)
diff (Mult x y) a = Add (Mult (diff x a) y) (Mult x (diff y a))

{- 	
	f(x, y) 	= 2 * x^2 * y^3 + 3 * x^4 + 5 * y - 7
	f(x, y)'_x	= 4 * x * y^3 + 12 * x^3
	f(x, y)'_y	= 6 * x^2 * y^2 + 5
-}
test :: Expr
test = diff (Add (Add (Add (Mult (Mult (Const 2) (Mult (Var "x") (Var "x"))) (Mult (Mult (Var "y") (Var "y")) (Var "y"))) (Mult (Const 3) (Mult (Mult (Var "x") (Var "x")) (Mult (Var "x") (Var "x"))))) (Mult  (Const 5) (Var "y"))) (Const (-7))) "x"

-- Second task
simplify :: Expr -> Expr
simplify e 	| smplf e == smplf (smplf e) = smplf e
			| otherwise = simplify (smplf e)

smplf :: Expr -> Expr
smplf (Add x (Const 0)) = smplf x
smplf (Add (Const 0) y) = smplf y
smplf (Add x y) = Add (smplf x) (smplf y)
smplf (Mult x (Const 1)) = smplf x
smplf (Mult (Const 1) y) = smplf y
smplf (Mult x y)	| x == Const 0 || y == Const 0 = Const 0
					| otherwise = Mult (smplf x) (smplf y)
smplf a = a

-- Third task
toString :: Expr -> String
toString (Const x) = show x
toString (Var x) = x
toString (Add x y) = toString x ++ " + " ++ toString y
toString (Mult (Add y1 z1) (Add y2 z2)) = "(" ++ toString (Add y1 z1) ++ ") * (" ++ toString (Add y2 z2) ++ ")" 
toString (Mult x (Add y z)) = toString x ++ " * (" ++ toString (Add y z) ++ ")"
toString (Mult (Add y z) x) = "(" ++ toString (Add y z) ++ ") * " ++ toString x
toString (Mult x y) = toString x ++ " * " ++ toString y

-- Fourth task
eval :: Expr -> [(String, Integer)] -> Integer
eval (Const a) _ = a
eval (Var x) ps = (snd . head . (filter ((== x).fst))) ps
eval (Add x y) ps = eval x ps + eval y ps
eval (Mult x y) ps = eval x ps * eval y ps
-}

-- Additional task
data Expr =    Const Integer
			 | X
			 | LogX -- only output
			 | Add Expr Expr
			 | Mult Expr Expr
			 | Div Expr Expr
			 | Pow Expr Expr deriving (Eq, Show)  -- 1st only X, 2nd only Const

int :: Expr -> Expr
int (Const a) = Mult (Const a) (X)

int (X) = Div (Pow (X) (Const 2)) (Const 2)

int (Add x y) = Add (int x) (int y)

int (Mult (Const x) y) = Mult (Const x) (int y)
int (Mult x (Const y)) = Mult (int x) (Const y)

int (Div (Const x) (Const y)) = Div (Mult (Const x) (X)) (Const y)
int (Div (Const x) (X)) = Mult (Const x) (LogX)
--int (Div (Const x) y) = Mult (Const x) (int (Div (Const 1) y))
int (Div x (Const y)) = Div (int x) (Const y)

int (Pow (X) (Const (-1))) = LogX
int (Pow (X) (Const y)) = Div (Pow (X) (Const (y + 1))) (Const (y + 1))


{-
	without pow: 2 * x * x / x * x * x + 3 * x * x * x * x + 5 / x - 7
	with pow: 2 * x^2 / x^3 + 3 * x^4 + 5 / x - 7
	with `only max simplified`: 2 / x + 3 * x^4 + 5 / x - 7
-}

test :: Expr
--test = simplify (Add (Div (Mult (Const 2) (Mult (X) (X))) (Mult (X) (Mult (X) (X)))) (Add (Mult (Const 3) (Mult (Mult (X) (X)) (Mult (X) (X)))) (Add (Div (Const 5) (X)) (Const (-7)))))
test = int (Add (Div (Const 2) (X)) (Add (Mult (Const 3) (Pow (X) (Const 4))) (Add (Div (Const 5) (X)) (Const (-7)))))

simplify :: Expr -> Expr
simplify e 	| smplf e == smplf (smplf e) = smplf e
			| otherwise = simplify (smplf e)

smplf :: Expr -> Expr
smplf (Add x (Const 0)) = smplf x
smplf (Add (Const 0) y) = smplf y
smplf (Add x y) = Add (smplf x) (smplf y)

smplf (Mult x (Const 1)) = smplf x
smplf (Mult (Const 1) y) = smplf y
smplf (Mult (X) (X)) = Pow (X) (Const 2)
smplf (Mult (X) (Pow (X) (Const z))) = Pow (X) (Const (z + 1))
smplf (Mult (Pow (X) (Const z)) (X)) = Pow (X) (Const (z + 1))
smplf (Mult x y)	| x == Const 0 || y == Const 0 = Const 0
					| otherwise = Mult (smplf x) (smplf y)

smplf (Div (Const 0) y) = Const 0
smplf (Div x (Const 1)) = smplf x
smplf (Div (Const 1) (Pow y (Const z))) = Pow y (Const (-z))
smplf (Div x y) = Div (smplf x) (smplf y)

smplf (Pow x (Const 0)) = Const 0
smplf (Pow x (Const 1)) = x

smplf a = a
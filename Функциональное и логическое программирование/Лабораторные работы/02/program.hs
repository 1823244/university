-- First task

---- First subtask
listN :: Integer -> [Integer]
listN 1 = [1]
listN n = listN (n - 1) ++ [n]

---- Second subtask
listOddN :: Integer -> [Integer]
listOddN 1 = [1]
listOddN n = listOddN (n - 1) ++ [2 * n - 1]

---- Third subtask
listEvenN :: Integer -> [Integer]
listEvenN 1 = [2]
listEvenN n = listEvenN (n - 1) ++ [2 * n]

---- Fourth subtask
listSquareN :: Integer -> [Integer]
listSquareN 1 = [1]
listSquareN n = listSquareN (n - 1) ++ [n^2]

---- Fifth subtask
listFactN :: Integer -> [Integer]
listFactN 1 = [1]
listFactN n = listFactN (n - 1) ++ [fact n]

fact :: Integer -> Integer
fact 1 = 1
fact n = fact' n 1
	where
		fact' 0 acc = acc
		fact' n acc = fact' (n - 1) (acc * n)

---- Sixth subtask
listBinSquareN :: Integer -> [Integer]
listBinSquareN 1 = [2]
listBinSquareN n = listBinSquareN (n - 1) ++ [2^n]

---- Seventh subtask
listTriangularN :: Integer -> [Integer]
listTriangularN 1 = [1]
listTriangularN n = listTriangularN (n - 1) ++ [triangular n]
 
triangular :: Integer -> Integer
triangular 1 = 1
triangular n = triangular' n 1
	where
		triangular' 1 acc = acc
		triangular' n acc = triangular' (n - 1) $ n + acc

---- Eighth subtask
listPyramidalN :: Integer -> [Integer]
listPyramidalN 1 = [1]
listPyramidalN n = listPyramidalN (n - 1) ++ [pyramidal n]

pyramidal :: Integer -> Integer
pyramidal 1 = 1
pyramidal n = pyramidal (n - 1) + triangular n

-- Second task

---- First subtask
meanOfFloat :: [Float] -> Float
meanOfFloat x = meanOfFloat' x 1 0

meanOfFloat' (x:[]) y z = (x + z) / fromIntegral y
meanOfFloat' (x:xs) y z = meanOfFloat' xs (y + 1) $ x + z
{-
	where
		meanOfFloat' (x:xs) y z = 
			if xs == [] 
				then (x + z) / (fromIntegral y)
				else meanOfFloat' xs y $ z + x
-}
---- Second task
removeOdd :: [Int] -> [Int]
removeOdd [] = []
removeOdd (x:xs) = 
	if even x
 		then x : (removeOdd xs)
		else removeOdd xs

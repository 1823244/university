import Data.Maybe

-- First task
---- First subtask
avg :: [Float] -> Float
avg xs = (fst res) / (snd res)
	where res = foldr (\x (sum, count) -> (sum + x, count + 1)) (0, 0) xs

---- Second subtask
scalar :: [Float] -> [Float] -> Float
scalar xs ys = foldr (+) 0 $ zipWith (*) xs ys

---- Third subtask
countEven :: [Int] -> Int
countEven = length . (filter even)

---- Fourth subtask
quicksort :: Ord a => [a] -> [a]
quicksort [] = []
quicksort (x:xs) = quicksort (filter (< x) xs) ++ [x] ++ quicksort (filter (>= x) xs)

---- Fifth subtask
quicksortF :: Ord a => [a] -> (a -> a -> Bool) -> [a]
quicksortF [] _ = []
quicksortF (x:xs) f = quicksortF less f ++ [x] ++ quicksortF more f
	where 
		less = [y | y <- xs, f x y]
		more = [y | y <- xs, not (f x y)]

test :: [Int]
--test = quicksortF [1, 2, 5, 14, 10, -4, 123, -100, 56, 32, 9, 100] (\x y -> if (x < y) then True else False)
test = quicksort [1, 2, 5, 14, 10, -4, 123, -100, 56, 32, 9, 100]

-- Second task
data Product =   Book String String
			   | Tape String
			   | Disc String String Int deriving (Eq, Show)
			   
getTitle :: Product -> String
getTitle (Book a _) = a
getTitle (Tape a) = a
getTitle (Disc a _ _) = a

getTitles :: [Product] -> [String]
getTitles = map getTitle

getAuthors :: [Product] -> [String]
getAuthors = map (\(Book _ b) -> b) . filter isBook
	where 
		isBook (Book _ _) = True
		isBook _ = False

lookupTitle :: String -> [Product] -> Maybe Product
lookupTitle a = listToMaybe . filter (\x -> a == getTitle x)

lookupTitles :: [String] -> [Product] -> [Product]
lookupTitles xs ys = catMaybes $ map (\x -> lookupTitle x ys) xs

test2 :: Maybe Product
test2 = lookupTitle "title-tape" [(Book "title" "author"), (Tape "title-tape"), (Disc "title-disc" "x3" 100)]
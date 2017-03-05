data Product =   Book String String
			   | Tape String
			   | Disc String String Int deriving (Eq, Show)
			   
getTitle :: Product -> String
getTitle (Book a _) = a
getTitle (Tape a) = a
getTitle (Disc a _ _) = a

getTitles :: [Product] -> [String]
getTitles [] = []
getTitles (x:xs) = (getTitle x) : (getTitles xs)

getAuthors :: [Product] -> [String]
getAuthors [] = []
getAuthors ((Book _ b):xs) = b : getAuthors xs
getAuthors (_:xs) = getAuthors xs

lookupTitle :: String -> [Product] -> Maybe Product
lookupTitle a [] = Nothing
lookupTitle a (x:xs) | a == getTitle x 	= Just x
					 | otherwise 		= lookupTitle a xs

lookupTitles :: [String] -> [Product] -> [Product]
lookupTitles [] _ = []
lookupTitles (x:xs) ys = (\(Just n) -> n) (lookupTitle x ys) : lookupTitles xs ys

-- avg (length of title / num of songs)
-- filter map sum list

avgTitleNum :: [Product] -> Float
{-avgTitleNum xs = avgTitleNum' xs 0 0

avgTitleNum' [] titles num = fromIntegral titles / fromIntegral num
avgTitleNum' ((Disc t _ n):xs) titles num = avgTitleNum' xs (titles + length t) (num + n)   
avgTitleNum' (_:xs) titles num = avgTitleNum' xs titles num
-}
avgTitleNum xs = (sum . map (\(Disc t _ n) -> fromIntegral (length t) / fromIntegral n) . filter filterDisc) xs
	where 
		filterDisc (Disc _ _ _) = True
		filterDisc (Book _ _) = False
		filterDisc (Tape _) = False
{-
length 'title' + length 'title2' = 11
12 + 3 = 15
res: 11/15
-}
test :: Float
test = avgTitleNum [(Tape "lalala"), (Disc "title" "author" 12), (Disc "title2" "author" 3)]

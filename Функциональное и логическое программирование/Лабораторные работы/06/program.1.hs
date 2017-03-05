main = do getLine >>= putStr . (++ "\n") . show . (foldr (+) 0) . (map read) . words

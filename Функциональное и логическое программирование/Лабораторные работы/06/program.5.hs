main = max3

max3 :: IO()
max3 = do line <- getLine
          (putStr . (++ "\n") . show . maximum) (map read (words line) :: [Int])

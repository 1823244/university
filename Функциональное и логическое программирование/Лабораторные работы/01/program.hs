-- First task
-- :t (('c', 0 :: Integer), "string" :: String, [0.0 :: Double, 0.1])

-- Second task
max3 :: Integer -> Integer -> Integer -> Integer
max3 x y z = max x (max y z)

-- Additional task
isParallel :: (Double, Double) -> (Double, Double) -> (Double, Double) -> (Double, Double) -> Bool
isParallel (x11, y11) (x12, y12) (x21, y21) (x22, y22) = ((y12 - y11) / (x12 - x11)) == ((y22 - y21) / (x22 - x21)) 

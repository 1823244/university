import System.Environment

main = do getArgs >>= mapM putStrLn
            
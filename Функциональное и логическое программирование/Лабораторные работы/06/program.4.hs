-- in command line == using args?
import System.Environment
import System.IO

main = do args <- getArgs
          fromHandle <- openFile (args !! 1) ReadMode
          hGetContents fromHandle >>= putStr . (++ "\n") . unlines . take (read $ args !! 0) . lines
          hClose fromHandle
-- in command line == using args?
import System.Environment
import System.IO

main = do args <- getArgs
          fromHandle <- openFile (head args) ReadMode
          hGetContents fromHandle >>= putStr . (++ "\n")
          hClose fromHandle
@echo Input File Name
@set /p path1=
@if not exist %path1%.asm goto ERROR_FILE
\masm32\bin\ml /c /coff %path1%.asm
@if errorlevel 1 goto ERROR_MASM
@pause
\masm32\bin\link /SUBSYSTEM:WINDOWS /DLL /DEF:%path1%.def %path1%.obj
@if errorlevel 1 goto ERROR_LINK
@goto END
:ERROR_FILE
@echo Error! File not found.
@goto END
:ERROR_MASM
@echo Error! Not Compile or dont find ml.exe
@goto END
:ERROR_LINK
@echo Error! Link.exe dont work or find. 
:END
@pause
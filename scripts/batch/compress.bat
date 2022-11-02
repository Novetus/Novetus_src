@ECHO OFF

SET SourceDir=%CD%
SET DestDir=%CD%

for %%f in (*.rbxl) do (
    if "%%~xf"==".rbxl" (
        "C:\Program Files\7-Zip\7z.exe" a "%DestDir%\%%f.bz2" "%SourceDir%\%%f"
        del "%%f"
    )
)

del "%SourceDir%\compress.bat"
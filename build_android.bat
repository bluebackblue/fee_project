SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%



@REM ---------------------------------------
@REM ■出力先
@REM ---------------------------------------
SET OUTPUT_PATH=%HOME%output



@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2018_3
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2018_3\
SET UNITY_EXE=%UNITY_2018_3_14F1%

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\WebGL_2018_3\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\Exe_2018_3\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Fee.apk .\unity_2018_3.apk
@REM ---------------------------------------



@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2018_4
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2018_4\
SET UNITY_EXE=%UNITY_2018_4_2F1%

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\WebGL_2018_4\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\Exe_2018_4\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Fee.apk .\unity_2018_4.apk
@REM ---------------------------------------



@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2019_1
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2019_1\
SET UNITY_EXE=%UNITY_2019_1_6F1%

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\WebGL_2019_1\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\Exe_2019_1\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Fee.apk .\unity_2019_1.apk
@REM ---------------------------------------



@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2019_2
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2019_2\
SET UNITY_EXE=%UNITY_2019_2_0A11%

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\WebGL_2019_2\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\Exe_2019_2\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Fee.apk .\unity_2019_2.apk
@REM ---------------------------------------



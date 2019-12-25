SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%




@REM ---------------------------------------
@REM ■設定
@REM ---------------------------------------
DEL build.log
RMDIR /s /q result
RMDIR /s /q output
MKDIR result
SET OUTPUT_PATH=%HOME%output




@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2019_2
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2019_2\
SET UNITY_EXE="C:\Program Files\Unity\Hub\Editor\2019.2.17f1\Editor\Unity.exe"

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\result\\WebGL_2019_2\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\result\Exe_2019_2\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
COPY /y output\Fee.apk .\result\unity_2019_2.apk
@REM ---------------------------------------




@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2020_1
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2020_1\
SET UNITY_EXE="C:\Program Files\Unity\Hub\Editor\2020.1.0a17\Editor\Unity.exe"

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\result\WebGL_2020_1\
@REM ---------------------------------------

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\result\Exe_2020_1\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
COPY /y output\Fee.apk .\result\unity_2020_1.apk
@REM ---------------------------------------



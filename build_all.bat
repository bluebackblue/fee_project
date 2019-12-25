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
SET VERSION_A=2019
SET VERSION_B=2
SET VERSION_C=17f1

@REM ---------------------------------------
@REM Set
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_%VERSION_A%_%VERSION_B%\
SET UNITY_EXE="C:\Program Files\Unity\Hub\Editor\%VERSION_A%.%VERSION_B%.%VERSION_C%\Editor\Unity.exe"

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\result\Exe_%VERSION_A%_%VERSION_B%\
@REM ---------------------------------------

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\result\\WebGL_%VERSION_A%_%VERSION_B%\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
COPY /y output\Fee.apk .\result\unity_%VERSION_A%_%VERSION_B%.apk
@REM ---------------------------------------




@REM ---------------------------------------
@REM ■リセット
@REM ---------------------------------------
RMDIR /s /q %OUTPUT_PATH%
MKDIR %OUTPUT_PATH%

@REM ---------------------------------------
@REM 2020_1
@REM ---------------------------------------
SET VERSION_A=2020
SET VERSION_B=1
SET VERSION_C=0a17

@REM ---------------------------------------
@REM Set
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_%VERSION_A%_%VERSION_B%\
SET UNITY_EXE="C:\Program Files\Unity\Hub\Editor\%VERSION_A%.%VERSION_B%.%VERSION_C%\Editor\Unity.exe"

@REM ---------------------------------------
@REM StandaloneWindows
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_StandAloneWindows ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\Exe .\result\Exe_%VERSION_A%_%VERSION_B%\
@REM ---------------------------------------

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
XCOPY /y /e output\WebGL .\result\\WebGL_%VERSION_A%_%VERSION_B%\
@REM ---------------------------------------

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_EXE% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
COPY /y output\Fee.apk .\result\unity_%VERSION_A%_%VERSION_B%.apk
@REM ---------------------------------------



SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%


SET OUTPUT_PATH=%HOME%



@REM ---------------------------------------
@REM ÉNÉäÉA
@REM ---------------------------------------
DEL Fee.apk




@REM ---------------------------------------
@REM 2018_3
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2018_3\

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_2018_3_14F1% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
RENAME Fee.apk unity_2018_3.apk
@REM ---------------------------------------




@REM ---------------------------------------
@REM 2018_4
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2018_4\

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_2018_4_2F1% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
RENAME Fee.apk unity_2018_4.apk
@REM ---------------------------------------




@REM ---------------------------------------
@REM 2019_1
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2019_1\

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_2019_1_6F1% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
RENAME Fee.apk unity_2019_1.apk
@REM ---------------------------------------




@REM ---------------------------------------
@REM 2019_2
@REM ---------------------------------------
SET PRIJECT_PATH=%HOME%unity_2019_2\

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_2019_2_0A11% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.EditorTool.Build.Build_Android ---outputpath %OUTPUT_PATH%
RENAME Fee.apk unity_2019_2.apk
@REM ---------------------------------------


















@PAUSE




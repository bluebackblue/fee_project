SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

SET PRIJECT_PATH=%HOME%
SET OUTPUT_PATH=%HOME%

@REM ---------------------------------------
@REM Android
@REM ---------------------------------------
%UNITY_2018_3% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.UnityInitialize.Build.Build_Android ---outputpath %OUTPUT_PATH%
@REM ---------------------------------------

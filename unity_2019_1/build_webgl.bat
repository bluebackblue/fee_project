SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

SET PRIJECT_PATH=%HOME%
SET OUTPUT_PATH=%HOME%

@REM ---------------------------------------
@REM WebGL
@REM ---------------------------------------
%UNITY_2019_1% -quit -batchmode -logFile .\build.log -projectPath %PRIJECT_PATH% -executeMethod Fee.UnityInitialize.Build.Build_WebGL ---outputpath %OUTPUT_PATH%
@REM ---------------------------------------

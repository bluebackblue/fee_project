SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

@REM %ANDROID_ADB% kill-server
@PAUSE

@REM %ANDROID_ADB% devices
@PAUSE

%ANDROID_ADB% install %1
@PAUSE

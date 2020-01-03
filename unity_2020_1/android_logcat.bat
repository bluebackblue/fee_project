SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

%ANDROID_ADB% logcat >> logcat.log

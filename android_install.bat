SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

%ANDROID_ADB% kill-server

%ANDROID_ADB% devices

%ANDROID_ADB% install -r .\result\unity_2019_2.apk
%ANDROID_ADB% install -r .\result\unity_2020_1.apk

@PAUSE

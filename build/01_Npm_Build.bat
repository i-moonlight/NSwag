rmdir "%~dp0\..\src\NSwag.Npm\bin\binaries" /Q /S nonemptydir
mkdir "%~dp0\..\src\NSwag.Npm\bin\binaries"

REM Build and copy full .NET command line
nuget restore "%~dp0/../src/NSwag.sln"
msbuild "%~dp0/../src/NSwag.sln" /p:Configuration=Release /t:rebuild

xcopy "%~dp0/../src/NSwag.Console/bin/Release/net461" "%~dp0/../src/NSwag.Npm/bin/binaries/full" /E /I /y
xcopy "%~dp0\..\src\NSwag.Console.x86\bin\Release\net461\NSwag.x86.exe" "%~dp0\..\src\NSwag.Npm\bin\binaries\full"
xcopy "%~dp0\..\src\NSwag.Console.x86\bin\Release\net461\NSwag.x86.exe.config" "%~dp0\..\src\NSwag.Npm\bin\binaries\full"

xcopy "%~dp0\..\src\NSwagStudio.Installer\bin\Release\NSwagStudio.msi" "%~dp0\..\src\NSwagStudio.Installer"

REM Build and copy .NET Core command line

dotnet restore "%~dp0/../src/NSwag.ConsoleCore" --no-cache
dotnet build "%~dp0/../src/NSwag.ConsoleCore"
dotnet publish "%~dp0/../src/NSwag.ConsoleCore" -c release -f "netcoreapp1.0"
dotnet publish "%~dp0/../src/NSwag.ConsoleCore" -c release -f "netcoreapp1.1"
dotnet publish "%~dp0/../src/NSwag.ConsoleCore" -c release -f "netcoreapp2.0"

xcopy "%~dp0/../src/NSwag.ConsoleCore/bin/release/netcoreapp1.0/publish" "%~dp0/../src/NSwag.Npm/bin/binaries/netcoreapp1.0" /E /I /y
xcopy "%~dp0/../src/NSwag.ConsoleCore/bin/release/netcoreapp1.1/publish" "%~dp0/../src/NSwag.Npm/bin/binaries/netcoreapp1.1" /E /I /y
xcopy "%~dp0/../src/NSwag.ConsoleCore/bin/release/netcoreapp2.0/publish" "%~dp0/../src/NSwag.Npm/bin/binaries/netcoreapp2.0" /E /I /y
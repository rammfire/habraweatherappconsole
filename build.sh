#! /bin/bash
dotnet build /p:Configuration=Debug /p:Platform="x64"
cd bin/x64/Debug/net5.0/
./habraweatherappconsole
cd ../../../../
@echo off

net start memurai
memurai-cli ping

cd back
cd StoreManager
start dotnet run


cd ..\..
cd  front
cd react-app
start npm i
start npm start

cd ..\
start npm i
start npm start

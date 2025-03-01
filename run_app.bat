@echo off

cd back
cd StoreManager
start dotnet run


cd ..\..
cd  front
cd react-app
start npm start

cd ..\
start npm start

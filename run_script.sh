#!/bin/bash

cd back
cd StoreManager
dotnet run &

cd ../../front
npm start &

cd ../


npm start &
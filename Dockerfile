FROM mcr.microsoft.com/azure-sql-edge:latest

WORKDIR /db

COPY AIRBNB2022.bacpac .

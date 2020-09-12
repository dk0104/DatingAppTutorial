# Implementation Notes
## VSCode
**CMD** command combo [ Shift Ctrl P ]

## API 
### Create API using .Net core SDK
> Get contextual help
>`- dotnet new -h // Contextual help `

`dotnet new webapi -n DatingApp.API`

- Controller / Routing
Enable CORS Policy
  

## Model 

- Install entity framework
>Terminal: `dotnet tool install --global dotnet-ef` <br>
>Nuget: `install package Microfot.EntityFramework.core` <br>

- Setup Sqlite with nuget package manager 
>Nuget : `Microsoft.EntityFrameworkCore.Sqlite`
- Setup for docker, use connection strings via environmetnt settings

## GUI  

# Packege API to docker

- Via CLI
  `docker build -t username/name`
- Via Visual studio code

  **CMD** *Docker add dockerfiles* <br>
  **CMD** *Docker Images build image* <br>
  **CMD** *Docker containers start* <br>


## Docker VSCode general 
https://www.youtube.com/watch?v=sUZxIWDUicA&t=1296s
 
Docker-Compose in order to mantain connections 
Todo
[ ] Learn more about docker compose 

Example compose file 
version: '3.4'

services:
  ms-sql-server:
    image: mcr.microsoft.com/mssql/server:2017-latest-ubuntu 
    environment: 
      ACCEPT_EULA: "Y"
      SA_PASSWORD: "Pa55w0rd"
      MSSQL_PID: Express
    ports: 
      - 1433:1433
  datingapp:
    image: datingapp
    build:
      context: .
      dockerfile: DatingApp.API/Dockerfile
    environment: 
      DBServer:"ms-sql-server"
    ports:
      - 8080:80
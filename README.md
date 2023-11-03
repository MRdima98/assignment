# Coding test
## Tecnologies used
The project was build with ASP.NET Core Web API and tested with Bogus+MSTest.
## How to run
Make sure .Net 6.0 is installed, then run: 
```
dotnet run
```
## How to run test
Install the packages
``` 
dotnet restore
```
Then run the tests
```
dotnet test 
```
## Endpoints
To verify that the system is working you can ping 
```
/ping
```
To get all todos filtered by offset and limit you can use 
```
/getAllTasks?limit=<number>&offset<number>
```
To get all todos filtered by user, offset and limit you can use
```
/getTasksByUser?userID=<id>&limit=<number>&offset<number>
```
Offset and limit are optional for both endpoints, while 
userID is needed. If your params are invalid 
it will return a 404.

## Frontend
The frontend was developed with angular.
For the sake of simplicity the folder was saved inside the same project as 
"Frontend".
Make sure to have ``ng`` and ``npm``
(or any other package manager) then 
enter the Frontend folder. In my case to update the folder using npm:
``` 
npm install
``` 
and run the server with 
``` 
ng serve --open
```


# Pragmateam code challenge server (.NET)

Please refer to the provided document for the code challenge requirements.

## Framework & languages
This project uses
* .Net 5.0 / C#

## Available scripts
- `cd code-challenge-server-dotnet`
- `dotnet restore`
- `dotnet build`
- `dotnet run --project ./server/server.csproj --launch-profile server` - Start the server (Port 8081)
- `dotnet test --logger "console;verbosity=detailed"` - To run the test cases

## Improvements
- A separate server.Tests project was created for integration test and unit tests.
- A solution file is created to include both the server and server.Tests projects.
- The service class is created and the business logic is moved into it.
- A sensor service URL has been added to the "SensorService" section of the appsettings.json file.
- There are more test cases added and the test coverage has nearly reached 100%
- An External API call failure log has been added.
- Dependency injection use to create service and configuration objects.


## Future Improvements
- Improve the Logging with different log levels in different environments.
- Add more integration test cases for unexpected failures/ exceptions from external API call.
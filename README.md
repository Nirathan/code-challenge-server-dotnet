# Pragmateam code challenge server (.NET)

Please refer to the provided document for the code challenge requirements.

## Framework & languages
This project uses
* .Net Core 5.0 / C#

## Available scripts
- `dotnet run` - Start the server (Port 8081)

## Improvements
- Created separate server.Tests project for Test classes.
- Solution file is created because of Tests in separate project and use that .sln file to load the project in IDE.
- Service package is created and the business logic is moved to service class.
- Configuration is moved to separate class and loading from appsettings.json (Eg: URL).
- Test cases are added and improve the test coverage ( ~100%).
- Logging added to log the external API call failures.
- Used dependency injection to inject the classes.


## Future Improvements
- Improve the Logging with different log levels in different environments.
- Add more integration test cases for unexpected failures/ exceptions from external API call.
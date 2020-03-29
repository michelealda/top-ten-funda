# top-ten-funda

![CI](https://github.com/michelealda/top-ten-funda/workflows/CI/badge.svg)

The solution is a web API that hosts a background service to pulling the information form the Funda API and presents the data through two endpoints.

Build it with `dotnet build`

Run it locally with `dotnet run --project Api/Api.csproj`

The application stores the information in memory for simplicity. Depending on how many requests are currently happening server side, the application might take a few seconds to load all the information. 
I've used [Polly](http://www.thepollyproject.org/) to handle transient errors and API overload while pulling the data. I've added an exponential 5secs delay for every retry.information

I've added a rest file that can be used with [this]((https://marketplace.visualstudio.com/items?itemName=humao.rest-client)) VS Code extension.
This file contains the simple calls to the 2 endpoints to show the top 10 lists.information

## Solution structure
**Core** contains the domain models and the definitions of the service/repositories
The aim of the domain objects is to be immutable and to be as smart as possible

**Infrastructure** contains the concrete implementations of the services plus additional helpers to generalize background service behavior

**API** hosts the services through the controllers and the bare minimum business logic to map results from the services to appropriate http responses (plain tabular text in this case)

## Next steps
- The background service is hosted by the same process that hosts the API and for testability purpose it could be refactored out on a separate service.information
- Add logging to trace events that might happen at runtime.
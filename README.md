SETUP
- Download .NET SDK 9.0.
- Clone the repo.
- From the "EveEventManager" directory, run `dotnet install` from the command line.

- Navigate to the "EveBackend" directory and run `dotnet run` from the command line.
- To test that the backend is running, navigate in a web browser to `localhost:5066/api/events` and confirm that a JSON list of events is shown on the page.

- Navigate to the "EveFrontend" directory and run `dotnet run` from the command line.
- Navigate to `localhost:5216` and confirm that the web frontend is displayed.

SETUP - AUTHENTICATION
- To use all features, you will need a Microsoft account (work or personal). Editing, deleting and RSVPing to events requires the user to be logged in.
- In the EveFrontend project, modify appsettings.json by pasting the ClientId and ClientSecret. Contact me to obtain the ClientId and ClientSecret.
- In the EveBackend project, modify appsettings.json by pasting the ClientId and ClientSecret. These values are the same as for EveFrontend.

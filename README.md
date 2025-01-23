**SETUP**
- Download .NET SDK 9.0.
- Clone the repo.
- From the "EveEventManager" directory, run `dotnet install` from the command line.

- Navigate to the "EveBackend" directory and run `dotnet run` from the command line.
- To test that the backend is running, navigate in a web browser to `localhost:5066/api/events` and confirm that a JSON list of events is shown on the page.

- Navigate to the "EveFrontend" directory and run `dotnet run` from the command line.
- Navigate to `localhost:5216` and confirm that the web frontend is displayed.


**SETUP - AUTHENTICATION**
- To use all features, you will need a Microsoft account (work or personal). Editing, deleting and RSVPing to events requires the user to be logged in.
- In the EveFrontend project, modify appsettings.json by pasting the ClientId and ClientSecret. Contact me to obtain the ClientId and ClientSecret.
- In the EveBackend project, modify appsettings.json by pasting the ClientId and ClientSecret. These values are the same as for EveFrontend.


**FEATURES**
- Access the /events page to view a list of events. If the user is logged in, events can be edited, deleted or RSVP to add the current user as an attendee. A user cannot RSVP to an event again if they are already an attendee. On a successful RSVP, a success snackbar is displayed.
  ![image](https://github.com/user-attachments/assets/6dd146df-e979-4d70-8704-135bde1d67f0)

- Create a new event. All fields have validation. The new event is added to the database and the events list is updated dynamically.
  ![image](https://github.com/user-attachments/assets/5397799d-6db1-4803-a195-7e1f0e164281)


**IMPLEMENTATION NOTES**
- EveFrontend and EveBackend are separate projects. The EveBackend API can be run independently.
- Integrates with Microsoft Azure for Sign-in with Microsoft.
- Uses SQLite as database.
- Uses MudBlazor for UI components.


**FUTURE IMPROVEMENTS**
- Ability to register as a user separately from Microsoft login. Users would be stored in a Users table in the database.
- Session management.
- Unit tests using xUnit.
- Integration tests using e.g. Selenium, Cypress.
- More detailed exception handling and handling of API error responses.
- Better backend validation of API requests.
- A more appealing UI, using MudBlazor components.

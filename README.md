# Webhooks Proof of Concept

This is a simple application that allows clients to subscribe to events by providing a URL which is called when the event occurs, AKA a **Webhook**.  

By default, there exist three events which clients can subscribe to:
1. MARKET_UP - When S&P500 goes up in a 1 hour interval
2. MARKET_DOWN - When S&P500 goes down in a 1 hour interval
3. CRASH - When the market crashes
  
Of course, the application doesn't really track these events, but they can be simulated by calling an API.

## Run the project locally

Clone the project. Run `dotnet restore` to get all the dependencies. Run `dotnet ef database update` to set up the database. Finally `dotnet run` to start the project.
  
By default the project starts on port **8585** and if your browser doesn't launch automatically for some reason, visit *http://localhost:8585/swagger/* to explore the endpoints

## Endpoints

The endpoints in Swagger should be pretty self-explanatory.
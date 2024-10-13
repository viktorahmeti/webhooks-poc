# Webhooks Proof of Concept

This is a simple application that allows clients to subscribe to events by providing a URL which is called when the event occurs, AKA a **Webhook**.  

By default, there exist three events which clients can subscribe to:
1. MARKET_UP - When S&P500 goes up in a 1 hour interval
2. MARKET_DOWN - When S&P500 goes down in a 1 hour interval
3. CRASH - When the market crashes
  
Of course, the application doesn't really track these events, but they can be simulated by clicking a button or calling an API.

## Run the Backend

Clone the project. Assuming you have a .NET environment ready, in the **Backend** folder Run `dotnet restore` to get all the dependencies. Run `dotnet ef database update` to set up the database. Finally `dotnet run` to start the Backend.
  
By default the Backend starts on port **8585** and if your browser doesn't launch automatically for some reason, visit *http://localhost:8585/swagger/* to explore the endpoints

## Run the Frontend

Assuming you have cloned the project, go to the **Frontend** folder and run `npm install` to get all the required dependencies. Now run `npm start` to start the frontend.

## How to use the web app

In the main page click **Add New Webhook**, select the Event you want to subscribe to, and write the webhook URL. When you click **Add** everything will be set up.  

The button **Dispatch EVENT_TYPE** simulates the dispatching of that event type, so when you click it your endpoint should be called.
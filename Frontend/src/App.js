import './App.css';
import WebhookList from './WebhookList/WebhookList';
import React, { useState, useEffect } from 'react';

function App() {
  const [events, setEvents] = useState([]);

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = async () => {
    const SEVER_API_PATH = "http://localhost:8585/api";

    try{
      const response = await fetch(SEVER_API_PATH + '/Event');
      const data = await response.json();
      setEvents(data);
    }
    catch(err){
      console.error(err);
    }
  }

  return (
    <div className="App">
      <h1>Webhooks</h1>
      <div className="Webhook-Lists-Container">
        {events.map(e => {
          return <WebhookList key={e.eventId} event={e}></WebhookList>
        })}
      </div>
    </div>
  );
}

export default App;

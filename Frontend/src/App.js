import './App.css';
import WebhookList from './WebhookList/WebhookList';
import WebhookDialog from './WebhookDialog/WebhookDialog';
import React, { useState, useEffect } from 'react';
import { SEVER_API_PATH } from './utils';

function App() {
  const [events, setEvents] = useState([]);
  const [showWebhookDialog, setShowWebhookDialog] = useState(false);

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = async () => {
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
          return <WebhookList fetchEvents={fetchEvents} key={e.eventId} event={e}></WebhookList>
        })}
      </div>
      <button onClick={() => setShowWebhookDialog(true)} className='Add-New-Webhook-Btn'>+ Add Webhook</button>
      {showWebhookDialog && <WebhookDialog events={events} setShow={setShowWebhookDialog} fetchEvents={fetchEvents}></WebhookDialog>}
    </div>
  );
}

export default App;

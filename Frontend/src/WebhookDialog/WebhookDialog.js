import { useState } from 'react';
import './WebhookDialog.css';
import { SEVER_API_PATH } from '../utils';

function WebhookDialog({events, setShow, fetchEvents}){
    const [selectedEvent, setSelectedEvent] = useState(null);
    const [webhook, setWebhook] = useState('');

    function closeDialog(event){
        if(!event || event.target == document.querySelector('.Webhook-Dialog-Background'))
            setShow(false);
    }

    async function createWebhook(){
        //validate empty
        if (!selectedEvent || !webhook){
            alert('Please fill out the form before submitting');
            return;
        }

        //validate url
        try {
            let url = new URL(webhook);
            if (url.protocol !== "http:" && url.protocol !== "https:"){
                alert('Please provide a valid URL');
                return;
            }
        } catch (_) {
            alert('Please provide a valid URL');
            return;
        }

        closeDialog();

        await fetch(SEVER_API_PATH + '/Webhook', {
            method: 'POST',
            body: JSON.stringify({
                eventId: selectedEvent,
                endpoint: webhook
            }),
            headers: new Headers({'content-type': 'application/json'})
        });

        fetchEvents();
    }

    return (
        <div onClick={closeDialog} className='Webhook-Dialog-Background'>
            <div className='Webhook-Dialog'>
                <select onChange={(e) => setSelectedEvent(e.target.value)}>
                    <option value="" disabled selected>Event Type</option>
                    {events.map(e => <option key={e.eventId} value={e.eventId}>{e.name}</option>)}
                </select>
                <input type='url' value={webhook} onChange={e => setWebhook(e.target.value)} placeholder="Webhook URL"></input>
                <button onClick={createWebhook} className='Confirm-New-Webhook-Btn'>Add</button>
            </div>
        </div>
    );
}

export default WebhookDialog;
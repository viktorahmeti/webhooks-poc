import './WebhookList.css';
import { DispatchEvent, SEVER_API_PATH } from '../utils';

function WebhookList({event, fetchEvents}){
    async function deleteWebhook(webhook){
        if (window.confirm(`Are you sure you want to DELETE the webhook ${webhook.endpoint}?`)){
            await fetch(SEVER_API_PATH + `/Webhook/${webhook.id}`,
                {
                    method: "DELETE"
                }
            )
            fetchEvents();
        }
    }

    if(event.webhooks !== null && event.webhooks.length !== 0){
        return <div key={event.eventId} className="Webhook-List-Container">
                <div className="Webhook-List-Top">
                    <div title={event.description} className="Webhook-List-Header">
                        ({event.name})
                    </div>
                    <ul className="Webhook-List">
                        {event.webhooks.map(w => <li onClick={() => deleteWebhook(w)} className='Webhook-Item' key={w.id}>{w.endpoint}</li>)}
                    </ul>
                </div>
                <button onClick={() => DispatchEvent(event.eventId)} className="Dispatch-Button">
                    Dispatch {event.name}
                </button>
            </div>
    }
    else{
        return null;
    }
}

export default WebhookList;
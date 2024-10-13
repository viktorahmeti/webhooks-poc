import './WebhookList.css';
import { DispatchEvent } from '../utils';

function WebhookList({event}){
    if(event.webhooks !== null && event.webhooks.length !== 0){
        return <div key={event.eventId} className="Webhook-List-Container">
                <div className="Webhook-List-Top">
                    <div className="Webhook-List-Header">
                        ({event.name}) - {event.description}
                    </div>
                    <ul className="Webhook-List">
                        {event.webhooks.map(w => <li key={w.id}>{w.endpoint}</li>)}
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
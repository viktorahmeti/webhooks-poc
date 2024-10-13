export const SEVER_API_PATH = "http://localhost:8585/api";

export async function DispatchEvent(eventId){
    try{
        await fetch(SEVER_API_PATH + `/Event/dispatch/${eventId}`, {
            method: "POST"
        });
    }
    catch(err){
        console.error(err);
    }
}
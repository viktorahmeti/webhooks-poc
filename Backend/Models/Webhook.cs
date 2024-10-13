namespace WebHooks.Models;

public class Webhook{
    public long Id {get; set;}
    public long EventId;    
    public Event Event {get; set;} = null!;
    public Uri Endpoint {get; set;} = null!;
}
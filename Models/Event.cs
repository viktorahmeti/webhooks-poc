namespace WebHooks.Models;

public class Event{
    public long Id {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public ICollection<Webhook> Webhooks {get;} = new List<Webhook>();
}
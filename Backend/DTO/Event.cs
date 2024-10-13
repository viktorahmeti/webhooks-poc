namespace WebHooks.DTO;

public class EventDTO{
    public required long EventId {get; set;}
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required string occurredAt {get; set;}
}

public class EventDTOWithWebhooks : EventDTO{
    public required ICollection<WebhookSimpleDTO> webhooks {get; set;}
}
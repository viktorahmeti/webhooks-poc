namespace WebHooks.DTO;

public class WebhookRequestDTO{
    public required long EventId {get; set;}
    public required string Endpoint {get; set;}
}

public class WebhookResponseDTO{
    public required long Id {get; set;}
    public required string Endpoint {get; set;}
    public required string EventName {get; set;}
    public required string EventDescription {get; set;}
}

public class WebhookSimpleDTO{
    public required long Id {get; set;}
    public required string Endpoint {get; set;}
}
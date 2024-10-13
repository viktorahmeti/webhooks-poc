using System.Collections.ObjectModel;
using WebHooks.DTO;
using WebHooks.Models;

namespace WebHooks.Mappers;

public static class EventMapperExtension{
    public static EventDTO MapToEventDTO(this Event e){
        return new EventDTO{
            EventId = e.Id,
            Name = e.Name,
            Description = e.Description,
            occurredAt = DateTime.Now.ToString()
        };
    }

    public static EventDTOWithWebhooks MapToEventDTOWithWebhooks(this Event e){
        return new EventDTOWithWebhooks{
            EventId = e.Id,
            Name = e.Name,
            Description = e.Description,
            occurredAt = DateTime.Now.ToString(),
            webhooks = new Collection<WebhookSimpleDTO>(e.Webhooks.Select(w => w.MapToWebhookSimpleDto()).ToList())
        };
    }
}
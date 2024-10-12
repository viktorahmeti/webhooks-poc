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
}
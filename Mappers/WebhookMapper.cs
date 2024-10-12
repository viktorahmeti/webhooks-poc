using WebHooks.DTO;
using WebHooks.Models;

namespace WebHooks.Mappers;

public static class WebhookMapperExtension{
    public static Webhook MapToWebhookEntity(this WebhookRequestDTO dto){
        return new Webhook{
            EventId = dto.EventId,
            Endpoint = new Uri(dto.Endpoint)
        };
    }

    public static WebhookResponseDTO MapToWebhookDto(this Webhook entity){
        return new WebhookResponseDTO{
            Id = entity.Id,
            Endpoint = entity.Endpoint.AbsoluteUri,
            EventName = entity.Event.Name,
            EventDescription = entity.Event.Description
        };
    }
}
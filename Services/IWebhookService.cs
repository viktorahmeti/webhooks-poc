using WebHooks.DTO;
using WebHooks.Models;

namespace WebHooks.Services;

public interface IWebhookService{
    Task<Webhook> CreateWebhook(Webhook dto);
    Task<ICollection<Webhook>> GetWebhooks();
}
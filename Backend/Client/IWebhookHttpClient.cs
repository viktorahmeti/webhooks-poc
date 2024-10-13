using WebHooks.Models;

namespace WebHooks.Client;

public interface IWebhookHttpClient{
    void CallWebhooks(Event e);
}
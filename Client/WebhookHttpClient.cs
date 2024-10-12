using System.Text;
using System.Text.Json.Serialization;
using WebHooks.DTO;
using WebHooks.Mappers;
using WebHooks.Models;

namespace WebHooks.Client;

public class WebhookHttpClient : IWebhookHttpClient{
    private readonly IHttpClientFactory _httpClientFactory;

    public WebhookHttpClient(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
    }

    public async void CallWebhooks(Event e){
        EventDTO dto = e.MapToEventDTO();
        string dtoAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(dto);

        Console.WriteLine($"Here is the data for the event: {dto.Name}");
        foreach (var webhook in e.Webhooks){
            using (HttpClient client = _httpClientFactory.CreateClient()){
                await client.PostAsync(webhook.Endpoint, new StringContent(dtoAsJson, Encoding.UTF8, "application/json"));
            }
        }
    }
}
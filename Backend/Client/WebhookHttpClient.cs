using System.Text;
using WebHooks.DTO;
using WebHooks.Mappers;
using WebHooks.Models;

namespace WebHooks.Client;

public class WebhookHttpClient : IWebhookHttpClient{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SemaphoreSlim _semaphore;
    private readonly int MAX_CONCURRENT_CALLS = 200;

    public WebhookHttpClient(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        //Using semaphores, as specified
        //https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-8.0
        _semaphore = new SemaphoreSlim(MAX_CONCURRENT_CALLS, MAX_CONCURRENT_CALLS);
    }

    public async void CallWebhooks(Event e){
        EventDTO dto = e.MapToEventDTO();
        string dtoAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
        StringContent eventPayload = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        var tasks = new List<Task>();

        foreach(var webhook in e.Webhooks){
            await _semaphore.WaitAsync();

            tasks.Add(Task.Run(async () => {
                try{
                    HttpClient client = _httpClientFactory.CreateClient("WebhookClient");
                    var response = await client.PostAsync(webhook.Endpoint, eventPayload);
                    response.EnsureSuccessStatusCode();
                }
                catch(Exception ex){
                    //handle error
                    Console.WriteLine(ex.ToString());
                }
                finally{
                    _semaphore.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);

        // Basic one-by-one implementation
        // foreach (var webhook in e.Webhooks){
        //     using (HttpClient client = _httpClientFactory.CreateClient()){
        //         await client.PostAsync(webhook.Endpoint, new StringContent(dtoAsJson, Encoding.UTF8, "application/json"));
        //     }
        // }
    }
}
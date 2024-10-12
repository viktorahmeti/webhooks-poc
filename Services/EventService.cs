using Microsoft.EntityFrameworkCore;
using WebHooks.Client;
using WebHooks.Database;
using WebHooks.Models;

namespace WebHooks.Services;

public class EventService : IEventService
{
    private readonly WebhookServiceContext _dbContext;
    private readonly IWebhookHttpClient _webhookHttpClient;

    public EventService(WebhookServiceContext dbContext, 
                        IWebhookHttpClient webhookHttpClient){
        _dbContext = dbContext;
        _webhookHttpClient = webhookHttpClient;
    }

    public async void DispatchEvent(long eventId)
    {
        Event? theEvent = await _dbContext.Events.Include(e => e.Webhooks).SingleOrDefaultAsync(e => e.Id == eventId);

        if (theEvent is null){
            //implement bad request
            return;
        }

        _webhookHttpClient.CallWebhooks(theEvent);
    }

    public async Task<Event> GetEvent(long eventId)
    {
        return await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == eventId);
    }

    public async Task<ICollection<Event>> GetEvents()
    {
        return await _dbContext.Events.ToListAsync();
    }
}
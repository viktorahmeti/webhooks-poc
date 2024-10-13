using Microsoft.EntityFrameworkCore;
using WebHooks.Database;
using WebHooks.Models;

namespace WebHooks.Services;

public class WebhookService : IWebhookService{
    private readonly WebhookServiceContext _dbContext;

    public WebhookService(WebhookServiceContext dbContext){
        _dbContext = dbContext;
    }

    public async Task<Webhook> CreateWebhook(Webhook webhook){
        Console.WriteLine(webhook.Endpoint.AbsoluteUri);
        await _dbContext.Webhooks.AddAsync(webhook);
        await _dbContext.SaveChangesAsync();
        return await GetWebhookById(webhook.Id);
    }

    public async Task<Webhook> GetWebhookById(long id){
        return await _dbContext.Webhooks.Include(w => w.Event).SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task<ICollection<Webhook>> GetWebhooks()
    {
        return await _dbContext.Webhooks.Include(w => w.Event).ToListAsync();
    }

    public async Task<ICollection<Webhook>> GetWebhooksForEvent(long eventId)
    {
        return await _dbContext.Webhooks.Where(w => w.EventId == eventId).ToListAsync();
    }

    public async Task DeleteWebhook(long id){
        Webhook? webhook = await _dbContext.Webhooks.SingleOrDefaultAsync(w => w.Id == id);

        if (webhook is not null){
            _dbContext.Webhooks.Remove(webhook);
            await _dbContext.SaveChangesAsync();
        }
    }
}
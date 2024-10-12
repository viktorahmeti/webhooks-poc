using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHooks.Database;
using WebHooks.Models;

namespace WebHooks.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class EventController : ControllerBase{
    private readonly WebhookServiceContext _dbContext;

    public EventController(WebhookServiceContext dbContext){
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("/event")]
    public async Task<ICollection<Event>> GetEvents(){
        return await _dbContext.Events.ToListAsync();
    }

    [HttpGet]
    [Route("/event/{eventId}")]
    public async Task<Event> GetEvent(long eventId){
        return await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == eventId);
    }

    [HttpPost]
    [Route("/dispatch/{eventId}")]
    public void DispatchEvent(long eventId)
    {
        //still not implemented
    }
}
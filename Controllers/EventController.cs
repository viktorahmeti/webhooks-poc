using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHooks.Database;
using WebHooks.Models;
using WebHooks.Services;

namespace WebHooks.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class EventController : ControllerBase{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService){
        _eventService = eventService;
    }

    [HttpGet]
    [Route("/event")]
    public async Task<ICollection<Event>> GetEvents(){
        return await _eventService.GetEvents();
    }

    [HttpGet]
    [Route("/event/{eventId}")]
    public async Task<Event> GetEvent(long eventId){
        return await _eventService.GetEvent(eventId);
    }

    [HttpPost]
    [Route("/dispatch/{eventId}")]
    public void DispatchEvent(long eventId)
    {
        _eventService.DispatchEvent(eventId);
    }
}
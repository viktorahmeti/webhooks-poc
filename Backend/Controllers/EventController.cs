using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHooks.Database;
using WebHooks.DTO;
using WebHooks.Mappers;
using WebHooks.Models;
using WebHooks.Services;

namespace WebHooks.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventController : ControllerBase{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService){
        _eventService = eventService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ICollection<EventDTOWithWebhooks>> GetEvents(){
        ICollection<Event> eventEntities = await _eventService.GetEvents();

        if (eventEntities is null)
            return [];

        //include the webhooks of each event
        return eventEntities.Select(e => e.MapToEventDTOWithWebhooks()).ToList();
    }

    [HttpGet]
    [Route("{eventId}")]
    public async Task<ActionResult<EventDTO>> GetEvent(long eventId){
        Event? eventEntity = await _eventService.GetEvent(eventId);

        if (eventEntity is null)
            return NotFound();

        return eventEntity.MapToEventDTO();
    }

    [HttpPost]
    [Route("dispatch/{eventId}")]
    public async Task<ActionResult> DispatchEvent(long eventId)
    {
        bool wrongId = await _eventService.DispatchEvent(eventId);

        if (wrongId)
            return NotFound();
        
        return Ok();
    }
}
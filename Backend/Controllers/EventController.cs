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
        return (await _eventService.GetEvents()).Select(e => e.MapToEventDTOWithWebhooks()).ToList();
    }

    [HttpGet]
    [Route("{eventId}")]
    public async Task<EventDTO> GetEvent(long eventId){
        return (await _eventService.GetEvent(eventId)).MapToEventDTO();
    }

    [HttpPost]
    [Route("dispatch/{eventId}")]
    public void DispatchEvent(long eventId)
    {
        _eventService.DispatchEvent(eventId);
    }
}
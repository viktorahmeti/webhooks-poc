using WebHooks.Models;

namespace WebHooks.Services;

public interface IEventService{
    Task<Event> GetEvent(long eventId);
    Task<ICollection<Event>> GetEvents();
    void DispatchEvent(long eventId);
}
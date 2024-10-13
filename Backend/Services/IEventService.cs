using WebHooks.Models;

namespace WebHooks.Services;

public interface IEventService{
    Task<Event?> GetEvent(long eventId);
    Task<ICollection<Event>> GetEvents();
    Task<bool> DispatchEvent(long eventId);
}
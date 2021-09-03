using System.Collections.Generic;

namespace CalendarAPI.Controllers
{
    public interface ICalendarService
    {
        IAsyncEnumerable<Event> GetEvents();
    }
}
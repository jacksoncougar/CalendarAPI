using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAPI.Controllers
{
    [Route("identity")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService CalendarService;

        public CalendarController(ICalendarService calendarService)
        {
            CalendarService = calendarService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var calendar = new Calendar();
            calendar.Properties.Add(new CalendarProperty("X-WR-CALNAME", "My calendar #1"));

            await foreach (var @event in this.CalendarService.GetEvents())
            {
                var e = new CalendarEvent
                {
                    Summary =@event.Summary,
                    Attendees = new List<Attendee>(@event.Attendees.Select(x => new Attendee(x))),
                    Start = new CalDateTime(@event.Start),
                    End = new CalDateTime(@event.End),
                    IsAllDay = true
                };
                calendar.Events.Add(e);
            }

            var serializer = new CalendarSerializer();
            var serializedCalendar1 = serializer.SerializeToString(calendar);

            return File(Encoding.ASCII.GetBytes($"{serializedCalendar1}"), "text/calendar", "calendar.ics");
        }
    }
}

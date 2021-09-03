using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;

namespace CalendarAPI.Controllers
{
    public class Event
    {
        public string Summary
        {
            get; init;
        }
        public DateTime Start
        {
            get; init;
        }
        public DateTime End
        {
            get; init;
        }
        public IEnumerable<Uri> Attendees
        {
            get;
            init;
        }
    }
}
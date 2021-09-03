using Bogus;
using System;
using System.Collections.Generic;

namespace CalendarAPI.Controllers
{
    public class BogusCalendarService : ICalendarService
    {
        public async IAsyncEnumerable<Event> GetEvents()
        {
            Faker<Event> faker = new Faker<Event>();
            faker.RuleFor(o => o.Summary, f => f.Person.FullName)
                .RuleFor(o => o.Start, f => f.Date.Between(DateTime.Now.Date, DateTime.Now.AddDays(31).Date))
                .RuleFor(o => o.End, (f, u) => u.Start.AddHours(f.Random.Int(1, 3)))
                .RuleFor(o => o.Attendees, (f, u) => f.Make(f.Random.Int(1,5), () => new Uri($"mailto:{f.Internet.Email(null,null, "example.com")}")));

            foreach (var fake in faker.Generate(80))
            {
                yield return fake;
            }
        }



    }
}
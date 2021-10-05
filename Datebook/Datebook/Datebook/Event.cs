using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebook
{
    public class Event
    {
        public Guid EventId { get; private set; }
        public DateTime EventCreationDate { get; private set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventDuration { get; set; }
        public string EventDescription { get; set; }
        public string EventPlace { get; set; }


        public Event(DateTime eventDate, TimeSpan eventDuration, string eventDescription, string eventPlace)
        {
            EventId = Guid.NewGuid();
            this.EventDate = eventDate;
            this.EventDuration = eventDuration;
            this.EventDescription = eventDescription;
            this.EventPlace = eventPlace;
            EventCreationDate = DateTime.Now;
            
        }

        public void WriteFullEvent()
        {
            Console.WriteLine($"{EventDate} {EventDuration} {EventDescription} {EventPlace} {EventCreationDate}");
        }
    }
}

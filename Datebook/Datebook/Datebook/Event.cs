using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebook
{
    public class Event
    {
        private Guid eventId;
        private DateTime eventDate;
        private TimeSpan eventDuration;
        private string eventDescription;
        private string eventPlace;
        private DateTime entryCreationDate;


        public Event(DateTime date, TimeSpan duration, string description, string place)
        {
            eventId = Guid.NewGuid();
            this.eventDate = date;
            this.eventDuration = duration;
            this.eventDescription = description;
            this.eventPlace = place;
            entryCreationDate = DateTime.Now;
            
        }
    }
}

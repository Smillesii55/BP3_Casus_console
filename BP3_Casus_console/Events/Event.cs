using BP3_Casus_console.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Events
{
    public class Event
    {
        public int ID { get; set; }
        public int EventTypeID { get; set; }  // Verwijzing naar EventType
        public DateTime Date { get; set; }
        public int MaxParticipants { get; set; }

        public Coach Coach { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();


        public Event(Coach coach, double expPerParticipant, DateTime Date, int MaxParticipants)
        {
            Coach = coach;
            this.Date = Date;
            this.MaxParticipants = MaxParticipants;
        }
    }
}

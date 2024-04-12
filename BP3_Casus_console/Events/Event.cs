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
        public string Name { get; set; }
        public double ExpPerParticipant { get; set; }
        public List<String> Tags { get; set; } = new List<string>();

        public Event(string name, double expPerParticipant)
        {
            Name = name;
            ExpPerParticipant = expPerParticipant;
        }
    }
}

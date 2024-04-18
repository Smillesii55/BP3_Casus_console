using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Events
{
    public class EventType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public double ExpPerParticipant { get; set; }

     
        public List<String> Tags { get; set; } = new List<string>();


        public EventType(string name, string description, double expPerParticipant)
        {
            Name = name;
            Description = description;
            ExpPerParticipant = expPerParticipant;
        }
    }
}

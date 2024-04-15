using BP3_Casus_console.Events.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Events
{
    public class EventProgress
    {
        public int EventID { get; set; }
        public int UserID { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; } = 0;

        public Event @event { get; set; }

        EventService EventService = EventService.Instance;
        public EventProgress(int eventID, int userID)
        {
            EventID = eventID;
            @event = EventService.GetEventById(eventID);
            UserID = userID;
        }

        public void GainExperience(double experience)
        {
            Experience += experience;

            while (Experience >= ExperienceToNextLevel(Level))
            {
                Level++;
            }


        }
        public Double ExperienceToNextLevel(int Level)
        {
            // Baseline experience (30 XP)
            int baselineXP = 30;

            // Increment for the first three levels (3 XP each)
            int incrementalXP = 3 * (Level - 1);

            // Exponential growth starting from level 4
            int exponentialXP = 10 * (int)Math.Pow(2, Level - 4);

            // Total max XP for the given level
            int maxXP = baselineXP + incrementalXP + exponentialXP;

            return maxXP;
        }
    }
}

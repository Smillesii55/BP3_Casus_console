using System;
using System.Collections.Generic;
using System.Linq;
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

        public EventProgress(int eventID, int userID)
        {
            EventID = eventID;
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
            return 100 * Level;
        }
    }
}

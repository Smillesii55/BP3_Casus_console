using BP3_Casus_console.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users
{
    public class Participant : User
    {
        public int GeneralLevel { get; set; } = 1;
        public double GeneralExperience { get; set; } = 0;
        public List<EventProgress> EventProgresses { get; set; } = new List<EventProgress>();

        public Participant(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth) : base(username, password, email, firstName, lastName, dateOfBirth)
        {
            Type = UserType.Participant;
        }

        public void GainGeneralExperience(double xp)
        {
            this.GeneralExperience += xp;
            while (this.GeneralExperience >= ExperienceNeededForGeneralLevel(this.GeneralLevel + 1))
            {
                this.GeneralLevel++;
                // Optional: Trigger some notification or reward for leveling up
            }
        }

        public double ExperienceNeededForGeneralLevel(int level)
        {
            return 100 * level; // Adjust the formula as needed for general progression
        }

        public void ParticipateInEvent(Event @event)
        {
            EventProgress? progress = EventProgresses.FirstOrDefault(p => p.EventID == @event.ID);
            if (progress == null)
            {
                progress = new EventProgress(@event.ID);
                EventProgresses.Add(progress);
            }

            progress.GainExperience(@event.ExpPerParticipant);
        }
    }
}

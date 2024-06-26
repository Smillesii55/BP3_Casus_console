﻿using BP3_Casus_console.Events;
using BP3_Casus_console.Events.Service;
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
        public List<EventTypeProgress> EventProgresses { get; set; } = new List<EventTypeProgress>();

        EventService EventService = EventService.Instance;

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
            // Baseline experience (30 XP)
            int baselineXP = 30;

            // Increment for the first three levels (3 XP each)
            int incrementalXP = 3 * (level - 1);

            // Exponential growth starting from level 4
            int exponentialXP = 10 * (int)Math.Pow(2, level - 4);

            // Total max XP for the given level
            int maxXP = baselineXP + incrementalXP + exponentialXP;

            return maxXP;
        }

        // KLOPT NIET MEER!
        public void ParticipateInEvent(Event @event)
        {
            EventTypeProgress? progress = EventProgresses.FirstOrDefault(p => p.ID == @event.ID);
            if (progress == null)
            {
                progress = new EventTypeProgress(@event.ID, this.ID);
                EventProgresses.Add(progress);
            }
        }
    }
}

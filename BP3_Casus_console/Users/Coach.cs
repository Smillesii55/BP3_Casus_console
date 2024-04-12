using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP3_Casus_console.Events;

namespace BP3_Casus_console.Users
{
    public class Coach : User
    {
        public AreaOfExpertise Expertise { get; set; }
        public enum AreaOfExpertise
        {
            VariabeleSport, 
            VrijeKeuze, 
            Kickboxen, 
            KungFu, 
            Bodytraining, 
            Yoga
        }

        public Coach(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, AreaOfExpertise expertise) : base(username, password, email, firstName, lastName, dateOfBirth)
        {
            Type = UserType.Coach;
            Expertise = expertise;
        }
        

        public void GradeParticipantForEvent(Participant participant, Event @event, int grade = 6)
        {
            double expGained = @event.ExpPerParticipant * (grade / 10.0);
            participant.GainGeneralExperience((expGained / 2));
            EventProgress? progress = participant.EventProgresses.FirstOrDefault(p => p.EventID == @event.ID);
            if (progress != null)
            {
                progress.GainExperience(expGained);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users
{
    public class Coach : User
    {
        public AreaOfExpertise Expertise { get; set; }
        public enum AreaOfExpertise
        {
            Fitness,
            Nutrition,
            MentalHealth,
            Sports
                // ATTENTION: Add more areas of expertise here!
                // According to the requirements.
        }

        public Coach(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, AreaOfExpertise expertise) : base(username, password, email, firstName, lastName, dateOfBirth)
        {
            Type = UserType.Coach;
            Expertise = expertise;
        }
    }
}

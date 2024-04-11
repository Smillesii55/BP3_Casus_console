using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users
{
    public class User
    {
        public int ID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime CreationDate { get; set; }

        public UserType Type { get; set; }
        public enum UserType
        {
            Participant,
            Coach,
            Admin
        }

        public User(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth)
        {
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}

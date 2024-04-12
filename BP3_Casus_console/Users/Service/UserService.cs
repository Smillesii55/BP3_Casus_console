using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users.Service
{
    public class UserService
    {
        UserDataAccesLayer userDataAccesLayer = UserDataAccesLayer.Instance;
        private UserService()
        {
            // hallo dit is een test.
        }

        private static UserService? instance = null;
        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
        }

        public User CreateUser(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth)
        {
            Participant participant = new Participant(username, password, email, firstName, lastName, dateOfBirth);
            userDataAccesLayer.InsertUser(participant);
            return participant;
        }
        public User CreateUser(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, Coach.AreaOfExpertise expertise)
        {
            Coach coach = new Coach(username, password, email, firstName, lastName, dateOfBirth, expertise);
            userDataAccesLayer.InsertUser(coach);
            return coach;
        }

        public void UpdateUserProfile(int userId, User updatedProfile)
        {
            // Retrieve the user from the database
            // Update the user's profile with the updatedProfile information
            // Save changes to the database
        }

        public void DeleteUserProfile(int userId)
        {
            // Retrieve the user from the database
            // Delete the user from the database
        }

        public User GetUserProfileById(int userId)
        {
            // Retrieve the user from the database
            return new Participant("username", "password", "email", "firstName", "lastName", DateTime.Now); // Placeholder
        }
        public User GetUserProdileByUsername(string username)
        {
            // Retrieve the user from the database
            return new Participant("username", "password", "email", "firstName", "lastName", DateTime.Now); // Placeholder
        }

        public User Login(string username, string password)
        {
            User? user = userDataAccesLayer.GetUserByUserCredentials(username, password);
            if (user != null)
            {
                return user;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid username or password.");
                return null;
            }
        }
    }
}

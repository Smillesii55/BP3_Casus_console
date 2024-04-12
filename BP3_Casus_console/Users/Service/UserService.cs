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

        public void UpdateUserProfile(User updatedProfile)
        {
            userDataAccesLayer.UpdateUser(updatedProfile);
        }

        public void DeleteUserProfile(User userToDelete)
        {
            userDataAccesLayer.DeleteUser(userToDelete);
        }

        public User GetUserProfileById(int userId)
        {
            User? user = userDataAccesLayer.GetUserById(userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("User not found.");
                return null;
            }
        }
        public User GetUserProdileByUsername(string username)
        {
            User? user = userDataAccesLayer.GetUserByUsername(username);
            if (user != null)
            {
                return user;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("User not found.");
                return null;
            }
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

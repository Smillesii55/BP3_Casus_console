using BP3_Casus_console.Events.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BP3_Casus_console.Users.Friends;
using BP3_Casus_console.Events;

namespace BP3_Casus_console.Users.Service
{
    public class UserDataAccesLayer
    {
        // To clarify how the UserDataAccesLayer should communicate with the database, here is how the database tables are built and how the data is inserted into them:
        // use the following SQL queries to create the necessary tables and insert data into them:

        /* -- THE DESIGN OF THE USER TABLE! --

        CREATE TABLE Users (
            UserID INT PRIMARY KEY IDENTITY,
            Username VARCHAR(255) NOT NULL,
            Password VARCHAR(255) NOT NULL,
            Email VARCHAR(255) NOT NULL,
            FirstName VARCHAR(255),
            LastName VARCHAR(255),
            DateOfBirth DATE,
            UserType VARCHAR(50), -- Used to distinguish between Participant and Coach
            GeneralLevel INT, -- NULL for coaches
            GeneralExperience INT, -- NULL for coaches
            Expertise VARCHAR(50) -- NULL for participants, enum values for Coach
        );

        ALTER TABLE Users
        ADD CreationDate DATETIME NOT NULL DEFAULT GETDATE();
        */

        /* -- THE CONSTRAINT THAT IS ATTACHED TO THIS TABLE! --
        CREATE FUNCTION Check_Participant
        (
            @UserType VARCHAR(50),
            @GeneralLevel INT,
            @GeneralExperience INT,
            @Expertise VARCHAR(50)
        )
        RETURNS BIT
        AS
        BEGIN
            IF @UserType = 'Participant' AND (@GeneralLevel IS NOT NULL AND @GeneralExperience IS NOT NULL AND @Expertise IS NULL)
                RETURN 1 -- Valid Participant
            IF @UserType != 'Participant' AND @Expertise IS NOT NULL
                RETURN 1 -- Valid Coach
            RETURN 0 -- Invalid
        END

        ALTER TABLE Users
        ADD CONSTRAINT CHK_User_Participant_Coach
        CHECK (dbo.Check_Participant(UserType, GeneralLevel, GeneralExperience, Expertise) = 1);
        */

        /*
        -- CREATE A PARTICIPANT USER! --
        INSERT INTO Users (Username, Password, Email, FirstName, LastName, DateOfBirth, UserType, GeneralLevel, GeneralExperience, Expertise, CreationDate)
        VALUES ('alexjohnson', 'securePass123', 'alex.johnson@example.com', 'Alex', 'Johnson', '1995-03-15', 'Participant', 3, 12, NULL, DEFAULT);

        -- UPDATE THE PARTICIPANT USER! --
        UPDATE Users
        SET DateOfBirth = '1996-04-10'
        WHERE UserID = 2; -- THIS COULD ALSO BE DONE WITH FOR EXAMPLE THE FIRSTNAME AND LASTNAME, HOWEVER FOR STABILITY THIS IS NOT RECOMENDED --
        */

        /*
        -- CREATE A COACH USER! --
        INSERT INTO Users (Username, Password, Email, FirstName, LastName, DateOfBirth, UserType, GeneralLevel, GeneralExperience, Expertise, CreationDate)
        VALUES ('sgreen_nutrition', 'pass456Secure', 's.green@example.com', 'Samantha', 'Green', '1988-07-22', 'Coach', NULL, NULL, 'Nutrition', DEFAULT);
        */

        /*
        -- UPDATE THE COACH USER! --
        UPDATE Users
        SET FirstName = 'Erica',
            LastName = 'Wells',
            Expertise = 'Fitness',
            Email = 'e.wells@example.com'
        WHERE UserID = 1; -- THIS COULD ALSO BE DONE WITH FOR EXAMPLE THE FIRSTNAME AND LASTNAME, HOWEVER FOR STABILITY THIS IS NOT RECOMENDED --
        */

        /*
        -- DELETING USERS FROM USERNAME! --
        DELETE FROM Users
        WHERE Username = 'vincentvega';

        DELETE FROM Users
        WHERE Username = 'marcelluswallace';
        */

        private string connectionString = "Server=.;Database=BP3Casus;Trusted_Connection=True;";

        private UserDataAccesLayer()
        {
        }

        private static UserDataAccesLayer? instance = null;
        public static UserDataAccesLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDataAccesLayer();
                }
                return instance;
            }
        }

        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password, Email, FirstName, LastName, DateOfBirth, UserType, GeneralLevel, GeneralExperience, Expertise) VALUES (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @UserType, @GeneralLevel, @GeneralExperience, @Expertise)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserType", user.Type.ToString());

                    if (user.Type == User.UserType.Participant)
                    {
                        command.Parameters.AddWithValue("@GeneralLevel", (user as Participant).GeneralLevel);
                        command.Parameters.AddWithValue("@GeneralExperience", (user as Participant).GeneralExperience);
                        command.Parameters.AddWithValue("@Expertise", DBNull.Value);
                    }
                    else if (user.Type == User.UserType.Coach)
                    {
                        command.Parameters.AddWithValue("@GeneralLevel", DBNull.Value);
                        command.Parameters.AddWithValue("@GeneralExperience", DBNull.Value);
                        command.Parameters.AddWithValue("@Expertise", (user as Coach).Expertise.ToString());
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Find the user by the id and update the user's information
                string query = "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, UserType = @UserType, GeneralLevel = @GeneralLevel, GeneralExperience = @GeneralExperience, Expertise = @Expertise WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", user.ID);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserType", user.Type.ToString());

                    if (user.Type == User.UserType.Participant)
                    {
                        command.Parameters.AddWithValue("@GeneralLevel", (user as Participant).GeneralLevel);
                        command.Parameters.AddWithValue("@GeneralExperience", (user as Participant).GeneralExperience);
                        command.Parameters.AddWithValue("@Expertise", DBNull.Value);
                    }
                    else if (user.Type == User.UserType.Coach)
                    {
                        command.Parameters.AddWithValue("@GeneralLevel", DBNull.Value);
                        command.Parameters.AddWithValue("@GeneralExperience", DBNull.Value);
                        command.Parameters.AddWithValue("@Expertise", (user as Coach).Expertise.ToString());
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", user.ID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public User? GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string username = reader.GetString(reader.GetOrdinal("Username"));
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            DateTime dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                            User.UserType userType = (User.UserType)Enum.Parse(typeof(User.UserType), reader.GetString(reader.GetOrdinal("UserType")));

                            if (userType == User.UserType.Participant)
                            {
                                int generalLevel = reader.GetInt32(reader.GetOrdinal("GeneralLevel"));
                                int generalExperience = reader.GetInt32(reader.GetOrdinal("GeneralExperience"));

                                Participant participant = new Participant(username, password, email, firstName, lastName, dateOfBirth);
                                participant.ID = userId;
                                participant.GeneralLevel = generalLevel;
                                participant.GeneralExperience = generalExperience;
                                GetEventProgressesOfParticipant(participant);
                                return participant;
                            }
                            else if (userType == User.UserType.Coach)
                            {
                                Coach.AreaOfExpertise expertise = (Coach.AreaOfExpertise)Enum.Parse(typeof(Coach.AreaOfExpertise), reader.GetString(reader.GetOrdinal("Expertise")));

                                Coach coach = new Coach(username, password, email, firstName, lastName, dateOfBirth, expertise);
                                coach.ID = userId;
                                return coach;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public User? GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            DateTime dateOfBirth = reader.GetDateTime(6);
                            User.UserType userType = (User.UserType)Enum.Parse(typeof(User.UserType), reader.GetString(7));

                            if (userType == User.UserType.Participant)
                            {
                                int generalLevel = reader.GetInt32(reader.GetOrdinal("GeneralLevel"));
                                int generalExperience = reader.GetInt32(reader.GetOrdinal("GeneralExperience"));

                                Participant participant = new Participant(username, password, email, firstName, lastName, dateOfBirth);
                                participant.ID = userId;
                                participant.GeneralLevel = generalLevel;
                                participant.GeneralExperience = generalExperience;
                                GetEventProgressesOfParticipant(participant);
                                return participant;
                            }
                            else if (userType == User.UserType.Coach)
                            {
                                Coach.AreaOfExpertise expertise = (Coach.AreaOfExpertise)Enum.Parse(typeof(Coach.AreaOfExpertise), reader.GetString(reader.GetOrdinal("Expertise")));

                                Coach coach = new Coach(username, password, email, firstName, lastName, dateOfBirth, expertise);
                                coach.ID = userId;
                                return coach;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public User? GetUserByUserCredentials(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string query = "SELECT * FROM Users WHERE Username = 'sgreen_nutrition' AND Password = 'pass456Secure'";
                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string email = reader.GetString(3);
                            string firstName = reader.GetString(4);
                            string lastName = reader.GetString(5);
                            DateTime dateOfBirth = reader.GetDateTime(6);
                            Console.WriteLine(reader.GetString(7));
                            User.UserType userType = (User.UserType)Enum.Parse(typeof(User.UserType), reader.GetString(7));

                            if (userType == User.UserType.Participant)
                            {
                                int generalLevel = reader.GetInt32(reader.GetOrdinal("GeneralLevel"));
                                int generalExperience = reader.GetInt32(reader.GetOrdinal("GeneralExperience"));

                                Participant participant = new Participant(username, password, email, firstName, lastName, dateOfBirth);
                                participant.ID = userId;
                                participant.GeneralLevel = generalLevel;
                                participant.GeneralExperience = generalExperience;
                                GetEventProgressesOfParticipant(participant);
                                return participant;
                            }
                            else if (userType == User.UserType.Coach)
                            {
                                Coach.AreaOfExpertise expertise = (Coach.AreaOfExpertise)Enum.Parse(typeof(Coach.AreaOfExpertise), reader.GetString(reader.GetOrdinal("Expertise")));

                                Coach coach = new Coach(username, password, email, firstName, lastName, dateOfBirth, expertise);
                                coach.ID = userId;
                                return coach;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void GetEventProgressesOfParticipant(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM EventProgresses WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", participant.ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int eventId = reader.GetInt32(reader.GetOrdinal("EventId"));
                            int level = reader.GetInt32(reader.GetOrdinal("Level"));
                            float experience = (float)reader.GetDouble(reader.GetOrdinal("Experience"));

                            EventTypeProgress eventProgress = new EventTypeProgress(eventId, participant.ID);
                            eventProgress.Level = level;
                            eventProgress.Experience = experience;

                            participant.EventProgresses.Add(eventProgress);
                        }
                    }
                }
            }
        }

        public void InsertRequest(FriendRequest friendRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO FriendRequests (SenderUserID, RecieverUserID, RequestDate, Status) VALUES (@SenderUserID, @RecieverUserID, @RequestDate, @Status)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderUserID", friendRequest.SenderUserId);
                    command.Parameters.AddWithValue("@RecieverUserID", friendRequest.ReceiverUserId);
                    command.Parameters.AddWithValue("@RequestDate", friendRequest.RequestDate);
                    command.Parameters.AddWithValue("@Status", friendRequest.Status.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRequestStatus(FriendRequest friendRequest)
        {  
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE FriendRequests SET Status = @Status WHERE RequestId = @RequestId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", friendRequest.RequestId);
                    command.Parameters.AddWithValue("@Status", friendRequest.Status.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

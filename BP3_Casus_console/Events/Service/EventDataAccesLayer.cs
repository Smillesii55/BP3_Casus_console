using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BP3_Casus_console.Events.Service
{
    public class EventDataAccesLayer
    {
        // To clarify how the EventDataAccesLayer should communicate with the database, here is how the database tables are built and how the data is inserted into them:
        // use the following SQL queries to create the necessary tables and insert data into them:

        /* -- THE DESIGN OF THE EVENTS TABLE AND THE CORROSPONDING EVENTTAGS AND EVENTPROGRESSES TABLE! --
        THE EVENTTAGS TABLE CONTAINS THE TAGS WHICH IDENTIFY THE EVENT
        THE EVENTPROGRESS TABLE CONTAINS THE PROGRESS A PARTICIPANT HAS MADE IN A SPECIFIC EVENT

        CREATE TABLE Events (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Name NVARCHAR(255) NOT NULL,
            ExperiencePerParticipation FLOAT NOT NULL
        );
        CREATE TABLE EventTags (
            EventId INT,
            Tag NVARCHAR(255),
            FOREIGN KEY (EventId) REFERENCES Events(Id)
        );
        */

        /* -- CREATING AN EVENT AND ADDING TAGS TO IT! --
            DECLARE @EventId INT;
            INSERT INTO Events (Name, ExperiencePerParticipation)
            VALUES ('Kung Fu', 50);

            SET @EventId = SCOPE_IDENTITY();
            -- Now, insert tags using @EventId
            INSERT INTO EventTags (EventId, Tag)
            VALUES (@EventId, 'Martial Arts');
            INSERT INTO EventTags (EventId, Tag)
            VALUES (@EventId, 'Fitness');
            INSERT INTO EventTags (EventId, Tag)
            VALUES (@EventId, 'Self Defense');
            */

        /*
        CREATE TABLE EventProgresses (
            EventProgressId INT IDENTITY(1,1) PRIMARY KEY,
            ID INT,
            EventId INT,
            Level INT NOT NULL DEFAULT 1,
            Experience FLOAT NOT NULL DEFAULT 0,
            FOREIGN KEY (ID) REFERENCES Users(UserID),
            FOREIGN KEY (EventId) REFERENCES Events(Id)
        );

        INSERT INTO EventProgresses (ID, EventId, Level, Experience)
        VALUES (3, 2, 1, 10.0);
        */

        private string connectionString = "Server=.;Database=CashRegisterSystem;Trusted_Connection=True;";

        private EventDataAccesLayer()
        {
        }

        private static EventDataAccesLayer? instance = null;
        public static EventDataAccesLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventDataAccesLayer();
                }
                return instance;
            }
        }

        public void InsertEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Events (Name, ExperiencePerParticipation) VALUES (@Name, @ExperiencePerParticipation)", connection))
                {
                    command.Parameters.AddWithValue("@Name", @event.Name);
                    command.Parameters.AddWithValue("@ExperiencePerParticipation", @event.ExpPerParticipant);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertEventTags(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id FROM Events WHERE Name = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", @event.Name);
                    int eventId = (int)command.ExecuteScalar();
                    foreach (string tag in @event.Tags)
                    {
                        using (SqlCommand insertCommand = new SqlCommand("INSERT INTO EventTags (EventId, Tag) VALUES (@EventId, @Tag)", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@EventId", eventId);
                            insertCommand.Parameters.AddWithValue("@Tag", tag);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public void InsertEventProgress(EventProgress progress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO EventProgresses (ID, EventId, Level, Experience) VALUES (@ID, @EventId, @Level, @Experience)", connection))
                {
                    command.Parameters.AddWithValue("@EventId", progress.EventID);
                    command.Parameters.AddWithValue("@Level", progress.Level);
                    command.Parameters.AddWithValue("@Experience", progress.Experience);
                    command.ExecuteNonQuery();
                }
            }
        }



        public void UpdateEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Events SET Name = @Name, ExperiencePerParticipation = @ExperiencePerParticipation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Name", @event.Name);
                    command.Parameters.AddWithValue("@ExperiencePerParticipation", @event.ExpPerParticipant);
                    command.Parameters.AddWithValue("@Id", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateEventTags(Event @event)
        {
            DeleteEventTags(@event.ID);
            InsertEventTags(@event);
        }

        public void UpdateEventProgress(EventProgress progress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE EventProgresses SET Level = @Level, Experience = @Experience WHERE EventProgressId = @EventProgressId", connection))
                {
                    command.Parameters.AddWithValue("@Level", progress.Level);
                    command.Parameters.AddWithValue("@Experience", progress.Experience);
                    command.ExecuteNonQuery();
                }
            }
        }



        public void DeleteEvent(Event @event)
        {             
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Events WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEventTags(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventTags WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEventProgress(int eventProgressId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventProgresses WHERE EventProgressId = @EventProgressId", connection))
                {
                    command.Parameters.AddWithValue("@EventProgressId", eventProgressId);
                    command.ExecuteNonQuery();
                }
            }
        }



        public Event? GetEventById(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // create and return the event
                        }
                    }
                }
            }
            return null;
        }


        public List<Event>? GetEventsByTag(string tag)
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Id IN (SELECT EventId FROM EventTags WHERE Tag = @Tag)", connection))
                {
                    command.Parameters.AddWithValue("@Tag", tag);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // add to the list of events
                        }
                    }
                }
            }
            return events;
        }

        public Event GetEventByName(string eventName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Name = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", eventName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // create and return the event
                        }
                    }
                }
            }
            return null;
        }

        public List<Event>? GetEvents()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // add to the list of events
                        }
                    }
                }
            }
            return events;
        }

        public EventProgress? GetEventProgress(int userId, int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventProgresses WHERE ID = @ID AND EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@ID", userId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // create and return the event progress
                        }
                    }
                }
            }
            return null;
        }
    }
}

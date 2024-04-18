using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BP3_Casus_console.Users;
using BP3_Casus_console.Users.Service;
using BP3_Casus_console.Events;

namespace BP3_Casus_console.Events.Service
{
    public class EventDataAccesLayer
    {
        private string connectionString = "Server=.;Database=BP3Casus;Trusted_Connection=True;";

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



        public void InsertEventType(EventType eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO EventTypes (Name, Description, ExpPerParticipant) VALUES (@Name, @Description, @ExpPerParticipant)", connection))
                {
                    command.Parameters.AddWithValue("@Name", eventType.Name);
                    command.Parameters.AddWithValue("@Description", eventType.Description);
                    command.Parameters.AddWithValue("@ExpPerParticipant", eventType.ExpPerParticipant);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Events (Date, MaxParticipants, Participants, EventTypeId ) VALUES (@Date, @MaxParticipants, @Participants, @EventTypeId)", connection))
                {
                    command.Parameters.AddWithValue("@Date", @event.Date);
                    command.Parameters.AddWithValue("@MaxParticipants", @event.MaxParticipants);
                    command.Parameters.AddWithValue("@Participants", @event.Participants.Count);
                    command.Parameters.AddWithValue("@EventTypeId", @event.EventType.ID);
                    command.ExecuteNonQuery();
                }
            }
            InsertEventParticipants(@event.Participants);
            InsertEventCoach(@event.Coach);
        }

        public void InsertEventTypeTags(EventType @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (string tag in @event.Tags)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO EventTags (EventId, Tag) VALUES (@EventId, @Tag)", connection))
                    {
                        command.Parameters.AddWithValue("@EventId", @event.ID);
                        command.Parameters.AddWithValue("@Tag", tag);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public void InsertEventTypeProgress(EventTypeProgress progress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO EventProgresses (UserId, EventTypeId, Level, Expereince) VALUES (@UserId, @EventId, @Level, @Experience)", connection))
                {
                    command.Parameters.AddWithValue("@UserId", progress.UserID);
                    command.Parameters.AddWithValue("@EventTypeId", progress.EventTypeID);
                    command.Parameters.AddWithValue("@Level", progress.Level);
                    command.Parameters.AddWithValue("@Experience", progress.Experience);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertEventParticipants(List<Participant> participants)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (Participant participant in participants)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Participants (EventId, UserId) VALUES (@EventId,@UserId)", connection))
                    {
                        command.Parameters.AddWithValue("@EventId", participant.ID);
                        command.Parameters.AddWithValue("@UserId", participant.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public void InsertEventCoach(Coach coach)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Coaches (EventId, UserId) VALUES (@EventId,@UserId)", connection))
                {
                    command.Parameters.AddWithValue("@EventId", coach.ID);
                    command.Parameters.AddWithValue("@UserId", coach.ID);
                    command.ExecuteNonQuery();
                }
            }
        }



        public void UpdateEventType(EventType eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE EventTypes SET Name = @Name, Description = @Description, ExpPerParticipant = @ExpPerParticipant WHERE EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@Name", eventType.Name);
                    command.Parameters.AddWithValue("@Description", eventType.Description);
                    command.Parameters.AddWithValue("@ExpPerParticipant", eventType.ExpPerParticipant);
                    command.Parameters.AddWithValue("@EventTypeId", eventType.ID);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Events SET Date = @Date, MaxParticipants = @MaxParticipants, Participants = @Participants, EventTypeId = @EventTypeId WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@Date", @event.Date);
                    command.Parameters.AddWithValue("@MaxParticipants", @event.MaxParticipants);
                    command.Parameters.AddWithValue("@Participants", @event.Participants.Count);
                    command.Parameters.AddWithValue("@EventTypeId", @event.EventType.ID);
                    command.Parameters.AddWithValue("@EventId", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEventTypeTags(EventType eventType)
        {
            DeleteEventTypeTags(eventType);
            InsertEventTypeTags(eventType);
        }
        public void UpdateEventProgress(EventTypeProgress progress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE EventProgresses SET Level = @Level, Experience = @Experience WHERE UserId = @UserId AND EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", progress.UserID);
                    command.Parameters.AddWithValue("@EventTypeId", progress.EventTypeID);
                    command.Parameters.AddWithValue("@Level", progress.Level);
                    command.Parameters.AddWithValue("@Experience", progress.Experience);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEventParticipants(Event @event)
        {
            DeleteEventParticipants(@event);
            InsertEventParticipants(@event.Participants);
        }
        public void UpdateEventCoach(Event @event)
        {
            DeleteEventCoach(@event);
            InsertEventCoach(@event.Coach);
        }



        public void DeleteEventType(EventType eventType)
        {
            DeleteAllEventTypeProgressesFromEventType(eventType);
            DeleteEventTypeTags(eventType);
            DeleteAllEventsFromEventType(eventType);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventTypes WHERE EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@EventTypeId", eventType.ID);
                    command.ExecuteNonQuery();
                }
            }

        }
        public void DeleteEvent(Event @event)
        {    
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Events WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllEventsFromEventType(EventType eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Events WHERE EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@EventTypeId", eventType.ID);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAllEventTypeProgressesFromEventType(EventType eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventProgresses WHERE EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@EventTypeId", eventType.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEventTypeTags(EventType eventType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventTags WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventType.ID);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteEventTypeProgress(EventTypeProgress eventTypeProgress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventProgresses WHERE EventProgressId = @EventProgressId", connection))
                {
                    command.Parameters.AddWithValue("@EventProgressId", eventTypeProgress.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEventParticipants(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventParticipants WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteEventCoach(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM EventCoaches WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", @event.ID);
                    command.ExecuteNonQuery();
                }
            }
        }



        public Event? GetEventById(int eventId)
        {
            Coach coach = GetEventCoachByEventID(eventId);
            List<Participant> participants = GetEventParticipantsByEventID(eventId);
            EventType eventType = GetEvent_EventTypeByEventID(eventId);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Event @event = new Event(coach, (DateTime)reader["Date"], (int)reader["MaxParticipants"]);
                            @event.ID = eventId;
                            @event.Participants = participants;
                            @event.EventType = GetEventTypeByEventId((int)reader["EventTypeId"]);
                            return @event;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }


        }
        public Event? GetEventByName(string eventName)
        {
            Coach coach = GetEventCoachByEventName(eventName);
            List<Participant> participants = GetEventParticipantsByEventName(eventName);
            EventType eventType = GetEvent_EventTypeByEventName(eventName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Date = @Date AND MaxParticipants = @MaxParticipants AND Participants = @Participants AND EventTypeId = @EventTypeId", connection))
                {
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@MaxParticipants", 0);
                    command.Parameters.AddWithValue("@Participants", 0);
                    command.Parameters.AddWithValue("@EventTypeId", eventType.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Event @event = new Event(coach, (DateTime)reader["Date"], (int)reader["MaxParticipants"]);
                            @event.ID = (int)reader["EventId"];
                            @event.Participants = participants;
                            @event.EventType = GetEventTypeByEventId((int)reader["EventTypeId"]);
                            return @event;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public EventType GetEventTypeById(int eventTypeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventTypes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", eventTypeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EventType eventType = new EventType((string)reader["Name"], (string)reader["Description"], (double)reader["ExpPerParticipant"]);
                            eventType.ID = eventTypeId;
                            return eventType;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public EventType GetEventTypeByName(string eventTypeName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventTypes WHERE Name = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", eventTypeName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EventType eventType = new EventType((string)reader["Name"], (string)reader["Description"], (double)reader["ExpPerParticipant"]);
                            eventType.ID = (int)reader["Id"];
                            return eventType;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Coach GetEventCoachByEventID(int eventId)
        {
            UserDataAccesLayer userDataAccesLayer = UserDataAccesLayer.Instance;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventCoach WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (Coach)userDataAccesLayer.GetUserById((int)reader["UserId"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public Coach GetEventCoachByEventName(string coachName)
        {
            UserDataAccesLayer userDataAccesLayer = UserDataAccesLayer.Instance;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventCoach WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userDataAccesLayer.GetUserByUsername(coachName).ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (Coach)userDataAccesLayer.GetUserById((int)reader["UserId"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Participant> GetEventParticipantsByEventID(int eventId)
        {
            UserDataAccesLayer userDataAccesLayer = UserDataAccesLayer.Instance;
            List<Participant> participants = new List<Participant>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventParticipants WHERE EventId = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            participants.Add((Participant)userDataAccesLayer.GetUserById((int)reader["UserId"]));
                        }
                    }
                }
            }
            return participants;
        }
        public List<Participant> GetEventParticipantsByEventName(string eventName)
        {
            UserDataAccesLayer userDataAccesLayer = UserDataAccesLayer.Instance;
            List<Participant> participants = new List<Participant>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventParticipants WHERE Id = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", GetEventByName(eventName).ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            participants.Add((Participant)userDataAccesLayer.GetUserById((int)reader["UserId"]));
                        }
                    }
                }
            }
            return participants;
        }

        public EventType GetEventTypeByEventId(int evenId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Id = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", evenId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return GetEventTypeById((int)reader["Id"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public EventType GetEvent_EventTypeByEventID(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Id = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", eventId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return GetEventTypeByEventId((int)reader["EventTypeId"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public EventType GetEvent_EventTypeByEventName(string eventName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE Id = @EventId", connection))
                {
                    command.Parameters.AddWithValue("@EventId", GetEventByName(eventName).ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return GetEventTypeByEventId((int)reader["EventTypeId"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<EventType>? GetEventTypesByTag(string tag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventTags WHERE Tag = @Tag", connection))
                {
                    command.Parameters.AddWithValue("@Tag", tag);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<EventType> eventTypes = new List<EventType>();
                        while (reader.Read())
                        {
                            eventTypes.Add(GetEventTypeByEventId((int)reader["EventId"]));
                        }
                        return eventTypes;
                    }
                }
            }
        }

        public List<EventType>? GetAllEventTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM EventTypes", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<EventType> eventTypes = new List<EventType>();
                        while (reader.Read())
                        {
                            EventType eventType = new EventType((string)reader["Name"], (string)reader["Description"], (double)reader["ExpPerParticipant"]);
                            eventType.ID = (int)reader["Id"];
                            eventTypes.Add(eventType);
                        }
                        return eventTypes;
                    }
                }
            }
        }
        public List<Event>? GetAllEvents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Events", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Event> events = new List<Event>();
                        while (reader.Read())
                        {
                            Coach coach = GetEventCoachByEventID((int)reader["Id"]);
                            List<Participant> participants = GetEventParticipantsByEventID((int)reader["Id"]);
                            EventType eventType = GetEvent_EventTypeByEventID((int)reader["Id"]);
                            Event @event = new Event(coach, (DateTime)reader["Date"], (int)reader["MaxParticipants"]);
                            @event.ID = (int)reader["Id"];
                            @event.Participants = participants;
                            @event.EventType = GetEventTypeByEventId((int)reader["Id"]);
                            events.Add(@event);
                        }
                        return events;
                    }
                }
            }
        }   
    }
}
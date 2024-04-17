using BP3_Casus_console.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }

        public QuizQuestion(string question, List<string> options)
        {
            Question = question;
            Options = options;
        }
    }
}
/*public List<Event> GetPreferedEventsBasedOnTags(List<string> tags)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string commandText = "SELECT * FROM Events WHERE Id IN (SELECT EventId FROM EventTags WHERE Tag IN ({0}))";
        // Creating parameter placeholders for each tag
        string[] paramNames = tags.Select((s, i) => "@Tag" + i.ToString()).ToArray();
        string inClause = string.Join(",", paramNames);
        string formattedCommandText = string.Format(commandText, inClause);

        using (SqlCommand command = new SqlCommand(formattedCommandText, connection))
        {
            // Adding parameters
            for (int i = 0; i < tags.Count; i++)
            {
                command.Parameters.AddWithValue(paramNames[i], tags[i]);
            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                List<Event> events = new List<Event>();
                while (reader.Read())
                {
                    // read event data and add to the list of events
                    string name = reader["Name"].ToString();
                    double expPerParticipant = (double)reader["ExperiencePerParticipation"];
                    Event @event = new Event(name, expPerParticipant);
                    events.Add(@event);
                }
                return events;
            }
        }
    }
}*/

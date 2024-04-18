using BP3_Casus_console.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz
{
    public class QuizResult
    {
        public List<QuizChoice> SelectedChoices { get; set; } = new List<QuizChoice>();

        // Logic to analyze results and recommend activities
        /*
        public List<Event> RecommendEvents(List<Event> allEvents)
        {
            var tags = SelectedChoices.SelectMany(choice => choice.EventTags).ToList();
            var recommendedEvents = allEvents
                .Where(Event => Event.Tags.Intersect(tags).Any()) // Assuming Activity class has a Tags property
                .ToList();

            return recommendedEvents;
        }
        */
    }
}

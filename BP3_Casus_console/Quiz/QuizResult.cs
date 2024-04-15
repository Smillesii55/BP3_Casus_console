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
        public List<Event> RecommendEvents(List<Event> allEvents)
        {
            var tags = SelectedChoices.SelectMany(choice => choice.EventTags).ToList();
            var recommendedEvents = allEvents
                .Where(Event => Event.Tags.Intersect(tags).Any()) // Assuming Activity class has a Tags property
                .ToList();

            return recommendedEvents;
        }
    }
    class TEST
    {
        static void Main(string[] args)
        {
            // Create quiz questions
            var question1 = new QuizQuestion("");
            question1.AddChoice("");
            question1.AddChoice("");
            question1.AddChoice("");

            var question2 = new QuizQuestion("");
            question2.AddChoice("");
            question2.AddChoice("");

            // Create a QuizResult to store the user's choices
            var quizResult = new QuizResult();

            // Assume GetUserChoice is a method that presents a question to the user and returns their choice
            quizResult.SelectedChoices.Add(GetUserChoice(question1));
            quizResult.SelectedChoices.Add(GetUserChoice(question2));

            // Now you can use the QuizResult to recommend events based on the user's choices
            var allEvents = GetAllEvents(); // Assume this is a method that returns a list of all possible events
            var recommendedEvents = quizResult.RecommendEvents(allEvents);

            // Now you can present the recommended events to the user
        }
    }
}

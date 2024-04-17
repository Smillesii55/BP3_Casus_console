using BP3_Casus_console.Events;
using BP3_Casus_console.Events.Service;
using BP3_Casus_console.Users.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz.Service
{
    public class QuizService
    {
        EventDataAccesLayer eventDal = EventDataAccesLayer.Instance;

        private QuizService()
        {

        }

        private static QuizService? instance = null;
        public static QuizService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuizService();
                }
                return instance;
            }
        }

        public List<QuizQuestion> GetQuestions()
        {
            List<QuizQuestion> questions = new List<QuizQuestion>();

            questions.Add(new QuizQuestion("Wat zijn je sportdoelen?", new List<string> { "Ik wil meer conditie opbouwen.", "Ik wil meer flexibiliteit.", "Ik wil meer kracht opbouwen.", "Ik wil mezelf kunnen verdedigen.", "Ik wil meer bewegen in het algemeen." }));
            questions.Add(new QuizQuestion("Wat voor soort beweging vind je leuk?", new List<string> { "Vechtsport", "Krachttraining", "Cardio", "Mindful bewegen", "Teamsport" }));

            return questions;
        }

        public List<Event> ProcessAnswers(List<String> answers)
        {
            List<string> tags = new List<string>();

            foreach (string answer in answers)
            {
                if (answer == "Ik wil meer conditie opbouwen." || answer == "Ik wil meer flexibiliteit." || answer == "Ik wil meer kracht opbouwen.")
                {
                    tags.Add("Fitness");
                }
                else if (answer == "Ik wil mezelf kunnen verdedigen.")
                {
                    tags.Add("Vechtsport");
                }
                else if (answer == "Ik wil meer bewegen in het algemeen.")
                {
                    tags.Add("Algemeen");
                }
                else if (answer == "Vechtsport")
                {
                    tags.Add("Vechtsport");
                }
                else if (answer == "Krachttraining")
                {
                    tags.Add("Fitness");
                }
                else if (answer == "Cardio")
                {
                    tags.Add("Fitness");
                }
                else if (answer == "Mindful bewegen")
                {
                    tags.Add("Mindfulness");
                }
                else if (answer == "Teamsport")
                {
                    tags.Add("Teamsport");
                }
            }

            return eventDal.GetPreferedEventsBasedOnTags(tags);
        }
    }
}

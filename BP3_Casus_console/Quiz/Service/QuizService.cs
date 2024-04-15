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
    }
}

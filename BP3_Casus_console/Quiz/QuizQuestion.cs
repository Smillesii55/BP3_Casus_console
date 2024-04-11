using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<QuizChoice> Choices { get; set; } = new List<QuizChoice>();

        public QuizQuestion(string questionText)
        {
            QuestionText = questionText;
        }
    }
}

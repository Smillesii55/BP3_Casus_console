using System;
using System.Collections.Generic;
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

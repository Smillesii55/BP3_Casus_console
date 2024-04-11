using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz
{
    public class QuizChoice
    {
        public string ChoiceText { get; set; }
        public List<string> EventTags { get; set; } = new List<string>(); // Tags to relate choices with activities4

        public QuizChoice(string choiceText)
        {
            ChoiceText = choiceText;
        }
    }
}

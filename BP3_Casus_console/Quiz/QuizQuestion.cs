using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Quiz
{

    public class QuizQuestion
    {
        public string QuestionText { get; }
        public readonly List<QuizChoice> Choices = new List<QuizChoice>();

        public QuizQuestion(string questionText)
        {
            if (string.IsNullOrWhiteSpace(questionText))
                throw new ArgumentException("Question text cannot be null or empty.", nameof(questionText));

            QuestionText = questionText;
        }

        public void AddChoice(string choiceText, params string[] eventTags)
        {
            Choices.Add(new QuizChoice(choiceText, eventTags.ToList()));
        }
    }

    public class QuizChoice
    {
        public string ChoiceText { get; }
        public readonly List<string> EventTags;

        public QuizChoice(string choiceText, List<string> eventTags)
        {
            if (string.IsNullOrWhiteSpace(choiceText))
                throw new ArgumentException("Choice text cannot be null or empty.", nameof(choiceText));

            ChoiceText = choiceText;
            EventTags = eventTags ?? new List<string>();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Create quiz questions
            QuizQuestion question1 = new QuizQuestion("");
            question1.AddChoice("", new List<string> { "" });
            question1.AddChoice("", new List<string> { "" });
            question1.AddChoice("", new List<string> { "" });

            QuizQuestion question2 = new QuizQuestion("");
            question2.AddChoice("", new List<string> { "" });
            question2.AddChoice("", new List<string> { "" });

            // Add more questions as needed

            // Now you have created your quiz questions with choices
            // You can proceed to present these questions to the user and collect their responses
        }
    }
}

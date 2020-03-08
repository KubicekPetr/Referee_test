using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_rozhodčího
{
    public class Question
    {
        public string Task { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public string True { get; set; }

        /// <param name="task">Otázka</param>
        /// <param name="answers">Odpovědi</param>
        public Question(string task, List<string> answers)
        {
            Task = task;
            Answers = answers;
            True = Answers[0];

            Rnd.Shuffle(answers);
        }

        public void Shuffle()
        {
            Rnd.Shuffle(Answers);
        }
    }
}

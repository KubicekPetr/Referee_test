using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;

namespace Test_rozhodčího
{
    public class Questions : INotifyPropertyChanged
    {
        XmlDocument doc = new XmlDocument();

        private List<Question> Data { get; set; } = new List<Question>();
      
        private List<Question> selection;
        public List<Question> Selection
        {
            get { return selection; }
            set
            {
                if (value != selection)
                {
                    selection = value;
                    OnPropertyChanged("Selection");
                }
            }
        }

        public Questions(string file)
        {
            Selection = new List<Question>();
            doc.Load(file);
            LoadData();        
        }

        private void LoadData()
        {
            string task = "";
            List<string> l = new List<string>();

            foreach (XmlNode questions in doc.DocumentElement)
            {
                task = "";
                foreach (XmlNode answers in questions.ChildNodes)
                {
                    if (task == "")
                    {
                        task = answers.InnerText;
                    }

                    string[] tokens = answers.InnerText.Split(';');
                    l = tokens.Cast<string>().ToList();
                }

                Data.Add(new Question(task, l));
            }
        }

        /// <summary>
        /// Zamíchá otázky a pak vybere prvních i až plug
        /// </summary>
        /// <param name="plug">Kolik chceme vytáhnout otázek</param>
        public void Select(int plug)
        {
            List<Question> l = new List<Question>();

            Rnd.Shuffle(Data); // zamíchá otázky
            Selection.Clear();
            for (int i = 0; i < plug; i++)
            {
                Data[i].Shuffle(); // zamíchá odpovědi
                l.Add(Data[i]);
            }
            Selection = l;
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

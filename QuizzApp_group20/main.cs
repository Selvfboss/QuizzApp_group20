using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Using System.Net.Http directive which will enable HttpClient.
using System.Net.Http;
//use newtonsoft to convert json to c# objects.
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace QuizzApp_group20
{
    public partial class main : Form
    {

        private const string URL = "https://opentdb.com/api.php?amount=1&category=18&difficulty=medium&type=multiple";
        private static Random random = new Random();
        private string correctOption;
        public main()
        {
            InitializeComponent();
            fetchApiData(URL);
        }

        public async void fetchApiData(string URL)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(URL))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if(data != null)
                            {

                                //Display questions
                                var apiData = JObject.Parse(data)["results"][0];
                                qtnLabel.Text = apiData["question"].ToString();

                                //Display answer options
                                string correctAnswer = apiData["correct_answer"].ToString();
                                string[] incorrectAnswers = apiData["incorrect_answers"].ToObject<string[]>();
                                string[] answers = MixAnswers(correctAnswer, incorrectAnswers);

                                checkBox1.Text = answers[0];
                                checkBox2.Text = answers[1];
                                checkBox3.Text = answers[2];
                                checkBox4.Text = answers[3];

                                //correct answer
                                correctOption = correctAnswer;
                            } else
                            {
                                Console.WriteLine("No data beep boop :(");
                            }
                        }
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static string[] MixAnswers(string correctAnswer, string[] incorrectAnswers)
        {
            List<string> mixedAnswers = new List<string>();
            mixedAnswers.Add(correctAnswer);
            mixedAnswers.AddRange(incorrectAnswers);

            // Use Fisher-Yates shuffle to randomly mix the answers
            for (int i = mixedAnswers.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                string temp = mixedAnswers[i];
                mixedAnswers[i] = mixedAnswers[j];
                mixedAnswers[j] = temp;
            }

            return mixedAnswers.ToArray();
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            fetchApiData(URL);
        }
    }
}

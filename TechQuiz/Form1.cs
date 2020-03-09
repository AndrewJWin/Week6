using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechQuiz
{
    public partial class Form1 : Form
    {

        private readonly SortedList<string, bool> Questions = new SortedList<string, bool>(){
            {"SSDs are faster than HDDs", true},
            {"Microsoft owns Github", true},
            {"100 is the ASCII code for A", false},
            {"NTFS was introduced in Windows Vista", false},
            {"Windows owns HP", false }
            };

        private int QuestionNumber = -1;
        private int Score = 0;


        public Form1()
        {
            InitializeComponent();
            nextQuestion();
        }

        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            if(rdoTrue.Checked || rdoFalse.Checked)
            {
                checkAnswer();
                nextQuestion();
            }
        }

        private void nextQuestion()
        {
            QuestionNumber++;

            if (QuestionNumber < Questions.Count)
            {
                KeyValuePair<String, bool> question = Questions.ElementAt(QuestionNumber);
                string questionText = question.Key;
                txtQuestion.Text = questionText;

                rdoFalse.Checked = false;
                rdoTrue.Checked = false;
            }
            else
            {
                btnNextQuestion.Enabled = false;
                btnNextQuestion.Text = "Quiz Over!";
                MessageBox.Show($"Your score is {Score}", "Quiz Over!");
            }
        }
        
        private void checkAnswer()
        {

            if (QuestionNumber < Questions.Count)
            {
                KeyValuePair<String, bool> question = Questions.ElementAt(QuestionNumber);
                bool correctAnswer = question.Value;

                if (correctAnswer == true && rdoTrue.Checked == true || correctAnswer == false && rdoFalse.Checked == true)
                {
                    Score++;
                }
                lblScore.Text = $"Score: {Score}";
            }
        }
    }
}

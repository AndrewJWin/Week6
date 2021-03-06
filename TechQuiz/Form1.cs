﻿using System;
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
        // Readonly Sorted list filled with questions.
        private readonly SortedList<string, bool> Questions = new SortedList<string, bool>(){
            {"SSDs are faster than HDDs", true},
            {"Microsoft owns Github", true},
            {"100 is the ASCII code for A", false},
            {"NTFS was introduced in Windows Vista", false},
            {"Windows owns HP", false }
            };
        
        // Global variables for question number and current score.
        private int QuestionNumber = -1;
        private int Score = 0;


        public Form1()
        {
            InitializeComponent();
            // Forcing the program to load the first question.
            nextQuestion();
        }

        // Next Question button method, checks if either of the radio buttons are clicked - then checks the answer and goes to the next question.
        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            if(rdoTrue.Checked || rdoFalse.Checked)
            {
                checkAnswer();
                nextQuestion();
            }
        }

        // nextQuestion method, increments the current Question number and posts the new question.
        private void nextQuestion()
        {
            QuestionNumber++;

            // Checking if the question number is past the last question in the list, if not - We post the next question.
            if (QuestionNumber < Questions.Count)
            {
                KeyValuePair<String, bool> question = Questions.ElementAt(QuestionNumber);
                string questionText = question.Key;
                txtQuestion.Text = questionText;

                // This sets both the checkboxes as unchecked.
                rdoFalse.Checked = false;
                rdoTrue.Checked = false;
            }
            else
            {
                // If we're past the last question in the quiz, the quiz is over - We need to post to the user their score.
                btnNextQuestion.Enabled = false;
                btnNextQuestion.Text = "Quiz Over!";
                MessageBox.Show($"Your score is {Score}", "Quiz Over!");
            }
        }
        
        // checkAnswer method, reviews the current state of the radio button and checks if it is correct.
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

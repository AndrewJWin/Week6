using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuarterBill
{
    public partial class Form1 : Form
    {
        List<Decimal> Quarters = new List<Decimal>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Preparing variables;
            decimal total = 0m;
            decimal average = 0m;

            // Validating all the inputs provided by the user before going ahead and adding to list.
            if (!ValidatePositiveDecimal(txbQtrOne.Text, out decimal QtrOne, out string QtrOneError))
            {
                txbQtrOne.Focus();
                MessageBox.Show($"Please verify the text in Quarter One. {QtrOneError}", "Quarter One Error");
            }
            Quarters.Add(QtrOne);
            if (!ValidatePositiveDecimal(txbQtrTwo.Text, out decimal QtrTwo, out string QtrTwoError))
            {
                txbQtrOne.Focus();
                MessageBox.Show($"Please verify the text in Quarter Two. {QtrTwoError}", "Quarter Two Error");
            }
            Quarters.Add(QtrTwo);
            if (!ValidatePositiveDecimal(txbQtrThree.Text, out decimal QtrThree, out string QtrThreeError))
            {
                txbQtrOne.Focus();
                MessageBox.Show($"Please verify the text in Quarter Three. {QtrThreeError}", "Quarter Three Error");
            }
            Quarters.Add(QtrThree);
            if (!ValidatePositiveDecimal(txbQtrFour.Text, out decimal QtrFour, out string QtrFourError))
            {
                txbQtrOne.Focus();
                MessageBox.Show($"Please verify the text in Quarter Four. {QtrFourError}", "Quarter Four Error");
            }
            Quarters.Add(QtrFour);

            // Once all the inputs have been validated and stored - we go through, add them together and figure out the average.
            foreach (decimal quarter in Quarters)
            {
                total += quarter;
                average += quarter;
            }
            average = average / 4;

            txbTotal.Text = total.ToString("C");
            txbAverage.Text = average.ToString("C");
        }

        private bool ValidatePositiveDecimal(string text, out decimal number, out string errorMsg)
        {
            errorMsg = null;
            number = 0m;
            try
            {
                number = Decimal.Parse(text);
                if (number >= 0m)
                {
                    return true;
                }
                else
                {
                    errorMsg = "Enter a positive Number";
                    return false;
                }
            }
            catch (FormatException)
            {
                errorMsg = "Enter numbers only.";
                return false;
            }
            catch (OverflowException)
            {
                errorMsg = "Enter a smaller number.";
                return false;
            }
        }
    }
}

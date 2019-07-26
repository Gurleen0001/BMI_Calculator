using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI_Calculator
{
    public partial class CalculatorForm : Form
    {
        // CLASS PROPPERTIES
        public string outputString { get; set; }
        public float outputValue { get; set; }
        public bool decimalExists { get; set; }

        /// <summary>
        /// This is the constructor for the CalculatorForm
        /// </summary>
        public CalculatorForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is a shared event handler for the CalculatorButton click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            var TheButton = sender as Button;
            var tag = TheButton.Tag.ToString();
            int numericValue = 0;

            bool numericResult = int.TryParse(tag, out numericValue);

            if(numericResult)
            {
                int maxSize = (decimalExists) ? 5 : 3;
                if(outputString=="0")
                {
                    outputString = tag;
                }
                else
                {
                    if(outputString.Length < maxSize)
                    outputString += tag;
                }
                ResultLabel.Text = outputString;
            }
            else
            {
                switch(tag)
                {
                    case "back":
                        removeLastCharacterFromResultLabel();
                        break;
                    case "done":
                        finalizeOutput();
                        break;
                    case "clear":
                        clearNumericKeyboard();
                        break;
                    case "decimal":
                        addDecimalToResultLabel();
                        break;

                }
            }

        }
        /// <summary>
        /// this method adds a decimal point to the resultLabel
        /// </summary>
        private void addDecimalToResultLabel()
        {
            if (!decimalExists)
            {
                outputString += ".";
                decimalExists = true;
            }
        }
        /// <summary>
        /// This method finalizes and converts the outputString to a floating point value
        /// </summary>
        private void finalizeOutput()
        {
            //if (decimalExists)
            //{
            //    if(outputString.Length > 3)
            //    {
            //        int charactersToRemove = outputString.IndexOf('.') + 2;
            //        outputString = outputString.Remove(charactersToRemove);
            //    }
            //}
            outputValue = float.Parse(outputString);
            outputValue = (float)(Math.Round(outputValue, 1));
            if (outputValue < 0.1f)
            {
                outputValue = 0.1f;
            }
            TheHeightLabel.Text = outputValue.ToString();
            clearNumericKeyboard();
            CalculatorButtonTableLayoutPanel.Visible = false;
        }
        /// <summary>
        /// This method removes the last character from the resultLabel
        /// </summary>
        private void removeLastCharacterFromResultLabel()
        {
            var lastChar = outputString.Substring(outputString.Length - 1);
            if (lastChar == ".")
            {
                decimalExists = false;
            }
            outputString = outputString.Remove(outputString.Length - 1);
            if (outputString.Length == 0)
            {
                outputString = "0";
            }
            ResultLabel.Text = outputString;
        }

        /// <summary>
        /// This method resets the numeric keyboard and related variables
        /// </summary>

        private void clearNumericKeyboard()
        {
            ResultLabel.Text = "0";
            outputString = "0";
            outputValue = 0.0f;
            decimalExists = false;
        }
        /// <summary>
        /// This is the event handler for the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            CalculatorButtonTableLayoutPanel.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// This is the event handler for the height label click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TheHeightLabel_Click(object sender, EventArgs e)
        {
            CalculatorButtonTableLayoutPanel.Visible = true;
        }
    }
}

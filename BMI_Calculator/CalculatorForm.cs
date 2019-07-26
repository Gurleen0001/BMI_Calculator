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
        public bool decimalExists { get; set; }
        public float outputValue { get; set; } 

        public Label ActiveLabel { get; set; }
        public double BMIresult { get; set; }

        public Animation animationfield;

       
        public CalculatorForm()
        {
            InitializeComponent();
        }
      
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            ActiveLabel = null;
            CalculatorButtonTableLayoutPanel.Visible = false;

            this.Size = new Size(320, 480);
            animationfield = Animation.IDLE;
        }
       
        private void CalculatorForm_Click(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            if(ActiveLabel!=null)
            {
                ActiveLabel.BackColor = Color.White;
            }
            ActiveLabel = null;
            animationfield = Animation.DOWN;
            AnimatedTimer.Enabled = true;
        }
        
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            Button TheButton = sender as Button;
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
       
        private void addDecimalToResultLabel()
        {
            if (!decimalExists)
            {
                outputString += ".";
                decimalExists = true;
            }
        }
       
        private void finalizeOutput()
        {
            if (outputString == string.Empty)
            {
                outputString = "0";
            }
            //if (decimalExists)
            //{
            //    if(outputString.Length > 3)
            //    {
            //        int charactersToRemove = outputString.IndexOf('.') + 2;
            //        outputString = outputString.Remove(charactersToRemove);
            //    }
            //}
            outputValue = float.Parse(outputString);
            //outputValue = (float)(Math.Round(outputValue, 1));
            //if (outputValue < 0.1f)
            //{
            //    outputValue = 0.1f;
            //}
            ActiveLabel.Text = outputValue.ToString();
            if (TheHeightLabel.Text=="0")
            {
                TheHeightLabel.Text = "0.1";
                clearNumericKeyboard();

                CalculatorButtonTableLayoutPanel.Visible = false;

                ActiveLabel.BackColor = Color.White;
                ActiveLabel = null;

                animationfield = Animation.DOWN;
                AnimatedTimer.Enabled = true;
            }
           else if (WeightLabel.Text=="0")
            {
                WeightLabel.Text = "0.1";
                clearNumericKeyboard();

                CalculatorButtonTableLayoutPanel.Visible = false;

                ActiveLabel.BackColor = Color.White;
                ActiveLabel = null;

                animationfield = Animation.DOWN;
                AnimatedTimer.Enabled = true;
            }

        }
        
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


        private void clearNumericKeyboard()
        {
            ResultLabel.Text = "0";
            outputString = "0";
            outputValue = 0.0f;
            decimalExists = false;
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void ActiveLabel_Click(object sender, EventArgs e)
        {
            if(ActiveLabel !=null)
            {
                ActiveLabel.BackColor = Color.White;
                ActiveLabel = null;
            }
            ActiveLabel = sender as Label;
            ActiveLabel.BackColor = Color.LightBlue;
            CalculatorButtonTableLayoutPanel.Visible = true;

            if(ActiveLabel.Text!="0")
            {
                ResultLabel.Text = ActiveLabel.Text;
                outputString = ResultLabel.Text;
            }
            CalculatorButtonTableLayoutPanel.BringToFront();
            AnimatedTimer.Enabled = true;
            animationfield = Animation.UP;
        }
        
        private void MetricButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AnimatedTimer_Tick(object sender, EventArgs e)
        {
            switch (animationfield)
            {
                case Animation.IDLE:
                    break;
                case Animation.UP:
                    UpKeyboard();
                    break;
                case Animation.DOWN:
                    DownKeyBoard();
                    break;
            }
        }

        private void UpKeyboard()
        {
            var currentLocation = CalculatorButtonTableLayoutPanel.Location;
            currentLocation = new Point(currentLocation.X, currentLocation.Y - 20);
            CalculatorButtonTableLayoutPanel.Location = currentLocation;
            if(currentLocation.Y <= ActiveLabel.Location.Y + 35)
            {
                CalculatorButtonTableLayoutPanel.Location = new Point(currentLocation.X, ActiveLabel.Location.Y + 35);

                AnimatedTimer.Enabled = false;
                animationfield = Animation.IDLE;
            }
        }
        private void DownKeyBoard()
        {
            var currentLocation = CalculatorButtonTableLayoutPanel.Location;
            currentLocation = new Point(currentLocation.X, currentLocation.Y + 20);
            CalculatorButtonTableLayoutPanel.Location = currentLocation;
            if (currentLocation.Y >=466)
            {
                CalculatorButtonTableLayoutPanel.Location = new Point(currentLocation.X, 466);
                AnimatedTimer.Enabled = false;
                animationfield = Animation.IDLE;
                CalculatorButtonTableLayoutPanel.Visible = false;
            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            double Height = Convert.ToDouble(TheHeightLabel.Text);
            double Weight = Convert.ToDouble(WeightLabel.Text);

            if (ImperialButton.Checked)
            {
                BMIresult = Weight * 703 / Math.Pow(Height, 2);
                ResultBox.Text = BMIresult.ToString();
            }
            else if (MetricButton.Checked)
            {
                BMIresult = Weight / Math.Pow(Height, 2);
                ResultBox.Text = BMIresult.ToString();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            ResultBox.Text = "";
            TheHeightLabel.Text = "";
            WeightLabel.Text = "";

        }
    }
    
}

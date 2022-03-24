using MetroSet_UI.Forms;

namespace Zad_0
{
    public partial class Calculator : MetroSetForm
    {
        private string firstNumber = "";
        private string operation = "";
        private string secondNumber = "";

        public Calculator()
        {
            InitializeComponent();
        }

        private void addNumber(int number)
        {
            if (firstNumber != "")
            {
                if (double.Parse(firstNumber) == number && number == 0)
                {
                    return;
                }
            }

            if (firstNumber == "0")
            {
                firstNumber = "";
            }
            firstNumber += number.ToString();

            refreshScreen();
        }

        private void addDecimal()
        {
            if (firstNumber == "")
            {
                firstNumber += "0.";
            }
            else
            {
                if (!firstNumber.Contains("."))
                {
                    firstNumber += ".";
                }
            }
            refreshScreen();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            setOperation("+");
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            clearScreen();
        }

        private void ButtonDivide_Click(object sender, EventArgs e)
        {
            setOperation("/");
        }

        private void ButtonMultiply_Click(object sender, EventArgs e)
        {
            setOperation("X");
        }

        private void ButtonResult_Click(object sender, EventArgs e)
        {
            calculate();
        }

        private void ButtonSubtract_Click(object sender, EventArgs e)
        {
            setOperation("-");
        }

        private void ButtonZero_Click(object sender, EventArgs e)
        {
            addNumber(0);
        }

        private void calculate()
        {
            double result = 0;
            bool zeroDivision = false;
            if (firstNumber == "" || secondNumber == "")
            {
                return;
            }
            switch (operation)
            {
                case "X":
                    result = Calculations.multiply(double.Parse(secondNumber), double.Parse(firstNumber));
                    break;

                case "/":
                    if (double.Parse(firstNumber) == 0)
                    {
                        zeroDivision = true;
                    }
                    else
                    {
                        result = Calculations.divide(double.Parse(secondNumber), double.Parse(firstNumber));
                    }
                    break;

                case "+":
                    result = Calculations.add(double.Parse(secondNumber), double.Parse(firstNumber));
                    break;

                case "-":
                    result = Calculations.subtract(double.Parse(secondNumber), double.Parse(firstNumber));
                    break;
            }
            clearScreen();
            if (zeroDivision)
            {
                mainLabel.Text = "Cannot divide by 0 :(";
            }
            else
            {
                mainLabel.Text = result.ToString().Replace(',', '.');
                firstNumber = mainLabel.Text;
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
        }

        private void clearScreen()
        {
            firstNumber = "";
            secondNumber = "";
            operation = "";
            mainLabel.Text = "";
            secondLabel.Text = "";
            operationLabel.Text = "";
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            addNumber(1);
        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            addNumber(2);
        }

        private void metroSetButton3_Click(object sender, EventArgs e)
        {
            addNumber(3);
        }

        private void metroSetButton4_Click(object sender, EventArgs e)
        {
            addNumber(4);
        }

        private void metroSetButton5_Click(object sender, EventArgs e)
        {
            addNumber(5);
        }

        private void metroSetButton6_Click(object sender, EventArgs e)
        {
            addNumber(6);
        }

        private void metroSetButton7_Click(object sender, EventArgs e)
        {
            addNumber(7);
        }

        private void metroSetButton8_Click(object sender, EventArgs e)
        {
            addNumber(8);
        }

        private void metroSetButton9_Click(object sender, EventArgs e)
        {
            addNumber(9);
        }

        private void refreshScreen()
        {
            mainLabel.Text = firstNumber;
            secondLabel.Text = secondNumber;
            if (secondLabel.Text != "")
            {
                operationLabel.Text = operation;
            }
        }
        private void setOperation(string operation)
        {
            this.operation = operation;
            if (secondNumber == "")
            {
                secondNumber = firstNumber;
                firstNumber = "";
            }
            refreshScreen();
        }

        private void metroSetButton10_Click(object sender, EventArgs e)
        {
            addDecimal();
        }
    }
}
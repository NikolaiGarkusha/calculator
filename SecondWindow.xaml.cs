using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }

        Parser p = new Parser();

        private void result()
        {
            double intOne;
            double intTwo;
            double denominatorOne;
            double denominatorTwo;
            double numeratorOne;
            double numeratorTwo;

            if (intNumberOne.Text != "")
                intOne = getResultOfExpression(intNumberOne.Text);
            else
                intOne = 0;

            if (intNumberTwo.Text != "")
                intTwo = getResultOfExpression(intNumberTwo.Text);
            else
                intTwo = 0;

            if (denominatorNumberOne.Text != "")
                denominatorOne = getResultOfExpression(denominatorNumberOne.Text);
            else
                denominatorOne = 1;

            if (denominatorNumberTwo.Text != "")
                denominatorTwo = getResultOfExpression(denominatorNumberTwo.Text);
            else
                denominatorTwo = 1;

            if (numeratorNumberOne.Text != "")
            {
                numeratorOne = getResultOfExpression(numeratorNumberOne.Text);
                if (numeratorOne == 0)
                {
                    textBox.AppendText("Error: Знаменатель 1 не может равняться 0");
                }
            }

            else
            {
                numeratorOne = 1;
            }  

            if (numeratorNumberTwo.Text != "")
            {
                numeratorTwo = getResultOfExpression(numeratorNumberTwo.Text);
                if (numeratorTwo == 0)
                {
                    textBox.AppendText("Error: Знаменатель 2 не может равняться 0");
                }
            }   
            else
            {
                numeratorTwo = 1;
            }
                

            double[] dataArray = getDenominator(numeratorOne, numeratorTwo);

            denominatorOne = (numeratorOne * intOne + denominatorOne) * dataArray[1];
            denominatorTwo = (numeratorTwo * intTwo + denominatorTwo) * dataArray[2];

            double[] resNumber = getResult(denominatorOne, numeratorOne, denominatorTwo, numeratorTwo, dataArray[0]);
            if (resNumber[0] < resNumber[1])
            {
                denominatorResult.AppendText(resNumber[0].ToString());
                numeratorResult.AppendText(resNumber[1].ToString());
            }
            else
            {
                intResult.AppendText(((int)(resNumber[0] / resNumber[1])).ToString());
                denominatorResult.AppendText((resNumber[0] % resNumber[1]).ToString());
                numeratorResult.AppendText(resNumber[1].ToString());
            }
        }

        private double[] getFactor(double first, double second, double max)
        {
            double[] array = new double[2];
            if (max == first)
            {
                array[1] = first / second;
                array[0] = 1;
            }
            else if (max == second)
            {
                array[0] = second / first;
                array[1] = 1;
            }
            return array;
        }

        private double[] getDenominator(double firstNumber, double secondNumber)
        {
            double[] result = new double[3];
            double maxNumber = System.Math.Max(firstNumber, secondNumber);
            double minNumber = System.Math.Min(firstNumber, secondNumber);
            if(maxNumber%minNumber == 0)
            {
                result[0] = maxNumber;
                if (maxNumber == firstNumber)
                {
                    result[2] = firstNumber / secondNumber;
                    result[1] = 1;
                }
                else if (maxNumber == secondNumber)
                {
                    result[1] = secondNumber / firstNumber;
                    result[2] = 1;
                }
                else
                {
                    result[1] = 1;
                    result[2] = 1;
                }
            }
            else
            {
                for(int i = 2; i < maxNumber; i++)
                {
                    double newMaxNumber = maxNumber;
                    newMaxNumber = maxNumber * i;
                    if (newMaxNumber % minNumber == 0)
                    {
                        result[0] = newMaxNumber;
                        if (maxNumber == firstNumber)
                        {
                            result[2] = newMaxNumber / minNumber;
                            result[1] = i;
                        }
                        else if (maxNumber == secondNumber)
                        {
                            result[1] = newMaxNumber / minNumber;
                            result[2] = i;
                        }
                        break;
                    } 
                }
            }

            return result;
        }

        private double getResultOfExpression(String str)
        {
            return Convert.ToDouble(p.Evaluate(str));
        }

        private double[] getResult(double denFirst, double numFirst, double denSecond, double numSecond, double common)
        {
            double[] result = new double[2];

            string oper = operand.Text;
            switch (oper)
            {
                case "+":
                    result[0] = denFirst + denSecond;
                    result[1] = common;
                    break;
                case "-":
                    result[0] = denFirst - denSecond;
                    result[1] = common;
                    break;
                case "*":
                    result[0] = denFirst * denSecond;
                    result[1] = numFirst * numSecond;
                    break;
                case "/":
                    result[0] = denFirst * numSecond;
                    result[1] = numFirst * denSecond;
                    break;
            }
            return result;
        }

        private void cleanButton_Click(object sender, RoutedEventArgs e)
        {
            denominatorResult.Clear();
            numeratorResult.Clear();
            intResult.Clear();
            denominatorNumberOne.Clear();
            denominatorNumberTwo.Clear();
            numeratorNumberOne.Clear();
            numeratorNumberTwo.Clear();
            intNumberOne.Clear();
            intNumberTwo.Clear();
            textBox.Clear();
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            result();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

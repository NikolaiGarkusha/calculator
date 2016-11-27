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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Parser p = new Parser();
        
        private void resultOfExpression()
        {
            string str = expressionTextBox.Text;
            expressionTextBox.Clear();
                expressionTextBox.AppendText(p.Evaluate(str).ToString());          
        }

        private void zeroButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("0");
        }

        private void oneButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("1");
        }

        private void twoButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("2");
        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("3");
        }

        private void fourButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("4");
        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("5");
        }

        private void sixButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("6");
        }

        private void sevenButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("7");
        }

        private void eightButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("8");
        }

        private void nineButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("9");
        }

        private void lBracketButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("(");
        }

        private void rBracketButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText(")");
        }

        private void factorButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("!");
        }

        private void lnButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("ln(");
        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("log(");
        }

        private void sqrButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("sqrt(");
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText(",");
        }

        private void percentButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("%");
        }

        private void divideButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("/");
        }

        private void multiplyButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("*");
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("-");
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("+");
        }

        private void cleanButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.Clear();
            resultTextBox.Clear();
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = expressionTextBox.Text;
            resultOfExpression();
            
        }

        private void powButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.AppendText("^");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SecondWindow window = new SecondWindow();
            window.Show();
        }
    }
}

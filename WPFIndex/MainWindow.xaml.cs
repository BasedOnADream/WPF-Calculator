using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using org.mariuszgromada.math.mxparser;

namespace WPFIndex
{
    public partial class MainWindow : Window
    {
        public bool canComma = true;
        public ushort lastSing = 0;
        public ushort canSign = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberClick(object sender, RoutedEventArgs e){block.Text += (string)(sender as Button).Content;}

        private void SignClick(object sender, RoutedEventArgs e) 
        {
            if (block.Text != "" && (block.Text.Substring(block.Text.Length - 1) == "×" || block.Text.Substring(block.Text.Length - 1) == "÷" || block.Text.Substring(block.Text.Length - 1) == "-" || block.Text.Substring(block.Text.Length - 1) == "+" || block.Text.Substring(block.Text.Length - 1) == "#" || block.Text.Substring(block.Text.Length - 1) == "!" || block.Text.Substring(block.Text.Length - 1) == "√" || block.Text.Substring(block.Text.Length - 1) == "^")) {block.Text = block.Text.Substring(0, block.Text.Length - 1) + (string)(sender as Button).Content;}
            else {block.Text += (string)(sender as Button).Content;}
            canComma = true;
            lastSing = canSign;
            canSign = Convert.ToUInt16(block.Text.Length);
        }
        private void AddUp(object sender, RoutedEventArgs e)
        {
            try 
            {
                while (block.Text.IndexOf(",") != -1) { block.Text = block.Text.Replace(",", "."); }
                if (block.Text.Substring(block.Text.Length - 1) != "×" && block.Text.Substring(block.Text.Length - 1) != "÷" && block.Text.Substring(block.Text.Length - 1) != "-" && block.Text.Substring(block.Text.Length - 1) != "+" && block.Text.Substring(block.Text.Length - 1) != "#" && block.Text.Substring(block.Text.Length - 1) != "√" && block.Text.Substring(block.Text.Length - 1) != "^" && block.Text.Substring(block.Text.Length - 1) != ",") 
                {
                    org.mariuszgromada.math.mxparser.Expression math = new org.mariuszgromada.math.mxparser.Expression(block.Text);
                    block.Text = Convert.ToString(math.calculate());
                }
                if (block.Text.IndexOf("∞") != -1) { block.Text = block.Text.Replace("∞", ""); }
                if (block.Text.IndexOf("NaN") != -1) { block.Text = block.Text.Replace("NaN", ""); }
                canComma = true;
                canSign = 0;
            }
            catch(Exception err)
            {
                MessageBox.Show(Convert.ToString(err), "Error");
                block.Text = "";
            }
        }
        private void Clear(object sender, RoutedEventArgs e) 
        {
            block.Text = "";
            canComma = true;
            lastSing = 0;
            canSign = 0;
        }

        private void Erase(object sender, RoutedEventArgs e) 
        {
            if(block.Text != "") 
            {
                if (block.Text[^1..] == ",") { canComma = true; }
                block.Text = block.Text[..^1];
            }
        }
        private void Comma(object sender, RoutedEventArgs e) {if (canComma) {block.Text += ","; canComma = false; }}
        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            if (block.Text.Length != 0 && (block.Text.Substring(block.Text.Length - 1) != "×" && block.Text.Substring(block.Text.Length - 1) != "÷" && block.Text.Substring(block.Text.Length - 1) != "-" && block.Text.Substring(block.Text.Length - 1) != "+" && block.Text.Substring(block.Text.Length - 1) != "#" && block.Text.Substring(block.Text.Length - 1) != "√" && block.Text.Substring(block.Text.Length - 1) != "^")) 
            {
                string temp = block.Text.Substring(canSign);
                temp += "*(-1)";
                temp = new DataTable().Compute(temp, null).ToString();
                block.Text = block.Text.Substring(0, canSign);
                block.Text += temp;
            }
        }
    }
}

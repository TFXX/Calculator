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

using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string? leftNum = null;
        string? rightNum = null;
        string? ope = null;



        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(
            IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute
        );

        public MainWindow()
        {
            InitializeComponent();
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
        }

        private void ButtonNum_Click(object sender,RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(null == ope)
            {
                leftNum += btn.Content;
                inputBox.Text = leftNum;
            }
            else
            {
                rightNum += btn.Content;
                inputBox.Text = rightNum;
            }
        }

        private void ButtonOpe_Click(object sender,RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ope = (string)btn.Content;
        }

        private void ButtonEqual_Click(object sender,RoutedEventArgs e)
        {
            double numL = Convert.ToDouble(leftNum);
            double numR = Convert.ToDouble(rightNum);
            double result = 0.0f;

            switch (ope)
            {
            case "+":
                result = numL + numR;
                break;
            case "-":
                result = numL- numR;
                break;
            case "*":
                result = numL * numR;
                break;
            case "/":
                result = numL / numR;
                break;
            }

            leftNum = Convert.ToString(result);
            rightNum = null;
            ope = null;

            inputBox.Text = leftNum;
        }

        private void ButtonDot_Click(object sender,RoutedEventArgs e)
        {
            ;
        }

        private void ButtonCE_Click(object sender,RoutedEventArgs e)
        {
            leftNum = null;
            rightNum = null;
            ope = null;

            inputBox.Text = " ";
        }

        private static void back_num(ref string? numStr)
        {
            if(!System.String.IsNullOrEmpty(numStr))
            {
                numStr = numStr.Remove(numStr.Length - 1);
            }
        }

        private void ButtonBK_Click(object sender,RoutedEventArgs e)
        {
            if(System.String.IsNullOrEmpty(ope))
            {
                back_num(ref leftNum);
                inputBox.Text = leftNum;
            }
            else
            {
                back_num(ref rightNum);
                inputBox.Text = rightNum;
            }
            
        }
    }
}

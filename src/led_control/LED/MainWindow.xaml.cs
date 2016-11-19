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
using System.IO;
using System.Runtime.InteropServices;
namespace LED
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class data
    {
        public static int red = 1;
        public static int blu = 1;
        public static int gre = 1;
    }
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
          
            AsynchronousClient.ip = File.ReadAllText(@"C:\Users\1\Desktop\led_client\led_control\LED\bin\x64\Debug\ip.t");
            InitializeComponent();
            th = 0;
            auto_connect();
       
      
        }
        //public      void log()
        //{
        //    var dd = new MainWindow();
        //    var rgb_ = "RED - " + data.red + " | " + "GREEN - " + data.gre + " | " + "BLU - " + data.blu;
        //textBox.Text = rgb_;
     
        //}

        private void slider_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_red.Minimum = Convert.ToDouble(1);
            slider_red.Maximum = Convert.ToDouble(1023);
            var t = Convert.ToUInt32(e.NewValue);
            var rgb_ = "RED - " + data.red + " | " + "GREEN - " + data.gre + " | " + "BLU - " + data.blu;
            textBox.Text = rgb_;
            data.red = (int)t;
            AsynchronousClient.packet(7, data.red, 6, data.blu, 5, data.gre);
        }

        private void slider_blu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_blu.Minimum = Convert.ToDouble(1);
            slider_blu.Maximum = Convert.ToDouble(1023);
            var t = Convert.ToUInt32(e.NewValue);
            var rgb_ = "RED - " + data.red + " | " + "GREEN - " + data.gre + " | " + "BLU - " + data.blu;
            textBox.Text = rgb_;
            data.blu = (int)t;
            AsynchronousClient.packet(7, data.red, 6, data.blu, 5, data.gre);
        }

        private void slider_gre_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_gre.Minimum = Convert.ToDouble(1);
            slider_gre.Maximum = Convert.ToDouble(1023);
            var t = Convert.ToUInt32(e.NewValue);

            data.gre = (int)t;
            var rgb_ = "RED - " + data.red + " | " + "GREEN - " + data.gre + " | " + "BLUE - " + data.blu;
            textBox.Text = rgb_;
            AsynchronousClient.packet(7, data.red, 6, data.blu, 5, data.gre);
        }
        public static void msg(string s)
        {
            MessageBox.Show(s);
        }
        public static int th = 0;
        public static void auto_connect()
        {
       
            var s = new System.Threading.Thread(AsynchronousClient.StartClient);
          
            if (th == 1)
            {
                s.Abort();
                th = 0;
          
            }
            else
            {
                s.Start();
                th = 1;
            
            }


        }
        private void button_Click_EXIT(object sender, RoutedEventArgs e)
        {
        

            Environment.Exit(1);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            auto_connect();
     //       Environment.Exit(1);
        }

        private void maxi(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;

        }

        private void mini(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void res(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }
        public static bool stop = false;
        public static  void r_()
        {
            try {     
            int r = 1;
            int g = 1;
            int b = 1;
                while (true)
                {
                    data.red = 1;
                    data.gre = 1;
                    data.blu = 1;
         
                    while (r < 1000 && g < 1000 && b < 1000)
                    {
                        r++;
                        g++;
                        b++;
                        r = r + 5;
                        b = b + 5;
                        g = g + 5;
                                data.red = r;
                        data.gre = g;
                        data.blu = b;
                        AsynchronousClient.packet(7, r, 6, b, 5, g);
                        if (stop == true)
                        {
                            System.Threading.Thread.CurrentThread.Interrupt();
                            stop = false;
                        }

                        // if (r == 1023) msg("true on_1023 ");
                    }
                    while (r > 1 && g > 1 && b > 1)
                    {
                        r--;
                        g--;
                        b--;
                        r = r - 5;
                        b = b - 5;
                        g = g - 5;
                        data.blu = b;
                        data.red = r;
                        data.gre = g;
                        AsynchronousClient.packet(7, r, 6, b, 5, g);
                        if (stop == true)
                        {
                            System.Threading.Thread.CurrentThread.Interrupt();
                            stop = false;
                        }
                    }
                }
            }
            catch { }

        }
        private void button_test_Click(object sender, RoutedEventArgs e)
        {
          //  var t =
                new System.Threading.Thread(r_).Start();
            //textBox.Text = data.red.ToString();
        }

        private void button_test_Copy_Click(object sender, RoutedEventArgs e)
        {
  
            stop = true;
            msg("true on ");
        }
    }
}
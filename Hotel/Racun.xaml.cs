using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Net.NetworkInformation;

namespace Hotel
{
	/// <summary>
	/// Interaction logic for Racun.xaml
	/// </summary>
	public partial class Racun : Window
	{
		public Racun()
		{
			this.InitializeComponent();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                tbKonekcija.Text = "Internet dostupan";
                tbKonekcija.Foreground = Brushes.LightGreen;
            }
            else
            {
                tbKonekcija.Text = "Internet nedostupan";
            }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "7";
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "8";
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "9";
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "4";
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "5";
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "6";
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "1";
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "2";
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "3";
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password += "0";
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            if (pbSifra.Password.Length > 0)
            {
                pbSifra.Password = pbSifra.Password.Remove(pbSifra.Password.Length - 1);
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            pbSifra.Password = "";
        }
	}
}
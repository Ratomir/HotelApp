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

namespace Hotel
{
	/// <summary>
	/// Interaction logic for Tastatura.xaml
	/// </summary>
    public partial class Tastatura : Window
    {
        MainWindow glavniProzor;

        Stack<UndoRedo> undoStack= new Stack<UndoRedo>();

        public Tastatura()
        {
            this.InitializeComponent();
            glavniProzor = new MainWindow();
        }

        private bool Testiranje()
        {
            if (rbIme.IsChecked == false && rbPrezime.IsChecked == false)
            {
                MessageBox.Show("Niste čekirali da li želiote da izaberete unos imena ili prezimena. Izaberite jedno.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void DodajZnak(char znak)
        {
            if (Testiranje())
            {
                if (elCapsLock.Fill == Brushes.LightGreen || elShift.Fill == Brushes.LightGreen)
                {
                    elShift.Fill = Brushes.Black;

                    znak = Char.ToUpper(znak);
                }
                if (rbIme.IsChecked == true)
                {
                    undoStack.Push(new UndoRedo(true, tbIme.Text));
                    tbIme.Text += znak;
                }
                else
                {
                    undoStack.Push(new UndoRedo(false, tbPrezime.Text));
                    tbPrezime.Text += znak;
                }
            }
        }

        private void btnCapsLock_Click(object sender, RoutedEventArgs e)
        {
            if (elCapsLock.Fill == Brushes.LightGreen)
            {
                elCapsLock.Fill = Brushes.Black;
            }
            else
            {
                elCapsLock.Fill = Brushes.LightGreen;
            }
        }

        private void btnShift_Click(object sender, RoutedEventArgs e)
        {
            if (elShift.Fill == Brushes.LightGreen)
            {
                elShift.Fill = Brushes.Black;
            }
            else
            {
                elShift.Fill = Brushes.LightGreen;
            }
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('q');
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('w');
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('e');
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('r');
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('t');
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('z');
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('u');
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('i');
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('o');
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('p');
        }

        private void btnŠ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('š');
        }

        private void btnĐ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('đ');
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('a');
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('s');
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('d');
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('f');
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('g');
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('h');
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('j');
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('k');
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('l');
        }

        private void btnČ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('č');
        }

        private void btnĆ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('ć');
        }

        private void btnŽ_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('ž');
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('y');
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('x');
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('c');
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('v');
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('b');
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('n');
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            DodajZnak('m');
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            if (rbIme.IsChecked == true)
            {
                tbIme.Text = "";
            }
            else
            {
                tbPrezime.Text = "";
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (undoStack.Count > 0)
            {
                UndoRedo izmjena = new UndoRedo();
                izmjena = undoStack.Pop();

                if (izmjena.Tip == true)
                {
                    tbIme.Text = izmjena.Tekst;
                }
                else
                {
                    tbPrezime.Text = izmjena.Tekst;
                }
            }
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
    }
}
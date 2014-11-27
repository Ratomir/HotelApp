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

using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static String conString = "Server=localhost;Database=hotel;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);

        string tipSobe = "";
        string tipPlacanja = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void rmiTipSobeJednokrevetna_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(0);
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Jednokrevetna'");
            tipSobe = "Jednokrevetna";
        }

        private void rmiTipSobeDvokrevetna_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(1);
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Dvokrevetna'");
            tipSobe = "Dvokrevetna";
        }

        private void rmiTipSobeApartman_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(2);
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Apartman'");
            tipSobe = "Apartman";
        }

        private void winMain_Loaded(object sender, RoutedEventArgs e)
        {
            staraSirina = this.Width;

            dtpRezervacijaPrijava.SelectedDate = DateTime.Now.Date;
            dtpRezervacijaPrijava.DisplayDateStart = DateTime.Now.Date;
            dtpRezervacijaOdjava.SelectedDate = DateTime.Now.Date.AddDays(1.00);
            dtpRezervacijaOdjava.DisplayDateStart = dtpRezervacijaOdjava.SelectedDate;

            try
            {
                konekcija.Open();

                using (MySqlCommand komanda = new MySqlCommand("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS  'TIP SOBE' FROM soba WHERE stanje = 'Slobodna'", konekcija))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komanda);
                    dataAdapter.Fill(dt);
                    dgRezervacijaSlobodneSobe.DataContext = dt;
                }
                using (MySqlCommand komanda = new MySqlCommand("SELECT brojSobe AS 'BROJ SOBE', stanje AS STANJE, tipSobe AS  'TIP SOBE', datumPrijave AS 'DATUM PRIJAVE', datumOdjave AS 'DATUM ODJAVE' FROM soba", konekcija))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komanda);
                    dataAdapter.Fill(dt);
                    dgPregledBaza.DataContext = dt;
                }
                using (MySqlCommand komanda = new MySqlCommand("SELECT brojSobe AS 'BROJ SOBE', stanje AS STANJE, tipSobe AS  'TIP SOBE', datumPrijave AS 'DATUM PRIJAVE', datumOdjave AS 'DATUM ODJAVE' FROM soba", konekcija))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komanda);
                    dataAdapter.Fill(dt);
                    dgPosjetiociBaza.DataContext = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK);
                Application.Current.Shutdown();
            }
        }

        private void btnRezervacijaUp_Click(object sender, RoutedEventArgs e)
        {
            txtRezervacijaDani.Text = (Convert.ToInt16(txtRezervacijaDani.Text) + 1).ToString();
            dtpRezervacijaOdjava.SelectedDate = DateTime.Now.AddDays(Convert.ToDouble(txtRezervacijaDani.Text));
        }

        private void btnRezervacijaDown_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(txtRezervacijaDani.Text) > 1)
            {
                txtRezervacijaDani.Text = (Convert.ToInt16(txtRezervacijaDani.Text) - 1).ToString();
                dtpRezervacijaOdjava.SelectedDate = DateTime.Now.AddDays(Convert.ToDouble(txtRezervacijaDani.Text));
            }
        }

        private void rbPlacanjeGotovina_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaPlacanje.SelectedItem = cmbRezervacijaPlacanje.Items.GetItemAt(0);
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Hidden;
        }

        private void rbPlacanjeKartica_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaPlacanje.SelectedItem = cmbRezervacijaPlacanje.Items.GetItemAt(1);
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Visible;

            OtvoriRacun();
        }

        private void rbPlacanjeOstalo_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaPlacanje.SelectedItem = cmbRezervacijaPlacanje.Items.GetItemAt(2);
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Hidden;
        }

        private void tbRezervacijaGotovina_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Hidden;
            tipPlacanja = "Gotovina";
        }

        private void tbRezervacijaKartica_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Visible;
            tipPlacanja = "Kartica";
        }

        private void tbRezervacijaOstalo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnRezervacijakartica.Visibility = System.Windows.Visibility.Hidden;
            tipPlacanja = "Ostalo";
        }

        private void txtRezervacijaIme_MouseEnter(object sender, MouseEventArgs e)
        {
            borRezervacijaIme.BorderThickness = new Thickness(2,2,2,2);
            txtRezervacijaIme.Padding = new Thickness(1, 1, 1, 1);

            if (txtRezervacijaIme.Text == "Ime...")
            {
                txtRezervacijaIme.Text = "";
            }
        }

        private void txtRezervacijaIme_MouseLeave(object sender, MouseEventArgs e)
        {
            borRezervacijaIme.BorderThickness = new Thickness(0, 0, 0, 0);
            txtRezervacijaIme.Padding = new Thickness(2, 4, 2, 2);
        }

        private void txtRezervacijaPrezime_MouseEnter(object sender, MouseEventArgs e)
        {
            borRezervacijaPrezime.BorderThickness = new Thickness(2, 2, 2, 2);
            txtRezervacijaPrezime.Padding = new Thickness(1, 1, 1, 1);

            if (txtRezervacijaPrezime.Text == "Prezime...")
            {
                txtRezervacijaPrezime.Text = "";
            }
        }

        private void txtRezervacijaPrezime_MouseLeave(object sender, MouseEventArgs e)
        {
            borRezervacijaPrezime.BorderThickness = new Thickness(0, 0, 0, 0);
            txtRezervacijaPrezime.Padding = new Thickness(2, 4, 2, 2);
        }

        private void btnRezervacijakartica_Click(object sender, RoutedEventArgs e)
        {
            OtvoriRacun();
        }

        private void winMain_Closed(object sender, EventArgs e)
        {
            konekcija.Close();
            Application.Current.Shutdown();
        }

        private void OtvoriRacun()
        {
            Racun windowRacun = new Racun();
            if (windowRacun.ShowDialog() == true)
            {
                Properties.Settings.Default.sifraRacuna = windowRacun.pbSifra.Password;
            }
        }

        private void tbTipSobeJednokrevetna_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Jednokrevetna'");
            tipSobe = "Jednokrevetna";
        }

        private void tbTipSobeDvokrevetna_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Dvokrevetna'");
            tipSobe = "Dvokrevetna";
        }

        private void NapuniBazu(string naredba)
        {
            using (MySqlCommand komanda = new MySqlCommand(naredba, konekcija))
            {
                DataTable dt = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komanda);
                dataAdapter.Fill(dt);
                dgRezervacijaSlobodneSobe.DataContext = dt;
            }
        }

        private void tbTipSobeApartman_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Apartman'");
            tipSobe = "Apartman";
        }

        private void dgRezervacijaSlobodneSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView podatak = (DataRowView)dgRezervacijaSlobodneSobe.SelectedItem;
            if (podatak != null)
            {
                tbRezervacijaBrojSobe.Text = podatak.Row["BROJ SOBE"].ToString();
                tipSobe = podatak.Row["TIP SOBE"].ToString();
                if (tipSobe == "Jednokrevetna")
                {
                    cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(0);
                }
                else if (tipSobe == "Dvokrevetna")
                {
                    cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(1);
                }
                else if(tipSobe == "Apartman")
                {
                    cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(2);
                }
            }
        }

        private void dtpRezervacijaOdjava_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtRezervacijaDani.Text = (dtpRezervacijaOdjava.DisplayDate - dtpRezervacijaPrijava.DisplayDate).TotalDays.ToString();
        }

        private void tbTipSobeSveSobe_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna'");
        }

        private void rmiTipSobeSveSobe_Click(object sender, RoutedEventArgs e)
        {
            cmbRezervacijaTipSobe.SelectedItem = cmbRezervacijaTipSobe.Items.GetItemAt(3);
            NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna'");
        }

        private bool TestirajRezervaciju()
        {
            if (cmbRezervacijaTipSobe.Items.IsEmpty == false)
            {
                MessageBox.Show("Niste izabrali tip sobe. Izabereite jednokrevetnu, dvokrevetnu ili apartman.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);


                cmbRezervacijaTipSobe.BorderThickness = new Thickness(2);

                return false;
            }
            else if (tipPlacanja != "Gotovina" && tipPlacanja != "Kartica" && tipPlacanja != "Ostalo")
            {
                MessageBox.Show("Niste izabrali tip plaćanja. Izaberite gotovinu, karticu ili ostalo.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            else if (txtRezervacijaIme.Text.Length == 0)
            {
                MessageBox.Show("Niste unjeli Vaše ime.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            else if (txtRezervacijaPrezime.Text.Length == 0)
            {
                MessageBox.Show("Niste unjeli Vaše prezime.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        private void btnRezervacijaRezervisi_Click(object sender, RoutedEventArgs e)
        {
            if (TestirajRezervaciju())
            {
                string datumPrijave = dtpRezervacijaPrijava.SelectedDate.Value.Day.ToString() + "." + dtpRezervacijaPrijava.SelectedDate.Value.Month.ToString() + "." + dtpRezervacijaPrijava.SelectedDate.Value.Year.ToString();
                string datumOdjave = dtpRezervacijaOdjava.SelectedDate.Value.Day.ToString() + "." + dtpRezervacijaOdjava.SelectedDate.Value.Month.ToString() + "." + dtpRezervacijaOdjava.SelectedDate.Value.Year.ToString();

                using (MySqlCommand komanda = new MySqlCommand())
                {
                    komanda.Connection = konekcija;
                    komanda.CommandText = "UPDATE soba SET stanje = 'Zauzeta', tipSobe = '" + tipSobe + "', datumPrijave = '" + datumPrijave + "', datumOdjave = '" + datumOdjave + "' WHERE brojSobe = '" + tbRezervacijaBrojSobe.Text + "'";
                    komanda.ExecuteNonQuery();

                    if (tipSobe == "Jednokrevetna")
                    {
                        NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Jednokrevetna'");
                    }
                    else if (tipSobe == "Dvokrevetna")
                    {
                        NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Dvokrevetna'");
                    }
                    else
                    {
                        NapuniBazu("SELECT brojSobe AS  'BROJ SOBE', stanje AS STANJE, tipSobe AS 'TIP SOBE' FROM soba WHERE stanje = 'Slobodna' AND tipSobe='Apartman'");
                    }

                    /*
                     * Kako dobiti max od id-a?
                     */

                    komanda.CommandText = "SELECT MAX(id) AS id FROM gost";
                    string id;
                    using (MySqlDataReader citac = komanda.ExecuteReader())
                    {
                        citac.Read();

                        id = citac["id"].ToString();
                        id = Convert.ToString(Convert.ToInt16(id) + 1);
                    }

                    komanda.CommandText = "INSERT INTO gost (id, brojSobe, imeiprezime, placeno, tipPlacanja) VALUES ('" + id + "', '" + tbRezervacijaBrojSobe.Text + "', '" + (txtRezervacijaIme.Text + " " + txtRezervacijaPrezime.Text) + "', '1', '" + txtRezervacijaPrezime.Text + "')";
                    komanda.ExecuteNonQuery();

                    tbRezervacijaBrojSobe.Text = "#";
                    txtRezervacijaIme.Text = "Ime...";
                    txtRezervacijaPrezime.Text = "Prezime...";
                }
            }

            
        }

        private void expGost_Collapsed(object sender, RoutedEventArgs e)
        {
            expGost.Width -= 249;
            dgPregledBaza.Width += 249;
        }

        private void expGost_Expanded(object sender, RoutedEventArgs e)
        {
            dgPregledBaza.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            expGost.Width += 249;
            dgPregledBaza.Width -= 249;
        }

        double staraSirina = 730;
        private void winMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgPregledBaza.Width += this.ActualWidth - staraSirina;
            staraSirina = this.ActualWidth;
        }

        private void dgPregledBaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView podatak = (DataRowView)dgPregledBaza.SelectedItem;

            if ((podatak != null) && (podatak.Row["STANJE"].ToString() == "Zauzeta"))
            {
                string brojSobe = podatak.Row["BROJ SOBE"].ToString();
                using (MySqlCommand komanda = new MySqlCommand())
                {
                    komanda.Connection = konekcija;
                    komanda.CommandText = "SELECT * FROM gost WHERE brojSobe='" + brojSobe + "'";
                    using (MySqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            tbPregledImePrezime.Text = citac["imeiprezime"].ToString();
                            tbPregledTipPlacanja.Text = citac["tipPlacanja"].ToString();
                            tbPregledPlaceno.Text = (citac["placeno"].ToString() == "1") ? "Plaćeno" : "Nije plaćeno";
                        }
                    }

                    komanda.CommandText = "SELECT * FROM soba WHERE brojSobe='" + brojSobe + "'";
                    using (MySqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            tbPregledDatumDolaska.Text = citac["datumPrijave"].ToString();
                            tbPregledDatumOdjave.Text = citac["datumOdjave"].ToString();
                        }
                    }
                }
            }
        }

        private void ProvjeriCheckBox(CheckBox box1, CheckBox box2)
        {
            /*
             * Nešto neće za ribbon check.
             * */
            if (box2.IsChecked == true)
            {
                box1.IsChecked = false;
                box2.IsChecked = false;
            }
            else
            {
                box1.IsChecked = true;
                box2.IsChecked = true;
            }
        }

        private void cbSobaSlobodna_Click(object sender, RoutedEventArgs e)
        {
            ProvjeriCheckBox(cbSobaSlobodna, cbPregledSlobodnaSoba);
        }

        private void cbSobaZauzeta_Click(object sender, RoutedEventArgs e)
        {
            ProvjeriCheckBox(cbSobaZauzeta, cbPregledZauzetaSoba);
        }

        private void cbSobaJednokrevetna_Click(object sender, RoutedEventArgs e)
        {
            ProvjeriCheckBox(cbSobaJednokrevetna, cbPregledJednokrevetna);
        }

        private void cbSobaDvokrevetna_Click(object sender, RoutedEventArgs e)
        {
            ProvjeriCheckBox(cbSobaDvokrevetna, cbPregledDvokrevetna);
        }

        private void cbSobaApartman_Click(object sender, RoutedEventArgs e)
        {
            ProvjeriCheckBox(cbSobaApartman, cbPregledApartman);
        }

        private void rbTastatura_Click(object sender, RoutedEventArgs e)
        {
            //Tastatura windowTastatura = new Tastatura();
            //if (windowTastatura.ShowDialog() == true)
            //{
            //    txtRezervacijaIme.Text = windowTastatura.tbIme.Text;
            //    txtRezervacijaPrezime.Text = windowTastatura.tbPrezime.Text;
            //}

            if (txtRezervacijaIme.Text == "")
            {
                txtRezervacijaIme.Focus();
            }
            else if (txtRezervacijaPrezime.Text == "")
            {
                txtRezervacijaPrezime.Focus();
            }

            Process.Start("osk.exe");
        }
    }
}

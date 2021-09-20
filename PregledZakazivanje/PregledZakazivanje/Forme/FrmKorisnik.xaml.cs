using PregledZakazivanje.Entiteti;
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

namespace PregledZakazivanje.Forme
{
    /// <summary>
    /// Interaction logic for FrmKorisnik.xaml
    /// </summary>
    public enum Stanje { DODAJ, IZMENI, REGISTER};//REGISTER kod registracije pacijenta s pocetne forme, samo pacijent je odabran i omogucen
    public partial class FrmKorisnik : Window
    {
        RegistrovaniKorisnik korisnik;
        Stanje stanje;
        public DomZdravlja domZdravljaOdabran { get; set; }//ako je lekar

        public FrmKorisnik(RegistrovaniKorisnik korisnik, Stanje stanje=Stanje.DODAJ)//inicijalno u stanju dodaj samo
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.stanje = stanje;

          

            //cbTipKorisnika.Items.Add(TipRegistrovanogKorisnika.pacijent);
            //cbTipKorisnika.Items.Add(TipRegistrovanogKorisnika.administrator);
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipRegistrovanogKorisnika)).Cast<TipRegistrovanogKorisnika>();//enumeracije stavljene u combo tipova
            cbTipKorisnika.DataContext = korisnik;//DataCOntext i Binding Path u XAML kodu
            if (Sistem.listaDomoviZdravlja.Count > 0)
            {
                cbDomoviZdravlja.ItemsSource = Sistem.listaDomoviZdravlja.Where(d => d.obrisano == false);//items source combo boxa na listu dom zdravlja
                if (cbDomoviZdravlja.Items.Count > 0)
                    cbDomoviZdravlja.SelectedIndex = 0;
            }

            if (stanje == Stanje.IZMENI)
            {
                cbTipKorisnika.IsEnabled = false;
                btnDodaj.Content = "Izmeni";
                btnDodajAdresu.Content = "Izmeni adresu";
                lbAdresa.Content = korisnik.Adresa.ToString();
                tbJmbg.IsEnabled = false;

                if(korisnik is Lekar)//ako je korisnik instanca lekara
                {
                    Lekar l = (Lekar)korisnik;
                    if (l.DomZdravlja != null)//ako lekar ima dom zdr
                    {
                        int indeks = -1;
                        foreach(DomZdravlja dz in cbDomoviZdravlja.Items)
                        {
                            if (dz.Sifra == l.DomZdravlja.Sifra)
                                indeks = cbDomoviZdravlja.Items.IndexOf(dz);//naci indeks iz combo boxa dom zdr koji ce se podesiti selektovan

                        }
                        if (indeks > -1)
                            cbDomoviZdravlja.SelectedIndex = indeks;
                        

                    }
                }
            }
          

            if (korisnik.TipKorisnika == TipRegistrovanogKorisnika.lekar)//lekaru prikazati domove zdravlja
            {
                lbDomZdravlja.Visibility = Visibility.Visible;
                cbDomoviZdravlja.Visibility = Visibility.Visible;
            }
            else
            {
                lbDomZdravlja.Visibility = Visibility.Hidden;
                cbDomoviZdravlja.Visibility = Visibility.Hidden;
            }

            if (stanje == Stanje.REGISTER)//dodavanje, ali registracija pacijenta
            {
                cbTipKorisnika.SelectedIndex = 0;
                cbTipKorisnika.IsEnabled = false;
            }

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbJmbg.DataContext = korisnik;
            tbEmail.DataContext = korisnik;

            cbPol.ItemsSource = Enum.GetValues(typeof(Pol)).Cast<Pol>();//pol iz enumeracije
            cbPol.DataContext = korisnik;

            tbLozinka.DataContext = korisnik;

        }

        bool validanJmbg(string jmbg)
        {
            bool validan = true;
            if (korisnik.Jmbg.Length != 13)
            {
                validan = false;
                MessageBox.Show("Jmbg mora imati 13 cifara!");
            }

            char[] nizJmbg = jmbg.ToCharArray();
            for(int i=0; i< nizJmbg.Length; i++)
            {
                if(!int.TryParse(nizJmbg[i].ToString(), out int cifra))
                {
                    validan = false;
                    MessageBox.Show("Jmbg mora imati sve cifre!");
                    break;
                }
            }
            return validan;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {

            if (tbIme.Text == "" || tbPrezime.Text == "" || tbEmail.Text == "" || tbLozinka.Text == "" || tbJmbg.Text == "")//ove vrednosti moraju biti unete
            {
                MessageBox.Show("Niste uneli sve podatke!");
            }
            else if ((this.stanje == Stanje.DODAJ || this.stanje == Stanje.REGISTER) && Sistem.listaRegKorisnici.Any(k => k.Jmbg == tbJmbg.Text))
            {
                MessageBox.Show("Uneli ste postojeci Jmbg!");
            }
            else if (!validanJmbg(korisnik.Jmbg))
            {
               
            }
            else
            {

                this.DialogResult = true;//true signalizira pocetnoj formi koja je pozvala ovu
                if (stanje == Stanje.DODAJ || stanje == Stanje.REGISTER)
                {
                    Sistem.upisUBazu("insert into Adresa values(" +
                                        korisnik.Adresa.Id + ",'" +
                                        korisnik.Adresa.Ulica + "', '" +
                                        korisnik.Adresa.Broj + "', '" +
                                        korisnik.Adresa.Grad + "', '" +
                                        korisnik.Adresa.Drzava + "', 0);");//upis adrese u bazu

                    string sifraDomaZdr = "null";
                    if (korisnik is Lekar)
                    {
                        Lekar l = (Lekar)korisnik;
                        if (l.DomZdravlja != null)
                        {
                            sifraDomaZdr = l.DomZdravlja.Sifra.ToString();
                        }
                    }//ako je null upisace se null za korisnika
                    Sistem.upisUBazu("insert into Korisnik values('" +
                                        korisnik.Ime + "', '" +
                                        korisnik.Prezime + "', '" +
                                        korisnik.Jmbg + "', '" +
                                        korisnik.Email + "', " +
                                        korisnik.Adresa.Id + ", '" +
                                        korisnik.Pol + "', '" +
                                        korisnik.Lozinka + "', '" +
                                        korisnik.TipKorisnika + "', 0, " +
                                        sifraDomaZdr + ");");//upis korisnika u bazu


                    if (korisnik.TipKorisnika == TipRegistrovanogKorisnika.pacijent)//ako je pacijent castovati u Pacijent
                    {
                        Pacijent p = new Pacijent(korisnik);
                        Sistem.listaRegKorisnici.Add(p);
                    }
                    else if (korisnik.TipKorisnika == TipRegistrovanogKorisnika.lekar)//ako je lekar castovati u Lekar
                    {
                        this.domZdravljaOdabran = (DomZdravlja)cbDomoviZdravlja.SelectedItem;
                        Lekar l = new Lekar(korisnik);
                        l.DomZdravlja = domZdravljaOdabran;

                        Sistem.listaRegKorisnici.Add(l);
                    }
                    else
                    {//admin
                        Sistem.listaRegKorisnici.Add(korisnik);
                    }

                }
                else if (stanje == Stanje.IZMENI)
                {
                    Sistem.upisUBazu("update Adresa set " +
                                        "ulica='" + korisnik.Adresa.Ulica + "', " +
                                        "broj='" + korisnik.Adresa.Broj + "', " +
                                        "grad='" + korisnik.Adresa.Grad + "', " +
                                        "drzava='" + korisnik.Adresa.Drzava + "' " +
                                        "where id=" + korisnik.Adresa.Id + ";");//update adrese u bazi
                    string sifraDomaZdr = "null";
                    if (korisnik is Lekar)
                    {
                        Lekar l = (Lekar)korisnik;
                        if (l.DomZdravlja != null)
                        {
                            sifraDomaZdr = l.DomZdravlja.Sifra.ToString();
                        }
                    }
                    Sistem.upisUBazu("update Korisnik set " +
                                        "ime='" + korisnik.Ime + "', " +
                                        "prezime='" + korisnik.Prezime + "', " +
                                        "email='" + korisnik.Email + "', " +
                                        "pol='" + korisnik.Pol + "', " +
                                        "lozinka='" + korisnik.Lozinka + "', " +
                                        "tipKorisnika='" + korisnik.TipKorisnika.ToString() + "', " +
                                        "fkDomZdravlja=" + sifraDomaZdr + " " +
                                        "where jmbg='" + korisnik.Jmbg + "';");//update korisnika u bazi


                }

                this.Close();
            }


        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbTipKorisnika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 1)//ako je selektovan lekar prikazati domove zdravlja
            {
                lbDomZdravlja.Visibility = Visibility.Visible;
                cbDomoviZdravlja.Visibility = Visibility.Visible;
            }
            else
            {
                lbDomZdravlja.Visibility = Visibility.Hidden;
                cbDomoviZdravlja.Visibility = Visibility.Hidden;
            }
        }

        private void cbDomoviZdravlja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            domZdravljaOdabran = (DomZdravlja)cbDomoviZdravlja.SelectedItem;
        }

        private void btnDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            if (this.stanje == Stanje.DODAJ || this.stanje==Stanje.REGISTER)
            {
                FrmAdresa frm = new FrmAdresa(korisnik.Adresa);//za dodavanje i registraciju otvoriti formu za upis adrese i proslediti joj adresu ovog korisnika da se setuje
                frm.ShowDialog();
                if (frm.DialogResult == true)
                {
                    lbAdresa.Content = korisnik.Adresa.ToString();
                }
            }
            else if(this.stanje==Stanje.IZMENI)
            {
                Adresa adresaClone = (Adresa)korisnik.Adresa.Clone();//sacuvati klon za slucaj da se na otvorenoj formi ne potvrdi izmena

                FrmAdresa frm = new FrmAdresa(korisnik.Adresa, Stanje.IZMENI);
                frm.ShowDialog();
                if (frm.DialogResult == true)
                {
                    lbAdresa.Content = korisnik.Adresa.ToString();//dodeliti labeli na ovoj formi adresu koja se dodelila na otvorenoj formi adresa
                }
                else
                {
                    korisnik.Adresa = adresaClone;
                }

            }
        }
    }
}

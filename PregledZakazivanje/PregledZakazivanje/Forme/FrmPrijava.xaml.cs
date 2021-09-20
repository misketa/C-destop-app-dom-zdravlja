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
    /// Interaction logic for FrmPrijava.xaml
    /// </summary>
    public partial class FrmPrijava : Window
    {
        public FrmPrijava()
        {
            InitializeComponent();
        }

        private void btnPrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            string korisnicko = tbKorisnicko.Text;
            string lozinka = pbLozinka.Password.ToString();

            bool pronadjen = false;
            foreach(RegistrovaniKorisnik korisnik in Sistem.listaRegKorisnici)
            {
                if(korisnik.Jmbg==korisnicko && korisnik.Lozinka == lozinka) //u listi svih korisnika trazi da li postoji korisnicko i lozinka
                {
                    pronadjen = true;
                    DialogResult = true;//signalizira pocetnoj formi da je pronadjen korisnik

                    if (korisnik is Lekar)
                    {
                        Lekar lekarClone = new Lekar(korisnik);
                        Lekar l = (Lekar)Sistem.listaRegKorisnici.Where(rk => rk.Jmbg == lekarClone.Jmbg).FirstOrDefault();
                        lekarClone.DomZdravlja = (DomZdravlja)l.DomZdravlja.Clone();
                        foreach (Termin t in l.listaTermina)
                            lekarClone.listaTermina.Add(t);


                        Sistem.korisnikPrijava = lekarClone;  //odavde ce se ucitavati prijavljen korisnik radi provera prava pristupa opcijama

                    }
                    else if (korisnik is Pacijent)
                    {
                        Pacijent pacijentClone = new Pacijent(korisnik);
                        Pacijent p = (Pacijent)Sistem.listaRegKorisnici.Where(rk => rk.Jmbg == pacijentClone.Jmbg).FirstOrDefault();
                        foreach (Termin t in p.listaTermina)
                            pacijentClone.listaTermina.Add(t);
                        foreach (Terapija t in p.listaTerapija)
                            pacijentClone.listaTerapija.Add(t);//klonirane i liste termina i terapija

                        Sistem.korisnikPrijava = pacijentClone;
                    }
                    else
                    {
                        RegistrovaniKorisnik regKorisnikClone = (RegistrovaniKorisnik)korisnik.Clone();
                        Sistem.korisnikPrijava = regKorisnikClone;
                    }
                }
            }

            if (!pronadjen)
                MessageBox.Show("Uneli ste pogresno korisnicko ime i lozinku!");
            else
                this.Close();


        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

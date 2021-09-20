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
    /// Interaction logic for FrmAdresa.xaml
    /// </summary>
    public partial class FrmAdresa : Window
    {
        Adresa adresa = null;
        Stanje stanje;
        public FrmAdresa(Adresa adresa, Stanje stanje=Stanje.DODAJ)//forma se moze otvoriti iz stanja DODAJ(prazan objekat)/IZMENI(postojeci objekat)
        {
            InitializeComponent();
            this.adresa = adresa;
            this.stanje = stanje;

            tbUlica.DataContext = adresa;
            tbBroj.DataContext = adresa;
            tbGrad.DataContext = adresa;
            tbDrzava.DataContext = adresa;//DataContext na objekat uz Binding Path u XAML kodu omogucuje Binding

            if (stanje == Stanje.IZMENI)
                btnDodaj.Content = "Izmeni";


        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            if (stanje == Stanje.DODAJ)//stanje DODAJ, elementi objekta setovani, naci max id i uvecati za 1
            {
                int idAdrese = 0;
                foreach (DomZdravlja dz in Sistem.listaDomoviZdravlja)
                    if (dz.Adresa.Id > idAdrese)
                        idAdrese = dz.Adresa.Id;
                foreach (RegistrovaniKorisnik k in Sistem.listaRegKorisnici)
                    if (k.Adresa.Id > idAdrese)
                        idAdrese = k.Adresa.Id;

                idAdrese++;

                adresa.Id = idAdrese;
            }

            this.Close();
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
    /// Interaction logic for FrmDomZdravlja.xaml
    /// </summary>
    public partial class FrmDomZdravlja : Window
    {
        DomZdravlja domZdravlja;
        Stanje stanje;
        public FrmDomZdravlja(DomZdravlja domZdravlja, Stanje stanje=Stanje.DODAJ)//u stanju DODAJ ili IZMENI
        {
            InitializeComponent();
            this.domZdravlja = domZdravlja;
            this.stanje = stanje;

            tbNazivInstitucije.DataContext = domZdravlja;//uz Binding PATH na property u XAmlu kodu
            if (this.stanje == Stanje.IZMENI)
            {
                this.btnDodaj.Content = "Izmeni";
                this.btnDodajAdresu.Content = "Izmeni adresu";
                lbAdresa.Content = domZdravlja.Adresa.ToString();

            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (tbNazivInstitucije.Text == "")
            {
                MessageBox.Show("Niste uneli naziv institucije!");
            }
            else
            {
                DialogResult = true;
                if (stanje == Stanje.DODAJ)
                {
                    int idDomaZdr = 0;
                    foreach (DomZdravlja dz in Sistem.listaDomoviZdravlja)
                        if (dz.Sifra > idDomaZdr)
                            idDomaZdr = dz.Sifra;
                    idDomaZdr++;//novi ID

                    domZdravlja.Sifra = idDomaZdr;

                    Sistem.upisUBazu("insert into Adresa values(" +
                                                        domZdravlja.Adresa.Id + ",'" +
                                                        domZdravlja.Adresa.Ulica + "', '" +
                                                        domZdravlja.Adresa.Broj + "', '" +
                                                        domZdravlja.Adresa.Grad + "', '" +
                                                        domZdravlja.Adresa.Drzava + "', 0" +
                                                    ");");

                    Sistem.upisUBazu("insert into DomZdravlja values(" +
                                            domZdravlja.Sifra + ",'" +
                                            domZdravlja.NazivInstitucije + "', " +
                                            domZdravlja.Adresa.Id + ", 0);");    //upis u bazu adresa i doma zdravlja


                    Sistem.listaDomoviZdravlja.Add(domZdravlja);

                }
                else if (stanje == Stanje.IZMENI)//izvrsiti update u bazi za adresu i dom zdravlja
                {
                    Sistem.upisUBazu("update Adresa set " +
                                        "ulica='" + domZdravlja.Adresa.Ulica + "', " +
                                        "broj='" + domZdravlja.Adresa.Broj + "', " +
                                        "grad='" + domZdravlja.Adresa.Grad + "', " +
                                        "drzava='" + domZdravlja.Adresa.Drzava + "' " +
                                        "where id=" + domZdravlja.Adresa.Id + ";");

                    Sistem.upisUBazu("update DomZdravlja set " +
                                        "nazivInstitucije='" + domZdravlja.NazivInstitucije + "' " +
                                        "where sifra=" + domZdravlja.Sifra + ";");
                }


                this.Close();
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            if (this.stanje == Stanje.DODAJ)
            {
                FrmAdresa frm = new FrmAdresa(domZdravlja.Adresa);//prosledjuje se property Adresa iz objekta domZdravlja prosledjenog ovoj formi
                frm.ShowDialog();
                if (frm.DialogResult == true)
                {
                    lbAdresa.Content = domZdravlja.Adresa.ToString();//setovati labelu da prikaze dodatu adresu nakon povratka iz Adresa forme
                }
            }
            else if (this.stanje == Stanje.IZMENI)
            {
                Adresa adresaClone = (Adresa)domZdravlja.Adresa.Clone();//klonirati za slucaj da se ne potvrde izmene na otvorenoj formi

                FrmAdresa frm = new FrmAdresa(domZdravlja.Adresa, Stanje.IZMENI);//prosledi se property Adresa formi adresa i stanje izmeni
                frm.ShowDialog();
                if (frm.DialogResult == true)
                {
                    lbAdresa.Content = domZdravlja.Adresa.ToString();//promeni labelu
                }
                else
                {
                    domZdravlja.Adresa = adresaClone;//ako nije potvrdjena adresa dodeli klon
                }

            }
        }
    }
}

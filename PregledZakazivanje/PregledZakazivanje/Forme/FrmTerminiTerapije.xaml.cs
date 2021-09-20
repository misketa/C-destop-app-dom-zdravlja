using PregledZakazivanje.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for FrmTerminiTerapije.xaml
    /// </summary>
    public partial class FrmTerminiTerapije : Window
    {
        ICollectionView viewTermini, viewTerapije;
        Lekar selektovanLekar = null;
        public FrmTerminiTerapije(Lekar lekar)//selektovan lekar iz data grida ili ako se prijavio lekar onda taj lekar 
        {
            InitializeComponent();
            this.selektovanLekar = lekar;

            refreshTerapije_DataGrid(null);//prikaze terapije svih inicijalno ili terapije pacijenta ako je pacijent prijavljen

            refreshDatumVreme();//date picker i sat minuti inicijalizovani

            refreshTermini_DataGrid();//da prikaze termine za odabran datum i prosledjenog lekara
            refreshDodajTerapiju_Visibility();//button prikazan ako je lekar prijavljen, za selektovan termin koji je zakazan

            refreshOpcijePrijavljenog();//admin i lekar ne mogu da zakazuju, dok pacijent ne moze da dodaje termine, brise ih..
        }

        void refreshDatumVreme()//podesi datum na danasnji i vreme sati i minut
        {
            lbTerminiLekara.Content = "Termini lekara: " + selektovanLekar.Ime + " " + selektovanLekar.Prezime + ", Jmbg: " + selektovanLekar.Jmbg;
            dtDatum.SelectedDate = DateTime.Now;

            for (int s = 8; s < 20; s++)
                cbVremeSat.Items.Add(s);
            for (int m = 0; m < 60; m+=15)
                cbVremeMin.Items.Add(m);

            cbVremeSat.SelectedIndex = 4;
            cbVremeMin.SelectedIndex = 0;
        }

        void refreshOpcijePrijavljenog()//za grupe korisnika prikaze ili sakrije odredjene opcije
        {
            if(Sistem.korisnikPrijava is Lekar || Sistem.korisnikPrijava.TipKorisnika==TipRegistrovanogKorisnika.administrator)
            {
                btnZakaziSelektovan.Visibility = Visibility.Hidden;

            }else if(Sistem.korisnikPrijava is Pacijent)
            {
                lbSati.Visibility = Visibility.Hidden;
                cbVremeSat.Visibility = Visibility.Hidden;
                lbMinuta.Visibility = Visibility.Hidden;
                cbVremeMin.Visibility = Visibility.Hidden;
                btnDodajTermin.Visibility = Visibility.Hidden;
                btnBrisiTermin.Visibility = Visibility.Hidden;
            }
        }

        //ako je prijavljen lekar i ako je selektovao termin koji je zakazan prikazuje se dodaj terapiju ispod datagrida terapija i prikazuju se terapije selektovanog pacijenta
        void refreshDodajTerapiju_Visibility()
        {
            btnDodajTerapiju.Visibility = Visibility.Hidden;
            lbOpis.Visibility = Visibility.Hidden;
            tbOpis.Visibility = Visibility.Hidden;

            if (Sistem.korisnikPrijava is Lekar)
            {
                Termin selektovaniTermin = viewTermini.CurrentItem as Termin;
                if (selektovaniTermin != null)
                {
                    if (selektovaniTermin.StatusTermina == StatusTermina.zakazan)
                    {
                        lbOpis.Visibility = Visibility.Visible;
                        tbOpis.Visibility = Visibility.Visible;
                        btnDodajTerapiju.Visibility = Visibility.Visible;
                        refreshTerapije_DataGrid(selektovaniTermin.Pacijent);

                    }
                    else
                    {
                        lbOpis.Visibility = Visibility.Hidden;
                        tbOpis.Visibility = Visibility.Hidden;
                        btnDodajTerapiju.Visibility = Visibility.Hidden;
                        refreshTerapije_DataGrid(null);
                    }
                }
            }
        }

        //prikazuju se termini selektovanog lekara za odabrani datum
        void refreshTermini_DataGrid()
        {
            viewTermini = CollectionViewSource.GetDefaultView(Sistem.listaTermini.Where(
                    t => t.obrisano == false &&
                    t.Lekar.Jmbg==this.selektovanLekar.Jmbg &&
                    t.DatumTermina.Date == (DateTime)dtDatum.SelectedDate.Value.Date
                    ).ToList());
            dgTermini.ItemsSource = viewTermini;
            dgTermini.IsSynchronizedWithCurrentItem = true;
            dgTermini.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }



        void refreshTerapije_DataGrid(Pacijent pacijent)//ako treba za odredjenog pacijenta, inace null
        {
            if (Sistem.korisnikPrijava is Pacijent)//prvenstveno ako je prijavljen pacijent, samo njegove terapije prikazane
            {
                Pacijent p = (Pacijent)Sistem.korisnikPrijava;
                viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(t => t.obrisano == false && t.Pacijent.Jmbg==p.Jmbg).ToList());
                lbTerapijePacijenta.Content = "Terapije pacijenta: " + p.Ime + " " + p.Prezime + ", Jmbg: " + p.Jmbg;

            }else if(pacijent != null)//kada se selektuje zakazani termin pacijenta metodi se prosledi tog pacijenta i prikazane su njegove terapije
            {
                viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(t => t.obrisano == false && t.Pacijent.Jmbg == pacijent.Jmbg).ToList());//za prosledjenog pacijenta
                lbTerapijePacijenta.Content = "Terapije pacijenta: " + pacijent.Ime + " " + pacijent.Prezime + ", Jmbg: " + pacijent.Jmbg;
            }
            else
            {
                viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(t => t.obrisano == false && t.Lekar.Jmbg==this.selektovanLekar.Jmbg).ToList());//sve terapije lekara
                lbTerapijePacijenta.Content = "Terapije svih pacijenata";
            }

            dgTerapije.ItemsSource = viewTerapije;
            dgTerapije.IsSynchronizedWithCurrentItem = true;
            dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void dtDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshTermini_DataGrid();//na promenu datuma refresh termina tog lekara za taj datum
        }

        private void dgTermini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Lekar" || (string)e.Column.Header == "Sifra")//da sakrije ove kolone
            {
                e.Cancel = true;
            }
        }

        private void btnDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            string vremeSat = cbVremeSat.SelectedItem.ToString();
            string vremeMin = cbVremeMin.SelectedItem.ToString();
            DateTime datum = (DateTime)dtDatum.SelectedDate.Value; //preuzmi datum iz date pickera i vreme iz combo boxova

            //proveri da li vec postoji taj termin kod ovog lekara
            bool vecUpisanTermin = Sistem.listaTermini.Any(
                t => t.obrisano==false &&
                t.Lekar.Jmbg==this.selektovanLekar.Jmbg &&
                t.DatumTermina.Date== (DateTime)dtDatum.SelectedDate.Value.Date &&
                t.DatumTermina.Hour== int.Parse(vremeSat) &&
                t.DatumTermina.Minute == int.Parse(vremeMin)
                );

            if (vecUpisanTermin)
            {
                MessageBox.Show("Vec postoji odabrani termin!");
            }
            else
            {
                TimeSpan ts = new TimeSpan(int.Parse(vremeSat), int.Parse(vremeMin), 0);
                datum = datum.Date + ts;//dodeljeno vreme u selektovani datum

                //public Termin(int sifra, Lekar lekar, Pacijent pacijent, DateTime datumTermina, StatusTermina statusTermina)
                int sifraTermin = 0;
                if (Sistem.listaTermini.Count > 0)
                {
                    sifraTermin = Sistem.listaTermini.Max(tm => tm.Sifra);
                }
                sifraTermin++;//nova sifra termina

                Termin t = new Termin(sifraTermin, this.selektovanLekar, null, datum, StatusTermina.slobodan);//inicijalno slobodan i bez pacijenta
                if (t != null)
                    Sistem.upisUBazu("insert into Termin values(" +
                                       t.Sifra + ", '" +
                                       t.Lekar.Jmbg + "', null, '" +
                                       t.DatumTermina + "', '" +
                                       t.StatusTermina.ToString() + "', 0);");//upis termina u bazu



                Sistem.listaTermini.Add(t);
                refreshTermini_DataGrid(); //dodat u listu i refresh prikaza

               


            }

        }

        private void btnZakaziSelektovan_Click(object sender, RoutedEventArgs e) //pacijent zakazuje
        {
            if (Sistem.korisnikPrijava is Pacijent) {
                Pacijent p = (Pacijent)Sistem.korisnikPrijava;

                Termin selektovaniTermin = viewTermini.CurrentItem as Termin;
                if (selektovaniTermin != null)
                {
                    if (selektovaniTermin.StatusTermina == StatusTermina.slobodan)//moze samo slobodan termin zakazati
                    {
                        int indexTermina = Sistem.listaTermini.IndexOf(selektovaniTermin);
                        Sistem.listaTermini[indexTermina].Pacijent = p;
                        Sistem.listaTermini[indexTermina].StatusTermina = StatusTermina.zakazan;//dodeliti pacijenta i status terminu u listi

                        //update u bazi
                        Sistem.upisUBazu("update Termin set fkPacijent='" + p.Jmbg + "', statusTermina='" + StatusTermina.zakazan.ToString() + "' where sifra=" + selektovaniTermin.Sifra + ";");

                        refreshTermini_DataGrid();//refresh prikaza termina
                    }
                    else
                    {
                        MessageBox.Show("Morate odabrati slobodan termin!");
                    }
                } 
            }
        }

        private void dgTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshDodajTerapiju_Visibility();//ako je zakazan omoguciti upis terapije
        }

        private void btnBrisiTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin selektovaniTermin = viewTermini.CurrentItem as Termin;//selektovan termin
            if (selektovaniTermin != null)
            {
                if(
                    selektovaniTermin.StatusTermina==StatusTermina.slobodan ||
                    (selektovaniTermin.StatusTermina == StatusTermina.zakazan && Sistem.korisnikPrijava.TipKorisnika==TipRegistrovanogKorisnika.administrator)
                    )
                {

                    int indexTermina = Sistem.listaTermini.IndexOf(selektovaniTermin);
                    Sistem.listaTermini[indexTermina].obrisano = true;//dodeliti true za logicko brisanje
                    Sistem.upisUBazu("update Termin set obrisano=1 where sifra=" + selektovaniTermin.Sifra + ";");//update u bazi
                    refreshTermini_DataGrid();//refresh prikaza


                }
                else
                {
                    MessageBox.Show("Lekar ne moze brisati zakazane termine");
                }
            }
        }

        private void btnDodajTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Termin selektovaniTermin = viewTermini.CurrentItem as Termin;
            if (selektovaniTermin != null && tbOpis.Text!="" && selektovaniTermin.StatusTermina==StatusTermina.zakazan)
            {
                int sifraTerapije = 0;
                if (Sistem.listaTerapije.Count > 0)
                {
                    sifraTerapije = Sistem.listaTerapije.Max(t => t.Sifra);
                }
                sifraTerapije++;//nova sifra terapije

                //c Terapija(int sifra, Lekar lekar, Pacijent pacijent, string opisTerapije)
                Terapija terapija = new Terapija(sifraTerapije, selektovaniTermin.Lekar, selektovaniTermin.Pacijent, tbOpis.Text);//za terapiju preuzeti podatke iz termina i dodati opis iz text boxa
                Sistem.listaTerapije.Add(terapija);

                Sistem.upisUBazu("insert into Terapija values(" +
                                      terapija.Sifra + ", '" +
                                      terapija.Lekar.Jmbg + "', '" +
                                      terapija.Pacijent.Jmbg + "', '" +
                                      terapija.OpisTerapije + "', 0);");//upis u bazu

                refreshTerapije_DataGrid(selektovaniTermin.Pacijent);//refresh data grida terapije

                //u terminu promeniti status da je unet pregled
                int indexTermina = Sistem.listaTermini.IndexOf(selektovaniTermin);
                Sistem.listaTermini[indexTermina].StatusTermina = StatusTermina.pregledan;
                Sistem.upisUBazu("update Termin set statusTermina='" + StatusTermina.pregledan.ToString() + "' where sifra="+selektovaniTermin.Sifra+";"); //promeniti termin u pregledan
                refreshTermini_DataGrid();


                tbOpis.Text = "";
                lbOpis.Visibility = Visibility.Hidden;
                tbOpis.Visibility = Visibility.Hidden;
                btnDodajTerapiju.Visibility = Visibility.Hidden;//sakriti opcije nakon uspesnog unosa terapije
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

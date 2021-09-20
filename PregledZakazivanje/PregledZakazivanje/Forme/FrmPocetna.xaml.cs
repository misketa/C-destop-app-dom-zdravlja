using PregledZakazivanje.BazaPodataka;
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
    /// Interaction logic for FrmPocetna.xaml
    /// </summary>
    public partial class FrmPocetna : Window
    {
        ICollectionView viewDomoviZdravlja, viewKorisnici, viewTerapije;

    

        public FrmPocetna()
        {
            InitializeComponent();
            Sistem.citajIzBaze();

            inicijalizujDataGridove();//inicijalizuje bindinge na gridove dom zdr, korisnike, terapije
            prikaziDugmadAdmin(false);//inicijalno se ne prikazu buttoni admina, korisnik nije prijavljen
            popuniCbGradoviDzPretraga();//uzme ih iz domova zdravlja i upise
            btnTerminiTerapije.Visibility = Visibility.Hidden;//ovaj button je u Korisnici tabu, prikazace se svima s razlicitim opcijama kad se neki od tipova korisnka prijavi

        }

        void prikaziDugmadAdmin(bool prikazi)
        {
            Visibility vis = Visibility.Visible;
            if (!prikazi)
                vis = Visibility.Hidden;

            btnDodajDomZdravlja.Visibility = vis;
            btnIzmeniDomZdravlja.Visibility = vis;
            btnBrisiDomZdravlja.Visibility = vis;

            btnDodajKorisnika.Visibility = vis;
            btnIzmeniKorisnika.Visibility = vis;
            btnBrisiKorisnika.Visibility = vis;
            btnLekariDezurstvo.Visibility = Visibility.Visible;
        }

        void popuniCbGradoviDzPretraga()
        {
            if (cbGradoviDzPretraga != null)
            {
                if (cbGradoviDzPretraga.Items.Count == 0)
                    cbGradoviDzPretraga.Items.Add("Svi gradovi");//prva opcija se filtriranje domova zdr po gradovima

                foreach (DomZdravlja dz in Sistem.listaDomoviZdravlja)//iz svih domova zdravlja dodati gradove ako ne postoje
                {
                    if (!cbGradoviDzPretraga.Items.Contains(dz.Adresa.Grad))
                        cbGradoviDzPretraga.Items.Add(dz.Adresa.Grad);

                }
                if (cbGradoviDzPretraga.Items.Count > 0)
                    cbGradoviDzPretraga.SelectedIndex = 0;
            }
            
        }

        private void btnDodajDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

            DomZdravlja noviDomZdravlja = new DomZdravlja();
            noviDomZdravlja.Adresa = new Adresa();//napravi novi objekat dom zdr

            FrmDomZdravlja frm = new FrmDomZdravlja(noviDomZdravlja);//prosledi formi na dodavanje
            frm.ShowDialog();
            if (frm.DialogResult == true)//ako se u formi potvrdilo dodavanje osveziti datagrid i combo box za filtriranje gradova ukoliko je dodat novi grad
            {
                refreshDomoviZdravlja_DataGrid();
                popuniCbGradoviDzPretraga();
            }
        }

        private void btnIzmeniDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja selektovanDomZdr = viewDomoviZdravlja.CurrentItem as DomZdravlja;//selektovati prvo iz data grida

            if (selektovanDomZdr != null)
            {

                DomZdravlja stariObjekatDomZdr = (DomZdravlja)selektovanDomZdr.Clone();
                Adresa stariObjekatAdresa = (Adresa)stariObjekatDomZdr.Adresa.Clone();//napraviti klonove ako na otvorenoj formi se ne potvrdi izmena, vratiti ih na isto mesto


                FrmDomZdravlja frm = new FrmDomZdravlja(selektovanDomZdr, Stanje.IZMENI);
                if (frm.ShowDialog() != true)
                {

                    int index = Sistem.listaDomoviZdravlja.IndexOf(selektovanDomZdr);
                    stariObjekatDomZdr.Adresa = stariObjekatAdresa;
                    Sistem.listaDomoviZdravlja[index] = stariObjekatDomZdr;  //nije potvrdjena izmena, u objektu na indeksu liste dodeliti klonove sacuvane pre modifikacije

                }
                else
                {
                    refreshDomoviZdravlja_DataGrid();//osvezi data grid nakon izmene
                }
            }
        }

        private void btnBrisiDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja selektovanDomZdr = viewDomoviZdravlja.CurrentItem as DomZdravlja;//selektuje se prvo iz data grida
            if (selektovanDomZdr != null)
            {
                selektovanDomZdr.obrisano = true;//logicko brisanje
                Sistem.upisUBazu("update DomZdravlja set obrisano=1 where sifra=" + selektovanDomZdr.Sifra + ";");//i u bazi uraditi

                refreshDomoviZdravlja_DataGrid();//refresh prikaza
            }
        }

        //poziva se svaki put kada se napravi neka promena da refreshuje prikaz
        void refreshDomoviZdravlja_DataGrid()
        {
            viewDomoviZdravlja = CollectionViewSource.GetDefaultView(Sistem.listaDomoviZdravlja.Where(dz => dz.obrisano == false).ToList());//oni koji nisu obrisani
            dgDomoviZdravlja.ItemsSource = viewDomoviZdravlja;
            dgDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
            dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        void refreshKorisnici_DataGrid()
        {
            viewKorisnici = CollectionViewSource.GetDefaultView(Sistem.listaRegKorisnici.Where(dz => dz.obrisano == false).ToList());
            dgKorisnici.ItemsSource = viewKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        void refreshTerapije_DataGrid()
        {
            viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(dz => dz.obrisano == false).ToList());
            dgTerapije.ItemsSource = viewTerapije;
            dgTerapije.IsSynchronizedWithCurrentItem = true;
            dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnDodajPacijentaAdmina_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik korisnik = new RegistrovaniKorisnik();
            korisnik.Adresa = new Adresa();

            FrmKorisnik frm = new FrmKorisnik(korisnik);
            frm.ShowDialog();
            if (frm.DialogResult == true)
            {
                refreshKorisnici_DataGrid();
            }
        }

        private void btnIzmeniPacijentaAdmina_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik selektovanKorisnik = viewKorisnici.CurrentItem as RegistrovaniKorisnik;

            if (selektovanKorisnik != null)
            {

                RegistrovaniKorisnik stariObjekatKorisnik = (RegistrovaniKorisnik)selektovanKorisnik.Clone();
                Adresa stariObjekatAdresa = (Adresa)stariObjekatKorisnik.Adresa.Clone();


                FrmKorisnik frm = new FrmKorisnik(selektovanKorisnik, Stanje.IZMENI);
                if (frm.ShowDialog() != true)
                {

                    int index = Sistem.listaRegKorisnici.IndexOf(selektovanKorisnik);
                    stariObjekatKorisnik.Adresa = stariObjekatAdresa;
                    Sistem.listaRegKorisnici[index] = stariObjekatKorisnik;

                }
                else
                {
                    if (selektovanKorisnik is Lekar)
                    {
                        Lekar l = (Lekar)selektovanKorisnik;
                        l.DomZdravlja = frm.domZdravljaOdabran;
                    }

                    refreshKorisnici_DataGrid();
                }
            }

        }



        private void btnBrisiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik selektovanKorisnik = viewKorisnici.CurrentItem as RegistrovaniKorisnik;
            if (selektovanKorisnik != null)
            {
                selektovanKorisnik.obrisano = true;
                Sistem.upisUBazu("update Korisnik set obrisano=1 where jmbg=" + selektovanKorisnik.Jmbg + ";");

                refreshKorisnici_DataGrid();
            }
        }

        //na ovom buttonu pise Registracija kad korisnik nije prijavljen i otvara formu za reg pacijenta. Kad se prijavi otvori profil prijavljenog za izmene
        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            if (btnRegistracija.Content.ToString() == "Registracija")
            {
                RegistrovaniKorisnik korisnik = new RegistrovaniKorisnik();
                korisnik.Adresa = new Adresa();
                FrmKorisnik frm = new FrmKorisnik(korisnik, Stanje.REGISTER);//stanje register, samo pacijenti. Admin moze upisati sve
                frm.ShowDialog();
                if (frm.DialogResult == true)
                    refreshKorisnici_DataGrid();

            }else if(btnRegistracija.Content.ToString() == "Profil")
            {
                foreach(RegistrovaniKorisnik korisnik in Sistem.listaRegKorisnici)
                {
                    if (korisnik.Jmbg == Sistem.korisnikPrijava.Jmbg)//zbog referenciranja pretraga, korisnikPrijava je napravljen kao Clone
                    {
                        FrmKorisnik frm = new FrmKorisnik(korisnik, Stanje.IZMENI);//u stanju izmene profila
                        frm.ShowDialog();
                        if (frm.DialogResult == true)
                            refreshKorisnici_DataGrid();
                    }
                }

               
            }
        }

        //natpis se menja prijava-odjava i obrnuto
        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            
            if (btnPrijava.Content.ToString() == "Prijava")//ako je prijava otvoriti formu za prijavu
            {
                FrmPrijava frm = new FrmPrijava();
                frm.ShowDialog();

                if (frm.DialogResult == true)
                {
                    btnTerminiTerapije.Visibility = Visibility.Visible;//prikazuje se opcija za termine i terapije

                    btnPrijava.Content = "Odjava";//nakon prijave promeniti tekst na odjava
                    btnRegistracija.Content = "Profil";
                    if (Sistem.korisnikPrijava is Pacijent)
                    {

                    }
                    else if (Sistem.korisnikPrijava is Lekar)
                    {

                    }
                    else if(Sistem.korisnikPrijava.TipKorisnika==TipRegistrovanogKorisnika.administrator)
                    {
                        prikaziDugmadAdmin(true);//adminu prikazati opcije azuriranja ispod svakog datagrida
                    }
                }

            }else if(btnPrijava.Content.ToString() == "Odjava")
            {
                btnPrijava.Content = "Prijava";
                btnRegistracija.Content = "Registracija";
                Sistem.korisnikPrijava = null; //odjava opet vrati na nul korisnik prijava
                prikaziDugmadAdmin(false);
                btnTerminiTerapije.Visibility = Visibility.Hidden;
            }
            

        }

        void inicijalizujDataGridove()
        {
            viewDomoviZdravlja = CollectionViewSource.GetDefaultView(Sistem.listaDomoviZdravlja.Where(dz => dz.obrisano == false).ToList());//oni koji nisu logicki obrisani
            dgDomoviZdravlja.ItemsSource = viewDomoviZdravlja;
            dgDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
            dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            viewKorisnici = CollectionViewSource.GetDefaultView(Sistem.listaRegKorisnici.Where(dz => dz.obrisano == false).ToList());
            dgKorisnici.ItemsSource = viewKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(dz => dz.obrisano == false).ToList());
            dgTerapije.ItemsSource = viewTerapije;
            dgTerapije.IsSynchronizedWithCurrentItem = true;
            dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }




        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Lozinka")//da ne prikaze lozinku 
            {
                e.Cancel = true;
            }
        }





        /// <summary>
        /// FILTRIRANJA PODATAKA, EVENTOVI DODATI NA COMBO BOX U DOMOVIMA ZDRAVLJA I NA TEXT BOXOVE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tbDpdatniFilterDomoviZdravlja_TextChanged(object sender, TextChangedEventArgs e)//event na promenu teksta text boxa za filter
        {
            filtriranjeDomovaZdravlja(); 
        }

        private void cbGradoviDzPretraga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtriranjeDomovaZdravlja();
        }

        private void tbFiltriranjeKorisnik_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtriranjeKorisnika();
        }

        private void tbFiltrirajTerapije_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtrirajTerapije();
        }

        void filtrirajTerapije()
        {

            string filtriranjeTerapija = tbFiltrirajTerapije.Text;
            if (filtriranjeTerapija == "")
                refreshTerapije_DataGrid();
            else
            {
                viewTerapije = CollectionViewSource.GetDefaultView(Sistem.listaTerapije.Where(
                    t => t.obrisano == false &&
                    (
                      t.OpisTerapije.ToUpper().Contains(filtriranjeTerapija.ToUpper()) ||
                      t.Lekar.Ime.ToUpper().Contains(filtriranjeTerapija.ToUpper()) ||
                      t.Lekar.Prezime.ToUpper().Contains(filtriranjeTerapija.ToUpper())   //filtrira terapije po opisu i lekaru
                    )
                    ).ToList());
                dgTerapije.ItemsSource = viewTerapije;
                dgTerapije.IsSynchronizedWithCurrentItem = true;
                dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            }

           
        }

        void filtriranjeKorisnika()
        {
            string filtriranjeKorisnika = tbFiltriranjeKorisnik.Text;
            if (filtriranjeKorisnika == "")
                refreshKorisnici_DataGrid();
            else
            {
                viewKorisnici = CollectionViewSource.GetDefaultView(Sistem.listaRegKorisnici.Where(
                    dz => dz.obrisano == false &&
                    (
                        dz.Ime.ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Prezime.ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Email.ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.TipKorisnika.ToString().ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Adresa.Ulica.ToString().ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Adresa.Broj.ToString().ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Adresa.Grad.ToString().ToUpper().Contains(filtriranjeKorisnika.ToUpper()) ||
                        dz.Adresa.Drzava.ToString().ToUpper().Contains(filtriranjeKorisnika.ToUpper()) 
                    )
                    ).ToList());
                dgKorisnici.ItemsSource = viewKorisnici;
                dgKorisnici.IsSynchronizedWithCurrentItem = true;
                dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void btnTerminiTerapije_Click(object sender, RoutedEventArgs e) 
        {

            RegistrovaniKorisnik selektovanKorisnik = viewKorisnici.CurrentItem as RegistrovaniKorisnik;

            if(Sistem.korisnikPrijava!=null && Sistem.korisnikPrijava is Lekar)//ako se prijavio lekar prikazuju se njegovi termini
            {
                Lekar l = (Lekar)Sistem.korisnikPrijava;
                FrmTerminiTerapije frm = new FrmTerminiTerapije(l);
                frm.ShowDialog();

            }
            else if (selektovanKorisnik != null)//adminu i pacijentu se prikazuju termini selektovanog lekara
            {
                if(selektovanKorisnik is Lekar)
                {
                    Lekar l = (Lekar)selektovanKorisnik;
                    FrmTerminiTerapije frm = new FrmTerminiTerapije(l);
                    frm.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("Morate selektovati lekara");
            }
        }

        private void btnLekariDezurstvo_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik selektovanKorisnik = viewKorisnici.CurrentItem as RegistrovaniKorisnik;
            if (selektovanKorisnik != null)//adminu i pacijentu se prikazuju dezurstva selektovanog lekara
            {
                if (selektovanKorisnik is Lekar)
                {
                    Lekar l = (Lekar)selektovanKorisnik;
                    FrmDezurstva frm = new FrmDezurstva(l);
                    frm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("Morate selektovati lekara");
            }

            //FrmDezurstva frm = new FrmDezurstva();
            //frm.ShowDialog();
        }

        bool postojiLekar_DomZdravlja(DomZdravlja dz, string deoImePrez)//da li dom zdravlja postoji kod nekod lekara na osnovu dela imena ili prezimena lekara. Za filtriranje
        {
            bool postoji = false;
            foreach (RegistrovaniKorisnik korisnik in Sistem.listaRegKorisnici)
            {
                if (korisnik is Lekar)
                {
                    Lekar l = (Lekar)korisnik;
                    if (l.DomZdravlja.Sifra == dz.Sifra && (l.Ime.ToUpper().Contains(deoImePrez.ToUpper()) || l.Prezime.ToUpper().Contains(deoImePrez.ToUpper())))
                    {
                        postoji = true;
                        break;
                    }
                }
            }
            return postoji;
        }


        void filtriranjeDomovaZdravlja()
        {
            string cbGradovi = cbGradoviDzPretraga.SelectedItem.ToString();
            string tbDodatnaPretraga = tbDpdatniFilterDomoviZdravlja.Text;

            if (cbGradovi == "Svi gradovi" && tbDodatnaPretraga == "")
                refreshDomoviZdravlja_DataGrid();

            else if(cbGradovi!="Svi gradovi" && tbDodatnaPretraga == "")
            {
                viewDomoviZdravlja = CollectionViewSource.GetDefaultView(Sistem.listaDomoviZdravlja.Where(dz => dz.obrisano == false && dz.Adresa.Grad==cbGradovi).ToList());
                dgDomoviZdravlja.ItemsSource = viewDomoviZdravlja;
                dgDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
                dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            }else if(cbGradovi=="Svi gradovi" && tbDodatnaPretraga != "")
            {
                viewDomoviZdravlja = CollectionViewSource.GetDefaultView(
                    Sistem.listaDomoviZdravlja.Where(
                        dz => dz.obrisano == false && 
                        (
                        dz.NazivInstitucije.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                        dz.Adresa.Ulica.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                        dz.Adresa.Broj.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                        postojiLekar_DomZdravlja(dz, tbDpdatniFilterDomoviZdravlja.Text)
                        )
                        ).ToList()
                    );
                dgDomoviZdravlja.ItemsSource = viewDomoviZdravlja;
                dgDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
                dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            else if (cbGradovi != "Svi gradovi" && tbDodatnaPretraga != "")
            {
                viewDomoviZdravlja = CollectionViewSource.GetDefaultView(
                   Sistem.listaDomoviZdravlja.Where(
                       dz => dz.obrisano == false &&
                       (
                       dz.NazivInstitucije.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                       dz.Adresa.Ulica.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                       dz.Adresa.Broj.ToUpper().Contains(tbDpdatniFilterDomoviZdravlja.Text.ToUpper()) ||
                       postojiLekar_DomZdravlja(dz, tbDpdatniFilterDomoviZdravlja.Text) ||
                       dz.Adresa.Grad == cbGradovi
                       )
                       ).ToList()
                   );
                dgDomoviZdravlja.ItemsSource = viewDomoviZdravlja;
                dgDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
                dgDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}

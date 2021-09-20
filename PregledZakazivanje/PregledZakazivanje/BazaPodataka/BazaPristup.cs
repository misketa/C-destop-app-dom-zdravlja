using PregledZakazivanje.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PregledZakazivanje.BazaPodataka
{
    public class BazaPristup
    {

        public static string prihvatiConnectionString()
        {
            string debugPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string bazaPodatakaFolder = Path.GetFullPath(Path.Combine(debugPath, @"..\..\BazaPodataka\"));
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + bazaPodatakaFolder + @"BazaTermini.mdf;Integrated Security=True";

            return connectionString;
        }

        public static void upisUBazu(string sql)  // za upis izmenu i brisanje
        {

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            finally
            {
                conn.Close();
            }


        }



        public static ObservableCollection<Adresa> citajAdrese() //vraca listu observabilne kolekcije adresa
        {

            ObservableCollection<Adresa> lst = new ObservableCollection<Adresa>();//prvo prazna lista

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());//prihvati connection string koji pokazuje na BazaPodataka folder
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Adresa", conn);//selektuje sve iz tabele Adresa
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
            while (sqlDataReader.Read())//iteracije kroz listu ucitanih redova tabele
            {
                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                string ulica = sqlDataReader.GetValue(1).ToString();
                string broj = sqlDataReader.GetValue(2).ToString();
                string grad = sqlDataReader.GetValue(3).ToString();
                string drzava = sqlDataReader.GetValue(4).ToString();
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(5));

                Adresa adresa = new Adresa(id, ulica, broj, grad, drzava);//napravljen objekat od celija iz tekuceg reda
                adresa.obrisano = obrisano;
                lst.Add(adresa); //dodat u listu
            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();

            return lst;
        }


       public static ObservableCollection<DomZdravlja> citajDomoveZdravlja()
        {
            ObservableCollection<DomZdravlja> lst = new ObservableCollection<DomZdravlja>();
            SqlConnection conn = new SqlConnection(prihvatiConnectionString());
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from DomZdravlja", conn);
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                int sifra = Convert.ToInt32(sqlDataReader.GetValue(0));
                string nazivInstitucije = sqlDataReader.GetValue(1).ToString();
                int idAdrese = Convert.ToInt32(sqlDataReader.GetValue(2));
                Adresa adresa = citajAdrese().Where(a => a.Id == idAdrese).FirstOrDefault();
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(3));
                DomZdravlja domZdr = new DomZdravlja(sifra, nazivInstitucije, adresa);
                domZdr.obrisano = obrisano;
                lst.Add(domZdr);

            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();

            return lst;

        }

        public static ObservableCollection<RegistrovaniKorisnik> citajRegistrovaneKorisnike()
        {
            ObservableCollection<RegistrovaniKorisnik> lst = new ObservableCollection<RegistrovaniKorisnik>();

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Korisnik", conn);
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();

            while (sqlDataReader.Read())
            {



                string ime = sqlDataReader.GetValue(0).ToString();
                string prezime = sqlDataReader.GetValue(1).ToString();
                string jmbg = sqlDataReader.GetValue(2).ToString();
                string email = sqlDataReader.GetValue(3).ToString();
                int idAdrese = Convert.ToInt32(sqlDataReader.GetValue(4));
                Adresa adresa = citajAdrese().Where(a => a.Id == idAdrese).FirstOrDefault();
                string polStr = sqlDataReader.GetValue(5).ToString();
                Pol pol = (Pol)Enum.Parse(typeof(Pol), polStr);
                string lozinka = sqlDataReader.GetValue(6).ToString();
                string tipKorisnikaStr = sqlDataReader.GetValue(7).ToString();
                TipRegistrovanogKorisnika tipKorisnika = (TipRegistrovanogKorisnika)Enum.Parse(typeof(TipRegistrovanogKorisnika), tipKorisnikaStr);
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(8));


                int? sifraDomZdravlja=-1;
                DomZdravlja domZdravlja = null;
                if (!(sqlDataReader.GetValue(9) is DBNull))
                {
                    sifraDomZdravlja = Convert.ToInt32(sqlDataReader.GetValue(9));
                    domZdravlja = citajDomoveZdravlja().Where(domZ => domZ.Sifra == sifraDomZdravlja).FirstOrDefault();
                }

                if (tipKorisnika == TipRegistrovanogKorisnika.administrator)
                {
                    RegistrovaniKorisnik regKorisnik = new RegistrovaniKorisnik(ime, prezime, jmbg, email, adresa, pol, lozinka, tipKorisnika);
                    regKorisnik.obrisano = obrisano;
                    lst.Add(regKorisnik);

                }
                else if(tipKorisnika==TipRegistrovanogKorisnika.pacijent)
                {

                    Pacijent pacijent = new Pacijent(ime, prezime, jmbg, email, adresa, pol, lozinka, tipKorisnika);
                    pacijent.obrisano = obrisano;

                    //ne moze se ucitati ovde, connection pool problem. Termini i terapije citaju korisnike a korisnici njih. U metodi koja ucitava sve liste iz baze..
                    //foreach (Termin t in citajTermine())
                    //    if (t.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                    //        pacijent.listaTermina.Add(t);
                    //foreach (Terapija t in citajTerapije())
                    //    if (t.Pacijent.Jmbg.Equals(pacijent.Jmbg))
                    //        pacijent.listaTerapija.Add(t);

                    lst.Add(pacijent);

                }else if (tipKorisnika == TipRegistrovanogKorisnika.lekar && domZdravlja!=null)
                {
                    Lekar lekar = new Lekar(ime, prezime, jmbg, email, adresa, pol, lozinka, tipKorisnika, domZdravlja);
                    lekar.DomZdravlja = (DomZdravlja)domZdravlja.Clone();
                    lekar.obrisano = obrisano;

                    //foreach (Termin t in citajTermine())
                    //    if (t.Lekar.Jmbg.Equals(lekar.Jmbg))
                    //        lekar.listaTermina.Add(t);

                    lst.Add(lekar);
                }

            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();


            return lst;
        }

        public static ObservableCollection<Termin> citajTermine()
        {
            ObservableCollection<RegistrovaniKorisnik> lstRegKorisnici = citajRegistrovaneKorisnike();
            ObservableCollection<Termin> lst = new ObservableCollection<Termin>();

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Termin", conn);
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
            while (sqlDataReader.Read())
            {



                int sifra = Convert.ToInt32(sqlDataReader.GetValue(0));

                string jmbgLekara = sqlDataReader.GetValue(1).ToString();
                Lekar lekar = (Lekar)lstRegKorisnici.Where(k => k.Jmbg == jmbgLekara && k.TipKorisnika==TipRegistrovanogKorisnika.lekar).FirstOrDefault();
                string jmbgPacijenta = sqlDataReader.GetValue(2).ToString();
                Pacijent pacijent = (Pacijent)lstRegKorisnici.Where(k => k.Jmbg == jmbgPacijenta && k.TipKorisnika == TipRegistrovanogKorisnika.pacijent).FirstOrDefault();
                DateTime datumTermina = Convert.ToDateTime(sqlDataReader.GetValue(3).ToString());
                string statusTerminaStr = sqlDataReader.GetValue(4).ToString();
                StatusTermina statusTermina = (StatusTermina)Enum.Parse(typeof(StatusTermina), statusTerminaStr);
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(5));

                Termin t = new Termin(sifra, lekar, pacijent, datumTermina, statusTermina);
                t.obrisano = obrisano;

                lst.Add(t);


            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();

            return lst;

        }

        public static ObservableCollection<Terapija> citajTerapije()
        {
            ObservableCollection<RegistrovaniKorisnik> lstRegKorisnici = citajRegistrovaneKorisnike();
            ObservableCollection<Terapija> lst = new ObservableCollection<Terapija>();

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Terapija", conn);
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
            while (sqlDataReader.Read())
            {



                int sifra = Convert.ToInt32(sqlDataReader.GetValue(0));

                string jmbgLekara = sqlDataReader.GetValue(1).ToString();
                Lekar lekar = (Lekar)lstRegKorisnici.Where(k => k.Jmbg == jmbgLekara && k.TipKorisnika == TipRegistrovanogKorisnika.lekar).FirstOrDefault();
                string jmbgPacijenta = sqlDataReader.GetValue(2).ToString();
                Pacijent pacijent = (Pacijent)lstRegKorisnici.Where(k => k.Jmbg == jmbgPacijenta && k.TipKorisnika == TipRegistrovanogKorisnika.pacijent).FirstOrDefault();
                string opisTerapije = sqlDataReader.GetValue(3).ToString();
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(4));

                Terapija t = new Terapija(sifra, lekar, pacijent, opisTerapije);
                t.obrisano = obrisano;

                lst.Add(t);

            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();

            return lst;

        }

        public static ObservableCollection<Dezurstva> citajDezurstva() //vraca listu observabilne kolekcije adresa
        {
            ObservableCollection<RegistrovaniKorisnik> lstRegKorisnici = citajRegistrovaneKorisnike();
            ObservableCollection<Dezurstva> lst = new ObservableCollection<Dezurstva>();//prvo prazna lista

            SqlConnection conn = new SqlConnection(prihvatiConnectionString());//prihvati connection string koji pokazuje na BazaPodataka folder
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Dezurstva", conn);//selektuje sve iz tabele dezurstva
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();
            while (sqlDataReader.Read())//iteracije kroz listu ucitanih redova tabele
            {
                string jmbgLekara = sqlDataReader.GetValue(0).ToString();
                Lekar lekar = (Lekar)lstRegKorisnici.Where(k => k.Jmbg == jmbgLekara && k.TipKorisnika == TipRegistrovanogKorisnika.lekar).FirstOrDefault();
                DateTime pocetak = Convert.ToDateTime(sqlDataReader.GetValue(1).ToString());
                DateTime kraj = Convert.ToDateTime(sqlDataReader.GetValue(2).ToString());

                Dezurstva dezurstva = new Dezurstva(lekar, pocetak, kraj);//napravljen objekat od celija iz tekuceg reda
                
                lst.Add(dezurstva); //dodat u listu
            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();

            return lst;
        }




    }
}

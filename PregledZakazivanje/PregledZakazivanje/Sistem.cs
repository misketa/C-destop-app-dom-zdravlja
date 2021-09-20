using PregledZakazivanje.BazaPodataka;
using PregledZakazivanje.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PregledZakazivanje
{
    public class Sistem
    {
        public static ObservableCollection<RegistrovaniKorisnik> listaRegKorisnici = new ObservableCollection<RegistrovaniKorisnik>();
        public static ObservableCollection<DomZdravlja> listaDomoviZdravlja = new ObservableCollection<DomZdravlja>();
        public static ObservableCollection<Termin> listaTermini = new ObservableCollection<Termin>();
        public static ObservableCollection<Terapija> listaTerapije = new ObservableCollection<Terapija>();  //liste vidljive svugde
        public static ObservableCollection<Dezurstva> listaDezurstava = new ObservableCollection<Dezurstva>();

        public static RegistrovaniKorisnik korisnikPrijava=null;//odavde se pristupa prijavljenom korisniku radi provere prava pristupa

        public static void citajIzBaze()
        {
            listaRegKorisnici = BazaPristup.citajRegistrovaneKorisnike();
            listaDomoviZdravlja= BazaPristup.citajDomoveZdravlja();
            listaTermini = BazaPristup.citajTermine();
            listaTerapije = BazaPristup.citajTerapije(); //cita sve liste iz baze
            listaDezurstava = BazaPristup.citajDezurstva();

            foreach(RegistrovaniKorisnik korisnik in listaRegKorisnici)
            {
                if(korisnik is Lekar)
                {
                    Lekar l = (Lekar)korisnik;
  
      
                    foreach (Termin t in listaTermini)
                        if (t.Lekar.Jmbg == l.Jmbg)
                            l.listaTermina.Add(t);  //dodaje se lista termina za lekara
                    

                }else if(korisnik is Pacijent)
                {
                    Pacijent p = (Pacijent)korisnik;

                    foreach (Termin t in listaTermini)
                        if(t.Pacijent!=null)
                            if (t.Pacijent.Jmbg == p.Jmbg)
                                p.listaTermina.Add(t);
                    foreach (Terapija t in listaTerapije)
                        if (t.Pacijent.Jmbg == p.Jmbg)
                            p.listaTerapija.Add(t);
                    
                }
            }

            
        }

        public static void upisUBazu(string sql)
        {
            //MessageBox.Show(sql);
            BazaPristup.upisUBazu(sql);
        }
    }
}

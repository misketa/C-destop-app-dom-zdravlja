using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public class Lekar:RegistrovaniKorisnik, ICloneable, INotifyPropertyChanged
    {
        DomZdravlja domZdravlja;
        public ObservableCollection<Termin> listaTermina = new ObservableCollection<Termin>();

        public Lekar(string ime, string prezime, string jmbg, string email, Adresa adresa, Pol pol, string lozinka, TipRegistrovanogKorisnika tipKorisnika, DomZdravlja domZdravlja)
            :base(ime, prezime, jmbg, email, adresa, pol, lozinka, tipKorisnika)
        {

        }
        public Lekar()
        {

        }

        public object Clone()
        {
            Lekar lekarClone = new Lekar();
            lekarClone.Ime = Ime;
            lekarClone.Prezime = Prezime;
            lekarClone.Jmbg = Jmbg;
            lekarClone.Email = Email;
            lekarClone.Adresa = (Adresa)Adresa.Clone();
            lekarClone.Pol = Pol;
            lekarClone.Lozinka = Lozinka;
            lekarClone.TipKorisnika = TipKorisnika;
            lekarClone.domZdravlja = (DomZdravlja)DomZdravlja.Clone();
            return lekarClone;

        }
        public Lekar(RegistrovaniKorisnik k)
        {
            this.Ime = k.Ime;
            this.Prezime = k.Prezime;
            this.Jmbg = k.Jmbg;
            this.Email = k.Email;
            this.Adresa = (Adresa)k.Adresa.Clone();
            this.Pol = k.Pol;
            this.Lozinka = k.Lozinka;
            this.TipKorisnika = k.TipKorisnika;
 
        }

        public DomZdravlja DomZdravlja
        {
            get
            {
                return domZdravlja;
            }
            set
            {
                domZdravlja = value;
                OnPropertyChanged("DomZdravlja");
            }
        }


    }
}

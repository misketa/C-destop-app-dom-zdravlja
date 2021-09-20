using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public enum Pol { muski, zenski}
    public enum TipRegistrovanogKorisnika { pacijent, lekar, administrator}

    public class RegistrovaniKorisnik : ICloneable, INotifyPropertyChanged
    {
        string ime;
        string prezime;
        string jmbg;
        string email;
        Adresa adresa;
        Pol pol;
        string lozinka;
        TipRegistrovanogKorisnika tipKorisnika;
        public bool obrisano = false;

        public RegistrovaniKorisnik(string ime, string prezime, string jmbg, string email, Adresa adresa, Pol pol, string lozinka, TipRegistrovanogKorisnika tipKorisnika)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.email = email;
            this.adresa = adresa;
            this.pol = pol;
            this.lozinka = lozinka;
            this.tipKorisnika = tipKorisnika;
        }

        public RegistrovaniKorisnik() { 
        }

        public object Clone()
        {
            RegistrovaniKorisnik regKorisnikClone = new RegistrovaniKorisnik();
            regKorisnikClone.Ime = Ime;
            regKorisnikClone.Prezime = Prezime;
            regKorisnikClone.Jmbg = Jmbg;
            regKorisnikClone.Email = Email;
            regKorisnikClone.Adresa = (Adresa)Adresa.Clone();
            regKorisnikClone.Pol = Pol;
            regKorisnikClone.Lozinka = Lozinka;
            regKorisnikClone.TipKorisnika = TipKorisnika;
            return regKorisnikClone;

        }


        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public Adresa Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }
        public Pol Pol
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
                OnPropertyChanged("Pol");
            }
        }
        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }
        public TipRegistrovanogKorisnika TipKorisnika
        {
            get
            {
                return tipKorisnika;
            }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return Ime + " " + Prezime + " " + Jmbg;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public class Pacijent:RegistrovaniKorisnik
    {
        public ObservableCollection<Termin> listaTermina = new ObservableCollection<Termin>();
        public ObservableCollection<Terapija> listaTerapija = new ObservableCollection<Terapija>();

        public Pacijent(string ime, string prezime, string jmbg, string email, Adresa adresa, Pol pol, string lozinka, TipRegistrovanogKorisnika tipKorisnika)
        : base(ime, prezime, jmbg, email, adresa, pol, lozinka, tipKorisnika)
            {

            }
        public Pacijent()
        {

        }
        public Pacijent(RegistrovaniKorisnik k)
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

        public object Clone()
        {
            Pacijent pacijentClone = new Pacijent();
            pacijentClone.Ime = Ime;
            pacijentClone.Prezime = Prezime;
            pacijentClone.Jmbg = Jmbg;
            pacijentClone.Email = Email;
            pacijentClone.Adresa = (Adresa)Adresa.Clone();
            pacijentClone.Pol = Pol;
            pacijentClone.Lozinka = Lozinka;
            pacijentClone.TipKorisnika = TipKorisnika;
            return pacijentClone;

        }


    }
}

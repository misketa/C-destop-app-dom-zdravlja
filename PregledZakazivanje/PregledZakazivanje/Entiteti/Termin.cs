using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public enum StatusTermina { slobodan, zakazan, pregledan}
    public class Termin
    {
        int sifra;
        Lekar lekar;
        Pacijent pacijent;
        DateTime datumTermina;
        StatusTermina statusTermina;
        
        public bool obrisano;

        public Termin(int sifra, Lekar lekar, Pacijent pacijent, DateTime datumTermina, StatusTermina statusTermina)
        {
            this.sifra = sifra;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.datumTermina = datumTermina;
            this.statusTermina = statusTermina;
        }
        public Termin()
        {

        }
        public object Clone()
        {
            Termin terminClone = new Termin();
            terminClone.Sifra = Sifra;
            terminClone.Lekar = Lekar;
            terminClone.Pacijent = Pacijent;
            terminClone.DatumTermina = DatumTermina;
            terminClone.StatusTermina = StatusTermina;
            return terminClone;

        }
        public int Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }
        public Lekar Lekar
        {
            get
            {
                return lekar;
            }
            set
            {
                lekar = value;
                OnPropertyChanged("Lekar");
            }
        }
        public Pacijent Pacijent
        {
            get
            {
                return pacijent;
            }
            set
            {
                pacijent = value;
                OnPropertyChanged("Pacijent");
            }
        }
        public DateTime DatumTermina
        {
            get
            {
                return datumTermina;
            }
            set
            {
                datumTermina = value;
                OnPropertyChanged("DatumTermina");
            }
        }
        public StatusTermina StatusTermina
        {
            get
            {
                return statusTermina;
            }
            set
            {
                statusTermina = value;
                OnPropertyChanged("StatusTermina");
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
            return Sifra + " " + DatumTermina + " " + StatusTermina;
        }
    }
}

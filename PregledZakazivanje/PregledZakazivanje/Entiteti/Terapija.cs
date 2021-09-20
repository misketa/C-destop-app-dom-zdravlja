using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public class Terapija : ICloneable, INotifyPropertyChanged
    {
        int sifra;
        Lekar lekar;
        Pacijent pacijent;
        string opisTerapije;

        public bool obrisano = false;

        public Terapija(int sifra, Lekar lekar, Pacijent pacijent, string opisTerapije)
        {
            this.sifra = sifra;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.opisTerapije = opisTerapije;
        }
        public Terapija()
        {

        }

        public object Clone()
        {
            Terapija terapijaClone = new Terapija();
            terapijaClone.Sifra = Sifra;
            terapijaClone.Lekar = Lekar;
            terapijaClone.Pacijent = Pacijent;
            terapijaClone.OpisTerapije = OpisTerapije;
            return terapijaClone;

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
        public string OpisTerapije
        {
            get
            {
                return opisTerapije;
            }
            set
            {
                opisTerapije = value;
                OnPropertyChanged("OpisTerapije");
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
            return Sifra + " " + OpisTerapije;
        }

    }
}

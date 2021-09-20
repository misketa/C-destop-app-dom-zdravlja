using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public class DomZdravlja : ICloneable, INotifyPropertyChanged
    {
        int sifra;
        string nazivInstitucije;
        Adresa adresa;
        public bool obrisano = false;

        public DomZdravlja(int sifra, string nazivInstitucije, Adresa adresa)
        {
            this.Sifra = sifra;
            this.NazivInstitucije = nazivInstitucije;
            this.Adresa = adresa;
        }

        public DomZdravlja() { }

        public object Clone()
        {
            DomZdravlja domZdravljaClone = new DomZdravlja();
            domZdravljaClone.Sifra = Sifra;
            domZdravljaClone.NazivInstitucije = NazivInstitucije;
            domZdravljaClone.Adresa = (Adresa)Adresa.Clone();
            return domZdravljaClone;

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
        public string NazivInstitucije
        {
            get
            {
                return nazivInstitucije;
            }
            set
            {
                nazivInstitucije = value;
                OnPropertyChanged("NazivInstitucije");
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
            return Sifra + " " + NazivInstitucije + " " + Adresa;
        }
    }
}

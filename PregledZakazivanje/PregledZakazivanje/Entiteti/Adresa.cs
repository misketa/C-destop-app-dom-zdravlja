using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregledZakazivanje.Entiteti
{
    public class Adresa : ICloneable, INotifyPropertyChanged
    {
        int id;
        string ulica;
        string broj;
        string grad;
        string drzava;
        public bool obrisano = false;

        public Adresa(int id, string ulica, string broj, string grad, string drzava)
        {
            this.Id = id;
            this.Ulica = ulica;
            this.Broj = broj;
            this.Grad = grad;
            this.Drzava = drzava;
        }
        public Adresa() { }

        public object Clone()
        {
            Adresa adresaClone = new Adresa();
            adresaClone.Id = Id;
            adresaClone.Ulica = Ulica;
            adresaClone.Broj = Broj;
            adresaClone.Grad = Grad;
            adresaClone.Drzava = Drzava;
            return adresaClone;

        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Ulica
        {
            get
            {
                return ulica;
            }
            set
            {
                ulica = value;
                OnPropertyChanged("Ulica");
            }
        }
        public string Broj
        {
            get
            {
                return broj;
            }
            set
            {
                broj = value;
                OnPropertyChanged("Broj");
            }
        }
        public string Grad
        {
            get
            {
                return grad;
            }
            set
            {
                grad = value;
                OnPropertyChanged("Grad");
            }
        }
        public string Drzava
        {
            get
            {
                return drzava;
            }
            set
            {
                drzava = value;
                OnPropertyChanged("Drzava");
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
            return Id + " " + Ulica + " " + Broj + " " + Grad + " " + Drzava;
        }


    }
}

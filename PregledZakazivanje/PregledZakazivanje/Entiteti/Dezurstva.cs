using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PregledZakazivanje.Entiteti
{
    public class Dezurstva : ICloneable, INotifyPropertyChanged
    {
        Lekar lekar;
        DateTime pocetak;
        DateTime kraj;

        public Dezurstva(Lekar lekar, DateTime pocetak, DateTime kraj)
        {
            this.lekar = lekar;
            this.pocetak = pocetak;
            this.kraj = kraj;
        }

        public Dezurstva()
        {

        }

        public Object Clone()
        {
            Dezurstva dezurstvaClone = new Dezurstva();
            dezurstvaClone.lekar = lekar;
            dezurstvaClone.pocetak = pocetak;
            dezurstvaClone.kraj = kraj;
            return dezurstvaClone;
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

        public DateTime Pocetak
        {
            get
            {
                return pocetak;
            }
            set
            {
                pocetak = value;
                OnPropertyChanged("Pocetak");
            }
        }

        public DateTime Kraj
        {
            get
            {
                return kraj;
            }
            set
            {
                kraj = value;
                OnPropertyChanged("Kraj");
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
            return Pocetak + " " + Kraj + " ";
        }
    }
}

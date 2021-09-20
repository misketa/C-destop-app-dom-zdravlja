using PregledZakazivanje.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PregledZakazivanje.Forme
{
    /// <summary>
    /// Interaction logic for FrmDezurstva.xaml
    /// </summary>
    public partial class FrmDezurstva : Window
    {
        ICollectionView viewDezurstva;
        Lekar selektovanLekar = null;
        public FrmDezurstva(Lekar lekar)
        {
            InitializeComponent();
            Sistem.citajIzBaze();
            this.selektovanLekar = lekar;
            
            inicijalizujDataGrid();
        }

        private void inicijalizujDataGrid()
        {
            viewDezurstva = CollectionViewSource.GetDefaultView(Sistem.listaDezurstava.ToList());
            dgDezurstva.ItemsSource = viewDezurstva;
            dgDezurstva.IsSynchronizedWithCurrentItem = true;
            dgDezurstva.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        void refreshDezurstva_DataGrid()
        {
            viewDezurstva = CollectionViewSource.GetDefaultView(Sistem.listaDezurstava.ToList());
            dgDezurstva.ItemsSource = viewDezurstva;
            dgDezurstva.IsSynchronizedWithCurrentItem = true;
            dgDezurstva.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}

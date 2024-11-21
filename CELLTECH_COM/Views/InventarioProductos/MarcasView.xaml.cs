using System.Windows;
using System.Windows.Controls;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para MarcasView.xaml
    /// </summary>
    public partial class MarcasView : UserControl
    {
        public MarcasView()
        {
            InitializeComponent();
        }

        private void btnNuevaMarca_Click(object sender, RoutedEventArgs e)
        {
            var ventanaNuevaMarca = new NuevaMarcaWindow();
            ventanaNuevaMarca.ShowDialog();
        }
    }
}
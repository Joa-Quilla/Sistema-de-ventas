using CELLTECH_COM.ViewModels.InventarioProductos;
using System.Windows;
using System.Windows.Controls;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para ProductosView.xaml
    /// </summary>
    public partial class ProductosView : Page
    {
        public ProductosView()
        {
            InitializeComponent();
            this.DataContext = new ProductoViewModel();
        }
        private void btnNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            var ventanaNuevoProducto = new NuevoProductoWindow();
            ventanaNuevoProducto.Owner = Window.GetWindow(this); // Establece la ventana principal como propietaria
            ventanaNuevoProducto.ShowDialog();
        }
    }
}

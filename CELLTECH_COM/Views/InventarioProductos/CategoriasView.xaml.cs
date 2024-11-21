using CELLTECH_COM.Models.InventarioProductos;
using System.Windows;
using System.Windows.Controls;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para CategoriasView.xaml
    /// </summary>
    public partial class CategoriasView : Page
    {
        private readonly CategoriaViewModel _viewModel;

        public CategoriasView()
        {
            InitializeComponent();
            _viewModel = new CategoriaViewModel();
            this.DataContext = _viewModel;
        }

        private void btnNuevaCategoria_Click(object sender, RoutedEventArgs e)
        {
            var ventanaNuevaCategoria = new NuevaCategoriaWindow();
            ventanaNuevaCategoria.Owner = Window.GetWindow(this);
            ventanaNuevaCategoria.ShowDialog();
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Categoria categoria)
            {
                MessageBox.Show($"Detalles de la categoría: {categoria.Nombre}",
                              "Detalles",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Categoria categoria)
            {
                MessageBox.Show($"Editando categoría: {categoria.Nombre}",
                              "Editar",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Categoria categoria)
            {
                if (MessageBox.Show($"¿Está seguro que desea eliminar la categoría: {categoria.Nombre}?",
                                  "Confirmar eliminación",
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Aquí irá la lógica para eliminar
                    MessageBox.Show("Categoría eliminada exitosamente",
                                  "Éxito",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
            }
        }
    }

    internal class CategoriaViewModel
    {
    }
}

using System.Windows;
using System.Windows.Input;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para NuevaCategoriaWindow.xaml
    /// </summary>
    public partial class NuevaCategoriaWindow : Window
    {
        public NuevaCategoriaWindow()
        {
            InitializeComponent();
            txtCodigo.Focus();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar? Se perderán los datos no guardados.",
                              "Confirmar",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar? Se perderán los datos no guardados.",
                              "Confirmar",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                // Aquí irá la lógica para guardar la categoría en la base de datos
                MessageBox.Show("Categoría guardada exitosamente\n\nPuede continuar agregando más categorías.",
                              "Éxito",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);

                LimpiarFormulario();
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNombre.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

            // Poner el foco en el primer campo
            txtCodigo.Focus();
        }
    }
}


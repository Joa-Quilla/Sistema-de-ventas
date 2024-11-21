using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para NuevoProductoWindow.xaml
    /// </summary>
    public partial class NuevoProductoWindow : Window
    {
        public NuevoProductoWindow()
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
                // Aquí irá la lógica para guardar el producto en la base de datos
                MessageBox.Show("Producto guardado exitosamente\n\nPuede continuar agregando más productos.",
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

            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una marca.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbMarca.Focus();
                return false;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbCategoria.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El precio es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("El stock es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStock.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            cmbMarca.SelectedIndex = -1;
            txtModelo.Text = string.Empty;
            cmbCategoria.SelectedIndex = -1;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtImagen.Text = string.Empty;

            // Poner el foco en el primer campo
            txtCodigo.Focus();
        }

        private void btnExaminar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif|Todos los archivos|*.*",
                Title = "Seleccionar imagen del producto"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtImagen.Text = openFileDialog.FileName;
            }
        }

        private void txtPrecio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo permite números y un punto decimal
            Regex regex = new Regex(@"^[0-9]+(\.[0-9]*)?$");
            string newText = txtPrecio.Text + e.Text;
            e.Handled = !regex.IsMatch(newText);
        }

        private void txtStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo permite números enteros
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}

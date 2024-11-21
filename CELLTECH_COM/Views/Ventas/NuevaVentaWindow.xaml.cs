using CELLTECH_COM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CELLTECH_COM.Views.Reports
{
    /// <summary>
    /// Lógica de interacción para NuevaVentaWindow.xaml
    /// </summary>
    public partial class NuevaVentaWindow : Window
    {
        private ObservableCollection<Product> _productos;
        private ObservableCollection<SaleItem> _productosVenta;
        private decimal _total = 0;

        // Propiedades públicas para acceder desde MainWindow
        public Customer Customer { get; private set; }
        public decimal Total => _total;
        public string PaymentMethod => (cmbMetodoPago.SelectedItem as ComboBoxItem)?.Content is StackPanel panel
            ? panel.Children.OfType<TextBlock>().LastOrDefault()?.Text ?? "No especificado"
            : "No especificado";

        public NuevaVentaWindow()
        {
            InitializeComponent();
            CargarDatosIniciales();
            Owner = Application.Current.MainWindow;
        }

        private void CargarDatosIniciales()
        {
            _productos = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Laptop HP", Price = 5999.99m, Stock = 10 },
                new Product { Id = 2, Name = "Mouse Gamer", Price = 299.50m, Stock = 15 },
                new Product { Id = 3, Name = "Teclado Mecánico", Price = 599.99m, Stock = 20 },
                new Product { Id = 4, Name = "Monitor 24\"", Price = 1999.99m, Stock = 8 },
                new Product { Id = 5, Name = "Audífonos Bluetooth", Price = 449.99m, Stock = 12 }
            };

            _productosVenta = new ObservableCollection<SaleItem>();

            lstProductos.ItemsSource = _productos;
            dgProductosVenta.ItemsSource = _productosVenta;
        }

        private void TxtBuscarProducto_TextChanged(object _, TextChangedEventArgs __)
        {
            string filtro = txtBuscarProducto.Text.ToLower();
            var productosFiltrados = _productos.Where(p =>
                p.Name.ToLower().Contains(filtro)).ToList();
            lstProductos.ItemsSource = productosFiltrados;
        }

        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs _)
        {
            var producto = ((FrameworkElement)sender).DataContext as Product;


            // Verifica que producto no sea null
            if (producto == null)
            {
                CustomMessageBox.ShowWarning("El producto no está disponible.");
                return;
            }

            // Verifica que _productosVenta no sea null
            if (_productosVenta == null)
            {
                _productosVenta = new ObservableCollection<SaleItem>();
            }

            // Verifica si el producto ya está en la venta
            var itemExistente = _productosVenta.FirstOrDefault(i => i.Product?.Id == producto.Id);

            if (itemExistente != null)
            {
                // Verifica que itemExistente y su propiedad Product no sean null
                if (itemExistente.Quantity < producto.Stock)
                {
                    itemExistente.Quantity++;
                    dgProductosVenta.Items.Refresh();
                }
                else
                {
                    CustomMessageBox.ShowWarning("No hay suficiente stock disponible.");
                }
            }
            else
            {
                // Asegúrate de que el producto no sea null al agregarlo
                _productosVenta.Add(new SaleItem
                {
                    Product = producto,
                    Quantity = 1
                });
            }

            ActualizarTotal();
            ActualizarTotal();
        }

        private void BtnAumentarCantidad_Click(object sender, RoutedEventArgs _)
        {
            var item = ((FrameworkElement)sender).DataContext as SaleItem;

            // Verifica que item no sea null
            if (item == null || item.Product == null)
            {
                CustomMessageBox.ShowWarning("El artículo no está disponible.");
                return;
            }

            if (item.Quantity < item.Product.Stock)
            {
                item.Quantity++;
                dgProductosVenta.Items.Refresh();
                ActualizarTotal();
            }
            else
            {
                CustomMessageBox.ShowWarning("No hay suficiente stock disponible.");
            }
        }

        private void BtnDisminuirCantidad_Click(object sender, RoutedEventArgs _)
        {
            var item = ((FrameworkElement)sender).DataContext as SaleItem;

            // Verifica que item no sea null
            if (item == null)
            {
                CustomMessageBox.ShowWarning("El artículo no está disponible.");
                return;
            }

            if (item.Quantity > 1)
            {
                item.Quantity--;
                dgProductosVenta.Items.Refresh();
                ActualizarTotal();
            }
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs _)
        {
            var item = ((FrameworkElement)sender).DataContext as SaleItem;

            // Verifica que item no sea null
            if (item == null)
            {
                CustomMessageBox.ShowWarning("El artículo no está disponible.");
                return;
            }

            _productosVenta.Remove(item);
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            _total = _productosVenta.Sum(item => item.Subtotal);
            txtTotal.Text = $"Q {_total:N2}";
        }

        private void CmbMetodoPago_SelectionChanged(object _, SelectionChangedEventArgs __)
        {
            if (cmbMetodoPago.SelectedItem is ComboBoxItem selectedItem)
            {
                var content = (selectedItem.Content as StackPanel)?.Children
                    .OfType<TextBlock>()
                    .LastOrDefault()?.Text ?? string.Empty;

                var isEfectivo = content == "Efectivo";
                pnlEfectivo.Visibility = isEfectivo ? Visibility.Visible : Visibility.Collapsed;

                if (!isEfectivo)
                {
                    txtEfectivoRecibido.Text = string.Empty;
                    txtCambio.Text = "Q 0.00";
                }
            }
        }

        private void TxtEfectivoRecibido_TextChanged(object _, TextChangedEventArgs __)
        {
            if (decimal.TryParse(txtEfectivoRecibido.Text, out var efectivoRecibido))
            {
                var cambio = efectivoRecibido - _total;
                txtCambio.Text = $"Q {Math.Max(0, cambio):N2}";
            }
            else
            {
                txtCambio.Text = "Q 0.00";
            }
        }

        private void BtnCompletarVenta_Click(object _, RoutedEventArgs __)
        {
            if (!_productosVenta.Any())
            {
                CustomMessageBox.ShowWarning("Agregue al menos un producto a la venta.");
                return;
            }

            if (cmbMetodoPago.SelectedItem == null)
            {
                CustomMessageBox.ShowWarning("Seleccione un método de pago.");
                return;
            }

            if (PaymentMethod == "Efectivo")
            {
                if (!decimal.TryParse(txtEfectivoRecibido.Text, out var efectivoRecibido)
                    || efectivoRecibido < _total)
                {
                    CustomMessageBox.ShowWarning("El efectivo recibido debe ser mayor o igual al total.");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtClienteNombre.Text))
            {
                Customer = new Customer
                {
                    Name = txtClienteNombre.Text,
                    Phone = txtClienteTelefono.Text,
                    Address = txtClienteDireccion.Text
                };
            }

            GuardarVenta(new Sale
            {
                Date = DateTime.Now,
                Customer = Customer?.Name ?? "Cliente General",
                Total = _total,
                Status = "Completada",
                PaymentMethod = PaymentMethod
            });

            DialogResult = true;
            Close();
        }

        // En NuevaVentaWindow.xaml.cs
        private void GuardarVenta(Sale venta)
        {
            try
            {
                // Aquí irá la lógica para guardar en la base de datos
                foreach (var item in _productosVenta)
                {
                    item.Product.Stock -= item.Quantity;
                }

                var mensaje = new CustomMessageBox
                {
                    Title = "Éxito",
                    Message = $"Venta #{venta.Id} completada exitosamente",  // Usando el parámetro venta
                    Icon = "✅",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#198754")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
            }
            catch (Exception ex)
            {
                var mensaje = new CustomMessageBox
                {
                    Title = "Error",
                    Message = $"Error al guardar la venta: {ex.Message}",
                    Icon = "⚠️",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc3545")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
                throw;
            }
        }

        private void BtnCancelar_Click(object _, RoutedEventArgs __)
        {
            if (_productosVenta.Any())
            {
                var result = CustomMessageBox.ShowConfirmation(
                    "¿Está seguro de cancelar la venta? Se perderán todos los datos ingresados.");

                if (!result) return;
            }

            DialogResult = false;
            Close();
        }

        private void BtnCerrar_Click(object _, RoutedEventArgs __)
        {
            BtnCancelar_Click(_, __);
           
        }
    }
}

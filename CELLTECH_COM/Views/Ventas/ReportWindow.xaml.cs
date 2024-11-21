using CELLTECH_COM.Models;
using CELLTECH_COM.Views.Clientes;
using CELLTECH_COM.Views.Reportes;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace CELLTECH_COM.Views.Reports
{

    public partial class ReportWindow : Window
    {
        private bool isMaximized = true;
        private readonly TextBlock? maximizeIcon;
        private ObservableCollection<Sale> _ventas;
        private decimal _ventasHoy;
        private decimal _ventasTotal;

        public ReportWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;

            // Configurar dimensiones
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.MaxWidth = screenWidth;
            this.MaxHeight = screenHeight;
            this.Width = screenWidth;
            this.Height = screenHeight;

            // Inicializar el ícono de maximizar
            maximizeIcon = FindName("MaximizeIcon") as TextBlock;
            if (maximizeIcon != null)
            {
                maximizeIcon.Text = "❐";
            }

            _ventas = new ObservableCollection<Sale>();

            // Inicializar fechas
            dpFechaInicio.SelectedDate = DateTime.Today;
            dpFechaFin.SelectedDate = DateTime.Today;

            // Cargar datos iniciales
            CargarDatos();
        }

        private void CargarDatos()
        {
            // Aquí irá la lógica para cargar datos desde la base de datos
            _ventasHoy = 0;
            _ventasTotal = 0;
            ActualizarEstadisticas();
        }

        private void ActualizarEstadisticas()
        {
            txtVentasHoy.Text = $"Q {_ventasHoy:N2}";
            txtVentasTotal.Text = $"Q {_ventasTotal:N2}";
        }

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            var ventanaNuevaVenta = new NuevaVentaWindow();
            if (ventanaNuevaVenta.ShowDialog() == true)
            {
                CargarDatos(); // Recargar datos después de una nueva venta
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fechaInicio = dpFechaInicio.SelectedDate ?? DateTime.Today;
                var fechaFin = dpFechaFin.SelectedDate ?? DateTime.Today;
                var cliente = txtBuscarCliente.Text;

                // Aquí irá la lógica de búsqueda
                // Por ahora solo mostramos un mensaje
                var mensaje = new CustomMessageBox
                {
                    Title = "Búsqueda",
                    Message = $"Buscando ventas:\nFecha Inicio: {fechaInicio:dd/MM/yyyy}\n" +
                             $"Fecha Fin: {fechaFin:dd/MM/yyyy}\n" +
                             $"Cliente: {(string.IsNullOrEmpty(cliente) ? "Todos" : cliente)}",
                    Icon = "🔍",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0d6efd")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
            }
            catch (Exception ex)
            {
                var mensaje = new CustomMessageBox
                {
                    Title = "Error",
                    Message = $"Error al buscar ventas: {ex.Message}",
                    Icon = "⚠️",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc3545")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
            }
        }


        private void BtnVerVenta_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Sale venta)
            {
                var mensaje = new CustomMessageBox
                {
                    Title = "Detalles de Venta",
                    Message = $"ID: {venta.Id}\n" +
                             $"Cliente: {venta.Customer}\n" +
                             $"Fecha: {venta.Date:dd/MM/yyyy HH:mm}\n" +
                             $"Total: Q{venta.Total:N2}\n" +
                             $"Estado: {venta.Status}\n" +
                             $"Método de Pago: {venta.PaymentMethod}",
                    Icon = "📋",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0d6efd")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
            }
        }

        private void BtnEditarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Sale)
            {
                var mensaje = new CustomMessageBox
                {
                    Title = "Editar Venta",
                    Message = "Función de edición en desarrollo",
                    Icon = "✏",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffc107")),
                    ButtonText = "Aceptar"
                };
                mensaje.ShowDialog();
            }
        }

        private void BtnEliminarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Sale venta)
            {
                var mensajeConfirmacion = new CustomMessageBox
                {
                    Title = "Confirmar Eliminación",
                    Message = $"¿Está seguro de eliminar la venta #{venta.Id}?",
                    Icon = "⚠️",
                    IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc3545")),
                    ButtonText = "Eliminar",
                    ShowCancelButton = true
                };

                if (mensajeConfirmacion.ShowDialog() == true)
                {
                    try
                    {
                        _ventas.Remove(venta);
                        CargarDatos(); // Recargar datos después de eliminar

                        var mensajeExito = new CustomMessageBox
                        {
                            Title = "Éxito",
                            Message = "Venta eliminada correctamente",
                            Icon = "✅",
                            IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#198754")),
                            ButtonText = "Aceptar"
                        };
                        mensajeExito.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        var mensajeError = new CustomMessageBox
                        {
                            Title = "Error",
                            Message = $"Error al eliminar la venta: {ex.Message}",
                            Icon = "⚠️",
                            IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc3545")),
                            ButtonText = "Aceptar"
                        };
                        mensajeError.ShowDialog();
                    }
                }
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMaximized)
            {
                // Restaurar
                WindowState = WindowState.Normal;
                Width = 1080;
                Height = 650;
                isMaximized = false;

                if (maximizeIcon != null)
                {
                    maximizeIcon.Text = "☐";
                }
            }
            else
            {
                // Maximizar
                WindowState = WindowState.Maximized;
                isMaximized = true;

                if (maximizeIcon != null)
                {
                    maximizeIcon.Text = "❐";
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #region Funcionalidad de arrastre y redimensionamiento
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;


#pragma warning disable SYSLIB1054
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
#pragma warning restore SYSLIB1054

        private void Window_SourceInitialized(object? sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(handle)?.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCHITTEST && WindowState != WindowState.Maximized)
            {
                int x = (int)((uint)lParam & 0xFFFF);
                int y = (int)((uint)lParam >> 16);
                Point point = PointFromScreen(new Point(x, y));

                double width = ActualWidth;
                double height = ActualHeight;
                const int borderWidth = 5;

                if (point.X <= borderWidth)
                {
                    if (point.Y <= borderWidth)
                    {
                        handled = true;
                        return (IntPtr)HTTOPLEFT;
                    }
                    if (point.Y >= height - borderWidth)
                    {
                        handled = true;
                        return (IntPtr)HTBOTTOMLEFT;
                    }
                    handled = true;
                    return (IntPtr)HTLEFT;
                }
                if (point.X >= width - borderWidth)
                {
                    if (point.Y <= borderWidth)
                    {
                        handled = true;
                        return (IntPtr)HTTOPRIGHT;
                    }
                    if (point.Y >= height - borderWidth)
                    {
                        handled = true;
                        return (IntPtr)HTBOTTOMRIGHT;
                    }
                    handled = true;
                    return (IntPtr)HTRIGHT;
                }
                if (point.Y <= borderWidth)
                {
                    handled = true;
                    return (IntPtr)HTTOP;
                }
                if (point.Y >= height - borderWidth)
                {
                    handled = true;
                    return (IntPtr)HTBOTTOM;
                }
            }
            return IntPtr.Zero;
        }
        #endregion


        private void BtnMenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow ReportWindow = new();
            ReportWindow.Show();    
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           ClientesWindow ReportWindow = new();
           ReportWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowReportes ReportWindow = new();
            ReportWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ventanaInventario = new Views.InventarioProductos.InventarioWindow();
            ventanaInventario.Show();
            this.Close();

        }

    }
}

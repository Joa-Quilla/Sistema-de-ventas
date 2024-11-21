using CELLTECH_COM.ViewModels.InventarioProductos;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;


namespace CELLTECH_COM.Views.InventarioProductos
{
    using CELLTECH_COM.Views.Clientes;
    using CELLTECH_COM.Views.Reportes;
    using CELLTECH_COM.Views.Reports;
    using System.Runtime.InteropServices;
    public partial class InventarioWindow : Window
    {
        private bool isMaximized = true;
        private readonly TextBlock? maximizeIcon;

        public InventarioWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ProductosView());

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
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductosView());
        }

        private void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoriasView());
        }

        private void BtnMarcas_Click(object sender, RoutedEventArgs e)
        {
            var marcasView = new MarcasView();
            marcasView.DataContext = new MarcaViewModel();
            MainFrame.Navigate(marcasView);
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



        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);


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

        #region Botones de control de ventana
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion


        #region Botones de Menu
        private void ButtonVentas_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow InventarioWindow = new();
            InventarioWindow.Show();
            this.Close();
        }

        private void BtnMenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow InventarioWindow = new();
            InventarioWindow.Show();
            this.Close();
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow InventarioWindow = new();
            InventarioWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowReportes InventarioWindow = new();
            InventarioWindow.Show();
            this.Close();

        }
        #endregion

    }
}

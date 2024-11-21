using CELLTECH_COM.Views;
using CELLTECH_COM.Views.Clientes;
using CELLTECH_COM.Views.Reportes;
using CELLTECH_COM.Views.Reports;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;



namespace CELLTECH_COM
{

    public partial class MainWindow : Window
    {
        private bool isMaximized = false;
        private readonly TextBlock? maximizeIcon;
        private const WindowState maximized = WindowState.Maximized;

        public MainWindow()
        {
            InitializeComponent();


            // Iniciar maximizado
            WindowState = maximized;

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

        private void CenterWindow()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double left = (screenWidth - this.Width) / 2;
            double top = (screenHeight - this.Height) / 2;

            this.Left = left;
            this.Top = top;
        }

        #region Botones de control de ventana
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
                Width = 1080; // Restaurar a 1080
                Height = 650; // Restaurar a 720
                CenterWindow();
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
            Close();
        }
        #endregion

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



        private string nombreUsuario;

        public MainWindow(string usuario)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            MostrarMensajeBienvenida();


        }

        private void MostrarMensajeBienvenida()
        {
            var mensajeBienvenida = new Window
            {
                Title = "Bienvenida",
                Width = 400,
                Height = 200,
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = System.Windows.Media.Brushes.Transparent,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true
            };

            var border = new Border
            {
                Background = System.Windows.Media.Brushes.White,
                CornerRadius = new CornerRadius(15),
                BorderBrush = System.Windows.Media.Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    BlurRadius = 10,
                    ShadowDepth = 1,
                    Direction = 315,
                    Color = System.Windows.Media.Color.FromArgb(34, 0, 0, 0),
                },
                Padding = new Thickness(20)
            };

            var stackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var textoBienvenida = new TextBlock
            {
                Text = "¡Bienvenido!",
                FontSize = 32,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
                Foreground = System.Windows.Media.Brushes.DodgerBlue
            };

            var textoNombre = new TextBlock
            {
                Text = nombreUsuario,
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = System.Windows.Media.Brushes.Gray
            };

            stackPanel.Children.Add(textoBienvenida);
            stackPanel.Children.Add(textoNombre);
            border.Child = stackPanel;
            mensajeBienvenida.Content = border;

            mensajeBienvenida.Show();

            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(3))
            };

            fadeOut.Completed += (s, e) => mensajeBienvenida.Close();

            mensajeBienvenida.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void Button_Click_Ventas(object sender, RoutedEventArgs e)
        {
           ReportWindow MainWindow = new();
            MainWindow.Show();
            this.Close();
        }

        private void Button_Click_Inventario(object sender, RoutedEventArgs e)
        {
            var MainWindow = new Views.InventarioProductos.InventarioWindow();
            MainWindow.Show();
            this.Close();

        }

        private void ButtonClientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow MaineWindo = new();
            MaineWindo.Show();
            this.Close();
        }

        private void ButtonReportes_Click(object sender, RoutedEventArgs e)
        {
           WindowReportes MainWindow = new();
           MainWindow.Show();
            this.Close();
        }
    }

}
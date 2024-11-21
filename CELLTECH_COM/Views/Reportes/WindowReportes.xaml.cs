using CELLTECH_COM.Views.Clientes;
using CELLTECH_COM.Views.Reports;
using System.Windows;
using System.Windows.Controls;

namespace CELLTECH_COM.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para WindowReportes.xaml
    /// </summary>
    public partial class WindowReportes : Window
    {

        private bool isMaximized = true;
        private readonly TextBlock? maximizeIcon;

        private ReportesView reportesView;
        private UserControl currentView;

        public WindowReportes()
        {
            InitializeComponent();
            reportesView = new ReportesView();
            ShowReportesView();

            // Inicializar el ícono de maximizar
            maximizeIcon = FindName("MaximizeIcon") as TextBlock;
            if (maximizeIcon != null)
            {
                maximizeIcon.Text = "❐";
            }
        }

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

        private void ButtonVentas_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow WindowReport = new();
            WindowReport.Show();
            this.Close();

        }

        private void ButtonInvetario_Click(object sender, RoutedEventArgs e)
        {
            var ventanaInventario = new Views.InventarioProductos.InventarioWindow();
            ventanaInventario.Show();
            this.Close();
        }

        private void ButtonClientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow WindowRerportes = new();
            WindowRerportes.Show();
            this.Close();
        }

        private void BtnMenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow WindowReportes = new();
            WindowReportes.Show();
            this.Close();
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            ShowReportesView();
        }

        private void ShowReportesView()
        {
            if (currentView != reportesView)
            {
                MainContent.Content = reportesView;
                currentView = reportesView;
            }
        }
        //------------------------------------------------------------------------------

    }
}


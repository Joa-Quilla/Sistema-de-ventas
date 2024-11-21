using CELLTECH_COM.Views.Reportes;
using CELLTECH_COM.Views.Reports;
using System.Windows;
using System.Windows.Controls;

namespace CELLTECH_COM.Views.Clientes
{
    /// <summary>
    /// Lógica de interacción para ClientesWindow.xaml
    /// </summary>
    public partial class ClientesWindow : Window
    {

        private bool isMaximized = true;
        private readonly TextBlock? maximizeIcon;

        public ClientesWindow()
        {
            InitializeComponent();

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



        private void BtnMenuClick(object sender, RoutedEventArgs e)
        {
            MainWindow ClientesWindow = new();
            ClientesWindow.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ventanaInventario = new Views.InventarioProductos.InventarioWindow();
            ventanaInventario.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowReportes ClinetesWindow = new();
            ClinetesWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReportWindow ClienteWindow = new();
            ClienteWindow.Show();
            this.Close();

        }

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            NuevoCliente ClienteWindow = new();
            ClienteWindow.Show();
            this.Close();
        }





        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditarVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminarVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVerVenta_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}

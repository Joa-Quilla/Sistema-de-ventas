using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CELLTECH_COM.Views.InventarioProductos
{
    /// <summary>
    /// Lógica de interacción para NuevaMarcaWindow.xaml
    /// </summary>
    public partial class NuevaMarcaWindow : Window
    {
        public NuevaMarcaWindow()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Aquí irá la lógica para guardar cuando conectes la BD
            this.DialogResult = true;
            this.Close();
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
    }
}

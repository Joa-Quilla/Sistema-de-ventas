using System.Windows;
using System.Windows.Input;

namespace CELLTECH_COM.Views.Clientes
{
    /// <summary>
    /// Lógica de interacción para NuevoCliente.xaml
    /// </summary>
    public partial class NuevoCliente : Window
    {
        public NuevoCliente()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
           ClientesWindow NuevoCleinte=new ();
            NuevoCleinte.Show();
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

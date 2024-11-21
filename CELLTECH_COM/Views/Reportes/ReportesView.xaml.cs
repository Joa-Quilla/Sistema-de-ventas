using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CELLTECH_COM.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReportesView.xaml
    /// </summary>
    public partial class ReportesView : UserControl
    {
        public ReportesView()
        {
            InitializeComponent();
           
        }


        private void MontoInicialTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+(\.[0-9]{0,2})?$");
            string newText = ((TextBox)sender).Text + e.Text;
            e.Handled = !regex.IsMatch(newText);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}

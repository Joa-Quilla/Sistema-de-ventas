
using System.Windows;
using System.Windows.Media;


namespace CELLTECH_COM.Views.Reports
{
    /// <summary>
    /// Lógica de interacción para CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public new string Title { get; set; }
        public string Message { get; set; }
        public new string Icon { get; set; }
        public Brush IconBrush { get; set; }
        public string ButtonText { get; set; }
        public bool ShowCancelButton { get; set; }

        public CustomMessageBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public static bool ShowConfirmation(string message)
        {
            var messageBox = new CustomMessageBox
            {
                Title = "Confirmar",
                Message = message,
                Icon = "⚠️",
                IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffc107")),
                ButtonText = "Aceptar",
                ShowCancelButton = true
            };

            return messageBox.ShowDialog() == true;
        }

        public static void ShowWarning(string message)
        {
            var messageBox = new CustomMessageBox
            {
                Title = "Advertencia",
                Message = message,
                Icon = "⚠️",
                IconBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffc107")),
                ButtonText = "Aceptar"
            };

            messageBox.ShowDialog();
        }
    }
}
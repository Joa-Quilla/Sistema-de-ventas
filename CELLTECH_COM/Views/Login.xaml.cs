using System.Windows;
using System.Windows.Input;
using CELLTECH_COM.ViewModels;

namespace CELLTECH_COM.Views
{
    public partial class Login : Window
    {
        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }

        private async void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Datos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            await _viewModel.LoginAsync(username, password);
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
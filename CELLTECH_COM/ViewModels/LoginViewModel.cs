using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using CELLTECH_COM.Views;
using CELLTECH_COM.Data;
using CELLTECH_COM.Models;

namespace CELLTECH_COM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string? _username;
        private string? _errorMessage;
        private bool _isLoading;

        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public async Task LoginAsync(string username, string password)
        {
            try
            {
                IsLoading = true;
                ErrorMessage = "✓ Iniciando sesión...";

                (bool success, string message, Usuario? user) = await DBConnection.ValidateLoginAsync(username, password);

                if (success && user != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow(user.NombreCompleto);
                        mainWindow.Show();

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window is Views.Login)
                            {
                                window.Close();
                                break;
                            }
                        }
                    });
                }
                else
                {
                    ErrorMessage = "✗ Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "✗ Error al intentar iniciar sesión: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
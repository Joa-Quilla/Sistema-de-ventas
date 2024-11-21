using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CELLTECH_COM.Views.Dialogs
{
    /// <summary>
    /// Lógica de interacción para CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {

        private bool _resultado;
        public CustomMessageBox()
        {
                InitializeComponent();
        }

            private void BtnAceptar_Click(object sender, RoutedEventArgs e)
            {
                _resultado = true;
                this.Close();
            }

            private void BtnCancelar_Click(object sender, RoutedEventArgs e)
            {
                _resultado = false;
                this.Close();
            }

            public static void ShowInformation(string mensaje, string titulo = "Información")
            {
                CustomMessageBox msgBox = new CustomMessageBox();
                msgBox.TxtTitulo.Text = titulo;
                msgBox.TxtMensaje.Text = mensaje;
                msgBox.ShowDialog();
            }

            public static bool ShowQuestion(string mensaje, string titulo = "Confirmar")
            {
                CustomMessageBox msgBox = new CustomMessageBox();
                msgBox.TxtTitulo.Text = titulo;
                msgBox.TxtMensaje.Text = mensaje;
                msgBox.BtnCancelar.Visibility = Visibility.Visible;
                msgBox.ShowDialog();
                return msgBox._resultado;
            }

            public static void ShowError(string mensaje, string titulo = "Error")
            {
                CustomMessageBox msgBox = new CustomMessageBox();
                msgBox.TxtTitulo.Text = titulo;
                msgBox.TxtMensaje.Text = mensaje;
                msgBox.ShowDialog();
            }
        }
    }

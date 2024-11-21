using System.Windows.Input;

namespace CELLTECH_COM.Helpers.Commands
{
    public class RelayCommandBase
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
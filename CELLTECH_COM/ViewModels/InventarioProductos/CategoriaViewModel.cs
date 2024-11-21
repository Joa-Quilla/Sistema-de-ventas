using CELLTECH_COM.Models.InventarioProductos;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CELLTECH_COM.ViewModels.InventarioProductos
{
    public class CategoriaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Categoria> _categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get { return _categorias; }
            set
            {
                _categorias = value;
                OnPropertyChanged("Categorias");
            }
        }

        public CategoriaViewModel()
        {
            Categorias = new ObservableCollection<Categoria>();
            CargarCategoriasPrueba();
        }

        private void CargarCategoriasPrueba()
        {
            // Datos de ejemplo
            Categorias.Add(new Categoria
            {
                Id = 1,
                Nombre = "Smartphones",
                Descripcion = "Teléfonos inteligentes de última generación",
                CantidadProductos = 15,
                Estado = true
            });

            Categorias.Add(new Categoria
            {
                Id = 2,
                Nombre = "Tablets",
                Descripcion = "Tablets y iPads de diferentes marcas",
                CantidadProductos = 8,
                Estado = true
            });

            Categorias.Add(new Categoria
            {
                Id = 3,
                Nombre = "Accesorios",
                Descripcion = "Accesorios para dispositivos móviles",
                CantidadProductos = 45,
                Estado = true
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using CELLTECH_COM.Models.InventarioProductos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CELLTECH_COM.ViewModels.InventarioProductos
{
    public class MarcaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Marca> _marcas;
        public ObservableCollection<Marca> Marcas
        {
            get { return _marcas; }
            set
            {
                _marcas = value;
                OnPropertyChanged("Marcas");
            }
        }

        public MarcaViewModel()
        {
            Marcas = new ObservableCollection<Marca>();
            CargarMarcasPrueba();
        }

        private void CargarMarcasPrueba()
        {
            // Datos de ejemplo
            Marcas.Add(new Marca
            {
                Id = 1,
                Nombre = "Samsung",
                Descripcion = "Fabricante líder de dispositivos electrónicos",
                Estado = true
            });

            Marcas.Add(new Marca
            {
                Id = 2,
                Nombre = "Apple",
                Descripcion = "Fabricante de iPhone, iPad y otros dispositivos premium",
                Estado = true
            });

            Marcas.Add(new Marca
            {
                Id = 3,
                Nombre = "Xiaomi",
                Descripcion = "Fabricante de dispositivos móviles y electrónicos",
                Estado = true
            });

            Marcas.Add(new Marca
            {
                Id = 4,
                Nombre = "Huawei",
                Descripcion = "Empresa de tecnología y telecomunicaciones",
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

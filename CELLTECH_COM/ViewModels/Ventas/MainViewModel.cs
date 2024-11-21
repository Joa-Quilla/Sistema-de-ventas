using CELLTECH_COM.Models;
using CELLTECH_COM.ViewModels.Base;
using System.Collections.ObjectModel;
using System;

namespace CELLTECH_COM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Sale> _sales;

        public ObservableCollection<Sale> Sales
        {
            get => _sales;
            set => SetProperty(ref _sales, value);
        }

        public MainViewModel()
        {
            // Datos de ejemplo
            Sales = new ObservableCollection<Sale>
            {
                new Sale
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Customer = "Cliente 1",
                    Total = 1500.00m,
                    PaymentMethod = "Efectivo"
                },
                new Sale
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(-1),
                    Customer = "Cliente 2",
                    Total = 2500.00m,
                    PaymentMethod = "Tarjeta"
                }
            };
        }
    }
}

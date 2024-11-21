using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CELLTECH_COM.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }

        public SolidColorBrush StatusColor
        {
            get
            {
                if (Status == "Completed")
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#198754"));
                else if (Status == "Pending")
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffc107"));
                else if (Status == "Cancelled")
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc3545"));
                else
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6c757d"));
            }
        }

        public Sale()
        {
            Date = DateTime.Now;
        }
    }
}
namespace CELLTECH_COM.Models.InventarioProductos
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int? CategoriaID { get; set; }
        public int? MarcaID { get; set; }
        public string? Modelo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public string NombreMarca { get; set; } = string.Empty;
    }
}
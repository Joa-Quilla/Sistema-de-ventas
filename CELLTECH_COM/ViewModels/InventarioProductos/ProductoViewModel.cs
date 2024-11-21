using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CELLTECH_COM.Models.InventarioProductos;
using CELLTECH_COM.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using CELLTECH_COM.ViewModels.Base;

namespace CELLTECH_COM.ViewModels.InventarioProductos
{
    public class ProductoViewModel : ViewModelBase
    {
        private ObservableCollection<Producto> _productos;
        private ObservableCollection<Categoria> _categorias;
        private ObservableCollection<Marca> _marcas;
        private Producto _productoSeleccionado;
        private string _busqueda = string.Empty;
        private bool _isLoading;

        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set => SetProperty(ref _productos, value);
        }

        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set => SetProperty(ref _categorias, value);
        }

        public ObservableCollection<Marca> Marcas
        {
            get => _marcas;
            set => SetProperty(ref _marcas, value);
        }

        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set => SetProperty(ref _productoSeleccionado, value);
        }

        public string Busqueda
        {
            get => _busqueda;
            set
            {
                SetProperty(ref _busqueda, value);
                FiltrarProductos();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ProductoViewModel()
        {
            Productos = new ObservableCollection<Producto>();
            Categorias = new ObservableCollection<Categoria>();
            Marcas = new ObservableCollection<Marca>();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                IsLoading = true;
                using var connection = DBConnection.GetConnection();
                await connection.OpenAsync();

                // Cargar Categorías
                string queryCategoria = "SELECT * FROM Categorias";
                using (var command = new SqlCommand(queryCategoria, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    Categorias.Clear();
                    while (await reader.ReadAsync())
                    {
                        Categorias.Add(new Categoria
                        {
                            CategoriaID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }

                // Cargar Marcas
                string queryMarca = "SELECT * FROM Marcas";
                using (var command = new SqlCommand(queryMarca, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    Marcas.Clear();
                    while (await reader.ReadAsync())
                    {
                        Marcas.Add(new Marca
                        {
                            MarcaID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }

                // Cargar Productos
                string queryProducto = @"SELECT p.*, c.Nombre as NombreCategoria, m.Nombre as NombreMarca 
                                       FROM Productos p 
                                       LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID 
                                       LEFT JOIN Marcas m ON p.MarcaID = m.MarcaID 
                                       WHERE p.Estado = 1";

                using (var command = new SqlCommand(queryProducto, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    Productos.Clear();
                    while (await reader.ReadAsync())
                    {
                        Productos.Add(new Producto
                        {
                            ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
                            Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                            CategoriaID = reader.IsDBNull(reader.GetOrdinal("CategoriaID")) ? null : reader.GetInt32(reader.GetOrdinal("CategoriaID")),
                            MarcaID = reader.IsDBNull(reader.GetOrdinal("MarcaID")) ? null : reader.GetInt32(reader.GetOrdinal("MarcaID")),
                            Modelo = reader.IsDBNull(reader.GetOrdinal("Modelo")) ? null : reader.GetString(reader.GetOrdinal("Modelo")),
                            PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                            StockMinimo = reader.GetInt32(reader.GetOrdinal("StockMinimo")),
                            Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                            NombreCategoria = reader.IsDBNull(reader.GetOrdinal("NombreCategoria")) ? string.Empty : reader.GetString(reader.GetOrdinal("NombreCategoria")),
                            NombreMarca = reader.IsDBNull(reader.GetOrdinal("NombreMarca")) ? string.Empty : reader.GetString(reader.GetOrdinal("NombreMarca"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(Busqueda))
            {
                await CargarDatosAsync();
                return;
            }

            var productosFiltrados = Productos.Where(p =>
                p.Codigo.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
                p.Nombre.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
                p.NombreCategoria.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
                p.NombreMarca.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ||
                (p.Modelo?.Contains(Busqueda, StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();

            Productos = new ObservableCollection<Producto>(productosFiltrados);
        }
    }
}
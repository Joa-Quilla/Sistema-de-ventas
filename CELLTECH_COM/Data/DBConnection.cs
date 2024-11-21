using Microsoft.Data.SqlClient;
using CELLTECH_COM.Models;
using System;
using System.Threading.Tasks;

namespace CELLTECH_COM.Data
{
    public class DBConnection
    {
        private static readonly string connectionString = @"Data Source=MSI;Initial Catalog=CellTech3;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static async Task<(bool success, string message, Usuario? user)> ValidateLoginAsync(string username, string password)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                string query = @"SELECT u.*, p.Nombres, p.Apellidos, r.Nombre as RolNombre 
                               FROM Usuarios u 
                               LEFT JOIN Personas p ON u.PersonaID = p.PersonaID 
                               LEFT JOIN Roles r ON u.RolID = r.RolID 
                               WHERE u.NombreUsuario = @Username 
                               AND u.Contrasena = @Password 
                               AND u.Estado = 1";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var usuario = new Usuario
                    {
                        UsuarioID = reader.GetInt32(reader.GetOrdinal("UsuarioID")),
                        NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                        NombreCompleto = $"{reader.GetString(reader.GetOrdinal("Nombres"))} {reader.GetString(reader.GetOrdinal("Apellidos"))}",
                        Rol = reader.GetString(reader.GetOrdinal("RolNombre"))
                    };
                    return (true, "Login exitoso", usuario);
                }
                return (false, "Usuario o contraseña incorrectos", null);
            }
            catch (Exception ex)
            {
                return (false, $"Error al intentar iniciar sesión: {ex.Message}", null);
            }
        }
    }
}
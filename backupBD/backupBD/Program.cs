using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        // Datos de conexión de SQL Server
        string servidor = @"";  // Nombre del servidor que has mostrado en la imagen
        string[] basesDeDatos = { "xxx" }; // Bases de datos a respaldar

        // Ruta base donde se guardarán los backups
        string rutaBaseBackup = @"C:\backups\";

        // Obtener la fecha y hora actual para agregarla al nombre del archivo
        string fechaActual = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        try
        {
            foreach (string baseDatos in basesDeDatos)
            {
                // Generar la ruta para el archivo de backup
                string rutaBackup = $"{rutaBaseBackup}{baseDatos}_backup_{fechaActual}.bak";

                // Cadena de conexión a SQL Server usando autenticación de Windows
                string cadenaConexion = $"Server={servidor};Database={baseDatos};Integrated Security=true;TrustServerCertificate=True;";

                // Crear la conexión a SQL Server
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Comando T-SQL para realizar el backup de la base de datos
                    string comandoBackup = $"BACKUP DATABASE [{baseDatos}] TO DISK = '{rutaBackup}' WITH INIT;";

                    // Crear el comando
                    using (SqlCommand comando = new SqlCommand(comandoBackup, conexion))
                    {
                        // Ejecutar el comando
                        comando.ExecuteNonQuery();
                    }

                    // Informar al usuario
                    Console.WriteLine($"Backup de la base de datos {baseDatos} completado exitosamente en: {rutaBackup}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al realizar el backup: " + ex.Message);
        }
    }
}

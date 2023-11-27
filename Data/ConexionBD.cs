using MySql.Data.MySqlClient;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data
{
    public class ConexionBD
    {
            private readonly string connectionString;

            //public ConexionBD(string server, string database, string username, string password)
            //{
            //    connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
            //}
            public ConexionBD(IConfiguration configuration)
            {
                string server = configuration["ConnectionStrings:Server"];
                string database = configuration["ConnectionStrings:Database"];
                string username = configuration["ConnectionStrings:Username"];
                string password = configuration["ConnectionStrings:Password"];

                connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
            }
            public MySqlConnection ObtenerConexion()
            {
                return new MySqlConnection(connectionString);
            }
        }
    }

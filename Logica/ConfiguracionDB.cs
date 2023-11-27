
using MySql.Data.MySqlClient;
using System.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Logica;

    public class ConfiguracionDB
    {

        MySqlConnection conex;

        private readonly ConexionBD _conexionBD;

        public ConfiguracionDB(ConexionBD conexionBD)
        {
        _conexionBD = conexionBD;
         }

        public void conec()
        {
            conex = _conexionBD.ObtenerConexion();
        }
        public bool Conectar()
        {
            try
            {
                Console.WriteLine("Conectar: Antes de crear la conexión");
                conec();
                Console.WriteLine("Conectar: Después de crear la conexión");

                Console.WriteLine("Conectar: Antes de abrir la conexión");
                conex.Open();
                Console.WriteLine("Conectar: Después de abrir la conexión");

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error al conectar con la base de datos: " + exc.Message);
                return false;
            }
        }

        /* private  MySqlConnection ConexionMySlq()
         {
             MySqlConnection Conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConexionMysql"].ConnectionString);

             try
             {
                 Conexion.Open();
             }
             catch
             {
                 return null;
             }

             return Conexion;
         }
         */
        public void Desconectar()
        {
            Console.WriteLine("Desconectar: Verificando estado de la conexión");
            if (conex.State == ConnectionState.Open)
            {
                Console.WriteLine("Desconectar: Cerrando la conexión");
                conex.Close();
                Console.WriteLine("Desconectar: La conexión ha sido cerrada");
            }
        }

        public int RetornarValidacion(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            Console.WriteLine("RetornarValidacion: Antes de crear el comando");

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = sentencia;
            comando.CommandType = TipoComando;
            comando.Connection = conex;

            Console.WriteLine("RetornarValidacion: Antes de agregar los parámetros");

            foreach (MySqlParameter parametro in ListaParametros)
            {
                comando.Parameters.Add(parametro);
            }

            Console.WriteLine("RetornarValidacion: Antes de ejecutar el comando");

            int count = Convert.ToInt32(comando.ExecuteScalar());

            Console.WriteLine("RetornarValidacion: Después de ejecutar el comando");

            Desconectar();

            Console.WriteLine("RetornarValidacion: Fin de la función");

            return count;
        }


        public void EjecutarOperacion(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            Console.WriteLine("EjecutarOperacion: Inicio");

            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = sentencia;
            comando.CommandType = TipoComando;
            comando.Connection = conex;

            Console.WriteLine("EjecutarOperacion: Antes de agregar los parámetros");

            foreach (MySqlParameter parametro in ListaParametros)
            {
                comando.Parameters.Add(parametro);
            }

            Console.WriteLine("EjecutarOperacion: Antes de ejecutar el comando");

            comando.ExecuteNonQuery();

            Console.WriteLine("EjecutarOperacion: Después de ejecutar el comando");

            Desconectar();

            Console.WriteLine("EjecutarOperacion: Fin");
        }

        public DataTable EjecutarConsulta(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            Console.WriteLine("EjecutarConsulta: Inicio");

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = new MySqlCommand(sentencia, conex);
            adaptador.SelectCommand.CommandType = TipoComando;

            Console.WriteLine("EjecutarConsulta: Antes de agregar los parámetros");

            foreach (MySqlParameter parametro in ListaParametros)
            {
                adaptador.SelectCommand.Parameters.Add(parametro);
            }

            Console.WriteLine("EjecutarConsulta: Antes de llenar el DataSet");

            DataSet resultado = new DataSet();
            adaptador.Fill(resultado);

            Console.WriteLine("EjecutarConsulta: Después de llenar el DataSet");

            Desconectar();

            Console.WriteLine("EjecutarConsulta: Fin");

            return resultado.Tables[0];
        }

        public DataTable EjecutarConsultaDS(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            Console.WriteLine("EjecutarConsultaDS: Inicio");

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = new MySqlCommand(sentencia, conex);
            adaptador.SelectCommand.CommandType = TipoComando;

            Console.WriteLine("EjecutarConsultaDS: Antes de agregar los parámetros");

            foreach (MySqlParameter parametro in ListaParametros)
            {
                adaptador.SelectCommand.Parameters.Add(parametro);
            }

            Console.WriteLine("EjecutarConsultaDS: Antes de crear el DataSet");

            DataSet dataset = new DataSet();

            Console.WriteLine("EjecutarConsultaDS: Antes de llenar el DataSet");

            adaptador.Fill(dataset);

            Console.WriteLine("EjecutarConsultaDS: Después de llenar el DataSet");

            Desconectar();

            Console.WriteLine("EjecutarConsultaDS: Fin");

            return dataset.Tables[0];
        }

        public void EjecutarTransaccion(List<string> Sentencia)
        {
            Console.WriteLine("EjecutarTransaccion: Inicio");

            MySqlTransaction transa = conex.BeginTransaction();
            MySqlCommand mySqlCommand;

            for (int i = 0; i < Sentencia.Count; i++)
            {
                if (Sentencia[i].Length > 0)
                {
                    Console.WriteLine("EjecutarTransaccion: Ejecutando sentencia " + (i + 1));

                    mySqlCommand = new MySqlCommand(Sentencia[i], conex);
                    mySqlCommand.Transaction = transa;
                    mySqlCommand.ExecuteNonQuery();
                }
            }

            Console.WriteLine("EjecutarTransaccion: Fin");
        }







}

  
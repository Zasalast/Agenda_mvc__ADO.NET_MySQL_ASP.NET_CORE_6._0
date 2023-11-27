using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using MySql.Data.MySqlClient;
namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public interface IPermisosRepository
{
    List<Permiso> GetPermisos();
    Permiso GetPermisoById(int id);
    void DeletePermiso(int id);

    void CreatePermiso(Permiso permiso, int[] rolesIds);

    void UpdatePermiso(int permisoId, Permiso permiso, int[] rolesIds);
}
public class PermisosRepository : IPermisosRepository
{
        private string _connectionString;

        // Código anterior

        public void CreatePermiso(Permiso permiso, int[] rolesIds)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            // Insert Permiso

            string rolesInsertSql = "INSERT INTO roles_permisos VALUES ";

            foreach (var rolId in rolesIds)
            {
                rolesInsertSql += $"(@permisoId, {rolId}),";
            }

            // Quita última coma
            rolesInsertSql = rolesInsertSql.Remove(rolesInsertSql.Length - 1);

            using (var cmd = new MySqlCommand(rolesInsertSql, conn))
            {
                cmd.Parameters.AddWithValue("@permisoId", permiso.IdPermiso);
                cmd.ExecuteNonQuery();
            }
        }
    }

        public void DeletePermiso(int id)
        {
            throw new NotImplementedException();
        }

        public Permiso GetPermisoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Permiso> GetPermisos()
        {
            throw new NotImplementedException();
        }

        public void UpdatePermiso(int permisoId, Permiso permiso, int[] rolesIds)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            // Actualiza Permiso

            // Elimina roles actuales
            string deleteSql = "DELETE FROM roles_permisos WHERE id_permiso = @idPermiso";

            // Inserta nuevos roles
            string insertSql = "INSERT INTO...";

            using (var cmd = new MySqlCommand(deleteSql, conn))
            {
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new MySqlCommand(insertSql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

        internal void UpdatePermiso(int id, Permiso permiso)
        {
            throw new NotImplementedException();
        }
    }
}
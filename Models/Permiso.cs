using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class Permiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPermiso { get; set; }
        public string Nombre { get; set; }
        public ICollection<RolPermiso>? RolesPermisos { get; set; }
    }
}

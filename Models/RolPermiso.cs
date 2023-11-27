using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class RolPermiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }
       

        public int IdPermiso { get; set; }
       
    }
}

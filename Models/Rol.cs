using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdRol { get; set; }
        public string ?Nombre { get; set; }

        public static implicit operator int(Rol v)
        {
            throw new NotImplementedException();
        }
    }
}

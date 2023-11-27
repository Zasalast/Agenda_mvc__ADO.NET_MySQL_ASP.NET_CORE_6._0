using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class Asistencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAsistencia { get; set; }
        public char? EstadoAsistencia { get; set; }
        public int? IdAgendamiento { get; set; }

        public Agendamiento Agendamiento { get; set; }
    }
}

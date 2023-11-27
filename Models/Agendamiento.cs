using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class Agendamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdAgendamiento { get; set; }
        public int? IdCliente { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public char? Estado { get; set; }
        public int? IdAgenda { get; set; }       
       
        public int? IdCancelacion { get; set; }
        public int? IdReserva { get; set; }
       
    }
}

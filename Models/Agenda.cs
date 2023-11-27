using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models
{
    public class Agenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdAgenda { get; set; }
        public int? IdSede { get; set; }
        public int? IdServicio { get; set; }
        public int? IdHorario { get; set; }
        public int? IdProfesional { get; set; }

        public Sede Sede { get; set; }
        public Servicio Servicio { get; set; }
        public Horario Horario { get; set; }
        public Persona Profesional { get; set; }
    }
}

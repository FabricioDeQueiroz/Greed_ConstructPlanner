using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Construct_Planner_API.Models
{
    public class Agendamento
    {
        public int Id { get; init; }
        
        [Required]
        public int ObraId { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataInicioProjeto { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataInicioFormas { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataInicioConcretagem { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataInicioTransporte { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataInicioMontagem { get; set; }
        
        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime DataFimMontagem { get; set; }
        
        [NotMapped]
        public int MaxLateness { get; set; }
        
        public virtual Obra? Obra { get; set; }
    }
}

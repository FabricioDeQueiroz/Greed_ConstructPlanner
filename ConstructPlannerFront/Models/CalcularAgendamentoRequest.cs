using System.ComponentModel.DataAnnotations;

namespace Construct_Planner_API.Models
{
    public class CalcularAgendamentoRequest
    {
        [Required(ErrorMessage = "A data inicial é obrigatória")]
        public DateTime DataInicial { get; set; }
    }
}

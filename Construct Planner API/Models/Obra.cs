using System.ComponentModel.DataAnnotations;

namespace Construct_Planner_API.Models {
    public class Obra
    {
        public int Id { get; init; }
        
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public DateTime Deadline { get; set; } = DateTime.Now;
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Color { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A duração não pode ser negativa.")]
        public int Project { get; set; } = 0;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A duração não pode ser negativa.")]
        public int Mold { get; set; } = 0;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A duração não pode ser negativa.")]
        public int Concreting { get; set; } = 0;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A duração não pode ser negativa.")]
        public int Transport { get; set; } = 0;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A duração não pode ser negativa.")]
        public int Mounting { get; set; } = 0;
    }
}
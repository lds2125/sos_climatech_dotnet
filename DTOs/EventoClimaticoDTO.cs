using System;
using System.ComponentModel.DataAnnotations;
namespace GsDotNet.DTOs
{
    public class EventoClimaticoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O tipo do evento é obrigatório")]
        [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "A data de ocorrência é obrigatória")]
        public DateTime DataOcorrencia { get; set; }
        [Required(ErrorMessage = "O local é obrigatório")]
        [StringLength(100, ErrorMessage = "O local deve ter no máximo 100 caracteres")]
        public string Local { get; set; }
        [Required(ErrorMessage = "A severidade é obrigatória")]
        [Range(1, 5, ErrorMessage = "A severidade deve ser entre 1 e 5")]
        public int Severidade { get; set; }
    }
}

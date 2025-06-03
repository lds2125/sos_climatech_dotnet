using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GsDotNet.Models
{
    public class PessoaAfetada
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; }
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O tipo de afetação é obrigatório")]
        [Display(Name = "Tipo de Afetação")]
        [StringLength(50, ErrorMessage = "O tipo de afetação deve ter no máximo 50 caracteres")]
        public string TipoAfetacao { get; set; }
        [Required(ErrorMessage = "O evento relacionado é obrigatório")]
        [Display(Name = "Evento Climático")]
        public int EventoClimaticoId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        [ForeignKey("EventoClimaticoId")]
        public virtual EventoClimatico EventoClimatico { get; set; }
    }
}

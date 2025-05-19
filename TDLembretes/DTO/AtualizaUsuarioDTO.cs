using System.ComponentModel.DataAnnotations;

namespace TDLembretes.DTO
{
    public class AtualizarUsuarioDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Senha { get; set; } 

        public string Telefone { get; set; }

        public int Pontos { get; set; }
    }
}


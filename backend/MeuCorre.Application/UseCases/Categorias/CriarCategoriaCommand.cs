using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias
{
    public class CriarCategoriaCommandValidator
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatório!")]
        public string Tipo { get; set; }

        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

        [Required(ErrorMessage = "ID do usuário é obrigatório!")]
        public Guid UsuarioID { get; set; }
    }
}

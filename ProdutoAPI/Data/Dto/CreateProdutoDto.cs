using System.ComponentModel.DataAnnotations;

namespace ProdutoAPI.Data.Dto
{
    public class CreateProdutoDto
    {
        [Required(ErrorMessage = "O campo descrição é obrigatório", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "Descrição máximo de 200 caracteres")]
        public string Descricao { get; set; }
    }
}

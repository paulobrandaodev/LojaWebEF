using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaWebEF.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduto { get; set; }
        [Required(ErrorMessage="Este campo não pode ficar vazio")]
        [StringLength(50, MinimumLength=2)]
        public string NomeProduto { get; set; }
        [Required(ErrorMessage="Este campo não pode ficar vazio")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public ICollection<Pedido> Pedido { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TB_CATEGORIAS")]
public class Categoria
{
    [Key]
    public int CD_CATEGORIA { get; set; }

    [Required]
    [MaxLength(100)]
    public string NM_CATEGORIA { get; set; }
}

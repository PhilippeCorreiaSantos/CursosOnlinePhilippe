using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

[Table("TB_AVALIACOES")]
public class Avaliacao
{
    [Key]
    public int CD_AVALIACAO { get; set; }

    [ForeignKey("Aluno")]
    public int CD_ALUNO { get; set; }

    [ValidateNever]
    public Aluno Aluno { get; set; }

    [ForeignKey("Curso")]
    public int CD_CURSO { get; set; }

    [ValidateNever]
    public Curso Curso { get; set; }

    [Range(1, 10)]
    public int VL_NOTA { get; set; }

    public string DS_COMENTARIO { get; set; }

    public DateTime DT_AVALIACAO { get; set; } = DateTime.Now;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TB_ALUNOS")]
public class Aluno
{
    [Key]
    public int CD_ALUNO { get; set; }

    [Required]
    [MaxLength(100)]
    public string NM_ALUNO { get; set; }

    [MaxLength(100)]
    public string EML_ALUNO { get; set; }

    public DateTime DT_NASC { get; set; }

    public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}

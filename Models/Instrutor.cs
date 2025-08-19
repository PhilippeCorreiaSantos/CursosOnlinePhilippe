using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TB_INSTRUTORES")]
public class Instrutor
{
    [Key]
    public int CD_INSTRUTOR { get; set; }

    [Required]
    [MaxLength(100)]
    public string NM_INSTRUTOR { get; set; }

    [MaxLength(100)]
    public string EML_INSTRUTOR { get; set; }

    [MaxLength(100)]
    public string NM_ESPECIALIDADE { get; set; }

}

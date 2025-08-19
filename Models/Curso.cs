using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

[Table("TB_CURSOS")]
public class Curso
{
    [Key]
    public int CD_CURSO { get; set; }

    [Required]
    [MaxLength(150)]
    public string NM_CURSO { get; set; }

    public string DS_CURSO { get; set; }

    [ForeignKey("Categoria")]
    public int CD_CATEGORIA { get; set; }
    [ValidateNever]
    public Categoria Categoria { get; set; }

    [ForeignKey("Instrutor")]
    public int CD_INSTRUTOR { get; set; }
    [ValidateNever]
    public Instrutor Instrutor { get; set; }

    
    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    //public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}

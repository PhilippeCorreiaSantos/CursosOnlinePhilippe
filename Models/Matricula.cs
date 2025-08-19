using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

[Table("TB_MATRICULAS")]
public class Matricula
{
    [Key]
    public int CD_MATRICULA { get; set; }

    [Required(ErrorMessage = "O campo Aluno é obrigatório.")]
    [Display(Name = "Aluno")]
    public int? CD_ALUNO { get; set; }

    [Required(ErrorMessage = "O campo Curso é obrigatório.")]
    [Display(Name = "Curso")]
    public int? CD_CURSO { get; set; }

    [Required(ErrorMessage = "A data da matrícula é obrigatória.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data da Matrícula")]
    public DateTime DT_MATRICULA { get; set; } = DateTime.Now;

    [ValidateNever]
    [ForeignKey("CD_ALUNO")]
    public Aluno Aluno { get; set; }

    [ValidateNever]
    [ForeignKey("CD_CURSO")]
    public Curso Curso { get; set; }
}

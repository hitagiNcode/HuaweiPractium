using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiEx.Domain.Exam;

public class Exam : BaseEntity
{
    [Key]
    public int ExamID { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal FullMarks { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Duration { get; set; }
}
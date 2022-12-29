using System.ComponentModel.DataAnnotations;

namespace CertiEx.Domain.Exam;

public class ExamResult : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string CandidateID { get; set; }
    public string SessionID { get; set; }
    public int ExamID { get; set; }
    public string Exam { get; set; }
    public string Date { get; set; }
    public string Score { get; set; }
    public string Status { get; set; }
}

public enum StatusEnum
{
    Passed,
    Failed
}
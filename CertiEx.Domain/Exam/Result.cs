using System.ComponentModel.DataAnnotations;

namespace CertiEx.Domain.Exam;

public class Result:BaseEntity
{
    [Key]
    public int Sl_No { get; set; }
    public string SessionID { get; set; }
    public string CandidateID { get; set; }        
    public int ExamID { get; set; }
    public int QuestionID { get; set; }
    public int AnswerID { get; set; }
    public int SelectedOptionID { get; set; }
    public bool IsCorrent { get; set; }
}
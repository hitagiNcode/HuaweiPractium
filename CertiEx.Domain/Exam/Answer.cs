using System.ComponentModel.DataAnnotations;

namespace CertiEx.Domain.Exam;

public class Answer:BaseEntity
{
    [Key]
    public int Sl_No { get; set; }
    public int QuestionID { get; set; }
    public int ChoiceID { get; set; }        
    public string DisplayText { get; set; }
}
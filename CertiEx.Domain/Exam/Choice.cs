using System.ComponentModel.DataAnnotations;

namespace CertiEx.Domain.Exam;

public class Choice:BaseEntity
{
    [Key]
    public int ChoiceID { get; set; }
    public int QuestionID { get; set; }      
    public string DisplayText { get; set; }
}
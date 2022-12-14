using Microsoft.AspNetCore.Identity;

namespace CertiEx.Domain.Exam;

public class Request
{
    
}

public class Option
{
    public string CandidateID { get; set; }
    public int ExamID { get; set; }
    public int QuestionID { get; set; }
    public int AnswerID { get; set; }
    public int SelectedOption { get; set; }
}
public class Root
{
    public IdentityUser objCandidate { get; set; }
    public List<QuizAttempt> objAttempt { get; set; }
    public List<UserScoreShort> objLeaderboard { get; set; }
}
public class QuizAttempt
{
    public int Sl_No { get; set; }
    public string SessionID { get; set; }
    public int ExamID { get; set; }
    public string Exam { get; set; }
    public string Date { get; set; }
    public int Score { get; set; }
    public string Status { get; set; }
}    
public class QuizReport
{
    public int CandidateID { get; set; }
    public string SessionID { get; set; }
    public int ExamID { get; set; }
    public string Exam { get; set; }
    public string Date { get; set; }
    public string Message { get; set; }
}    
public class ReqReport
{
    public int ExamID { get; set; }
    public string CandidateID { get; set; }
    public string SessionID { get; set; }
}
public class ReqCertificate
{
    public int CandidateID { get; set; }
    public string SessionID { get; set; }
    public int ExamID { get; set; }
    public string Exam { get; set; }
    public string Date { get; set; }
    public string Score { get; set; }
}

public class ResPDF
{
    public bool IsSuccess { get; set; }
    public string Path { get; set; }
}
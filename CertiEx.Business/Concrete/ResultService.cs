using CertiEx.Business.Abstract;
using CertiEx.Dal;
using CertiEx.Domain;
using CertiEx.Domain.Exam;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CertiEx.Business.Concrete;

public class ResultService<TEntity> : IResultService<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DbSet<TEntity> _dbSet;
    private readonly ICacheService _cache;

    public ResultService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, ICacheService cache)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _cache = cache;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<int> AddResult(List<TEntity> entity)
    {
        _dbSet.AddRange(entity);
        var output = await _dbContext.SaveChangesAsync();
        return output;
    }

    public async Task<bool> CalculateResult(List<Result> entity)
    {
        var exams = await _dbContext.Exam.ToListAsync();
        var examName = exams.Find(r => r.ExamID == entity.First().ExamID)?.Name ?? "Test Exam";
        var correctAnswers = entity.Count(result => result.IsCorrent);
        
        var score = correctAnswers * 100;

        return true;
    }

    public async Task<IEnumerable<QuizAttempt>> GetAttemptHistory(string argCandidateID)
    {
        try
        {
            var rran = new Random();
            var exams = await _dbContext.Exam.ToListAsync();
            var candidateHistory = await _dbContext.Result.Where(r => r.CandidateID == argCandidateID).ToListAsync();

            var sessionHistory = candidateHistory.GroupBy(r => r.SessionID).Select(r => r.Key).ToList();

            var quizAttempts = new List<QuizAttempt>();

            foreach (var session in sessionHistory)
            {
                var answersOfSession = candidateHistory.Where(result => result.SessionID == session).ToList();

                var correctAnswers = answersOfSession.Count(result => result.IsCorrent);
                var status = "Not Passed";
                if (correctAnswers >= answersOfSession.Count / 2)
                {
                    status = "Passed";
                }

                quizAttempts.Add(new QuizAttempt
                {
                    Sl_No = rran.Next(0, 1000),
                    SessionID = session,
                    ExamID = answersOfSession.First().ExamID,
                    Exam = exams.Find(r => r.ExamID == answersOfSession.First().ExamID)?.Name ?? "Test Exam",
                    Date = answersOfSession.First().CreatedOn?.ToString("dd/MM/yyyy") ?? "01/01/2023",
                    Score = (correctAnswers * 100).ToString(),
                    Status = status
                });
            }

            return quizAttempts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<IEnumerable<QuizReport>> ScoreReport(ReqReport argRpt)
    {
        try
        {
            List<QuizReport> obj = await _dbContext.Set<QuizReport>().FromSqlRaw(@"EXEC GetReport {0},{1},{2}", argRpt.ExamID, argRpt.CandidateID, argRpt.SessionID).ToListAsync();
            return obj;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<string> GetCertificateString(ReqCertificate argRpt)
    {
        IdentityUser _candidate = await _userManager.FindByIdAsync(argRpt.CandidateID.ToString());

        try
        {
            string cert = null;
            cert = @"<html>
<head>
<style type='text/css'>
.outer-border {
    width: 800px;
    height: 650px;
    padding: 20px;
    text-align: center;
    border: 10px solid #673AB7;
    margin-left: 21%
}

.inner-dotted-border {
    width: 750px;
    height: 600px;
    padding: 20px;
    text-align: center;
    border: 5px solid #673AB7;
    border-style: dotted
}

.certification {
    font-size: 50px;
    font-weight: bold;
    color: #663ab7
}

.certify {
    font-size: 25px
}

.name {
    font-size: 30px;
    color: green
}

.fs-30 {
    font-size: 30px
}

.fs-20 {
    font-size: 20px
}
</style>
</head>
<body>
<div class='outer-border'>
    <div class='inner-dotted-border'><br> 
	<span><img src='https://www.citypng.com/public/uploads/preview/hd-gold-black-certificate-logo-transparent-png-31625761576hadwkhbj6t.png' alt='avatar' class='w3-left w3-circle w3-margin-right' style='width:100px'></span>
	<br><br><br>
	<span class='certification'>Certificate of Completion</span> 
	<br><br> 
	<span class='certify'><i>This is to certify that</i></span> 
	<br><br> <span class='name'><b>" + _candidate.UserName + @"</b></span><br />
	<br /> <span class='certify'><i>has successfully completed the certification</i></span>
	<br /><br /> <span class='fs-30'>" + argRpt.Exam + @"</span> <br /><br /> 
	<span class='fs-20'>with score of <b>" + argRpt.Score + @"</b></span>
	<br /><br /><br /> 
	<span class='certify'><i>dated</i></span><br> <span class='fs-30'>" + argRpt.Date + @"</span>
    </div>
</div>
</body>
</html>";
            return cert;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

}

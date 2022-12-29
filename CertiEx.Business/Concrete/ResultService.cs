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

    public ResultService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<int> AddResult(List<TEntity> entity)
    {
        _dbSet.AddRange(entity);
        var output = await _dbContext.SaveChangesAsync();
        return output;
    }

    public async Task<IEnumerable<QuizAttempt>> GetAttemptHistory(string argCandidateID)
    {
        try
        {
            var obj = new List<QuizAttempt>()
            {
                new()
                {
                    Sl_No = 0,
                    SessionID = "123",
                    ExamID = 1,
                    Exam = "Huawei Cloud Certified Associate",
                    Date = DateTime.Now.ToString("dd/MM/yyyy"),
                    Score = "500",
                    Status = "Passed"
                }
            };
            return obj;
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

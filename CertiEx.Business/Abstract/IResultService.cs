using CertiEx.Domain.Exam;

namespace CertiEx.Business.Abstract;

public interface IResultService<TEntity>
{
    Task<IEnumerable<QuizAttempt>> GetAttemptHistory(string argCandidateID);
    Task<IEnumerable<QuizReport>> ScoreReport(ReqReport argRpt);
    Task<int> AddResult(List<TEntity> entity);
    Task<bool> CalculateResultAddRedis(List<Result> entity);
    Task<string> GetCertificateString(ReqCertificate argRpt);

    Task<IEnumerable<ExamScore>> GetAllScores();
    void InitCache();

    Task InitCacheAsync();
}
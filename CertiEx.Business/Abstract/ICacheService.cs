using CertiEx.Domain.Exam;

namespace CertiEx.Business.Abstract;

public interface ICacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T data, DateTimeOffset expirationTime);
    object RemoveData(string key);
    Task CleanCache();

    Task InitLeaderBoard(IEnumerable<ExamScore> scores);
}
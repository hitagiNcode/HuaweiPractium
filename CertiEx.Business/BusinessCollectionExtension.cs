using CertiEx.Business.Abstract;
using CertiEx.Business.Concrete;
using CertiEx.Domain.Exam;
using Microsoft.Extensions.DependencyInjection;

namespace CertiEx.Business;

public static class BusinessCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICacheService, CacheService>()
            .AddScoped<IExamService<Exam>, ExamService<Exam>>()
            .AddScoped<IQuestionService<Question>, QuestionService<Question>>()
            .AddScoped<IResultService<Result>, ResultService<Result>>();
    }
}
using CertiEx.Domain.Exam;

namespace CertiEx.Dal.DbInit;

public class DbInit : IDbInit
{
    private readonly ApplicationDbContext _db;

    public DbInit(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Initialize()
    {
        if (!_db.Exam.Any())
        {
            _db.Exam.Add(new Exam
            {
                Name = "Huawei Cloud Certified Associate",
                FullMarks = 10,
                Duration = 3
            });

            _db.Exam.Add(new Exam
            {
                Name = "AWS Certified Solutions Architect - Associate",
                FullMarks = 5,
                Duration = 1.30m
            });

            _db.Exam.Add(new Exam
            {
                Name = "Azure Fundamentals",
                FullMarks = 3,
                Duration = 1
            });

            _db.SaveChanges();
        }
    }
}
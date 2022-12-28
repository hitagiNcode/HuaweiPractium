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

            _db.SaveChanges();
        }
    }
}
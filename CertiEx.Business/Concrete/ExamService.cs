using System.Linq.Expressions;
using CertiEx.Business.Abstract;
using CertiEx.Dal;
using CertiEx.Domain;
using Microsoft.EntityFrameworkCore;

namespace CertiEx.Business.Concrete;

public class ExamService<TEntity> : IExamService<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private DbSet<TEntity> _dbSet;
    
    public ExamService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public async Task<IEnumerable<TEntity>> GetExamList()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<TEntity> GetExam(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<IQueryable<TEntity>> SearchExam(Expression<Func<TEntity, bool>> search = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (search != null)
        {
            query = query.Where(search);
        }
        return query;
    }

    public async Task<int> AddExam(TEntity entity)
    {
        _dbSet.Add(entity);
        var output = await _dbContext.SaveChangesAsync();
        return output;
    }

    public async Task<int> UpdateExam(TEntity entity)
    {
        _dbSet.Update(entity);
        var output = await _dbContext.SaveChangesAsync();
        return output;
    }
    public async Task<int> DeleteExam(TEntity entity)
    {
        _dbSet.Remove(entity);
        var output = await _dbContext.SaveChangesAsync();
        return output;
    }

}

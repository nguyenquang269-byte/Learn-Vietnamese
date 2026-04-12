using Microsoft.EntityFrameworkCore;
using Noihay.DataAccessLayer.Interfaces;
using System.Linq.Expressions;

namespace Noihay.DataAccessLayer.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly NoihayDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(NoihayDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => 
        await _dbSet.Where(predicate).ToListAsync();
}

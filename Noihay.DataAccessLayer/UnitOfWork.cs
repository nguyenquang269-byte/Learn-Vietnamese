using Noihay.DataAccessLayer.Interfaces;
using Noihay.DataAccessLayer.Repositories;

namespace Noihay.DataAccessLayer;

public class UnitOfWork : IUnitOfWork
{
    private readonly NoihayDbContext _context;
    private readonly Dictionary<string, object> _repositories = new();

    public UnitOfWork(NoihayDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            if (repositoryInstance != null)
                _repositories.Add(type, repositoryInstance);
        }
        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}

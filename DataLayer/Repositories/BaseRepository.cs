using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly AppDbContext _dbDbContext;
        protected readonly DbSet<T> Set;
        #endregion

        #region Constructors
        protected BaseRepository(AppDbContext dbContext)
        {
            _dbDbContext = dbContext;
            Set = _dbDbContext.Set<T>();
        }
        #endregion

        #region Public members
        public bool Any(Func<T, bool> expression)
        {
            return Set.Any(expression);
        }

        public List<T> GetAll()
        {
            return Set.ToList();
        }

        public T? GetById(int id)
        {
            return Set.FirstOrDefault(entity => entity.Id == id);
        }

        public void Insert(T entity)
        {
            Set.Add(entity);
        }

        public int SaveChanges() => _dbDbContext.SaveChanges();
        #endregion
    }
}

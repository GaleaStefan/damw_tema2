using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class ClassRepository : BaseRepository<Class>
    {
        #region Constructors
        public ClassRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public members
        public Class? FindByCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            return Set.FirstOrDefault(c => c.Code == code);
        }
        #endregion
    }
}

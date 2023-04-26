using DataLayer.Entities;

namespace DataLayer.Repositories;

public class UserRepository : BaseRepository<User>
{
    #region Constructors
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    #endregion

    #region Public members
    public User? FindByEmail(string email)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));
        return Set.FirstOrDefault(u => u.Email == email);
    }
    #endregion
}

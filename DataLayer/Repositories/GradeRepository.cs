using DataLayer.Entities;

namespace DataLayer.Repositories;

public class GradeRepository : BaseRepository<Grade>
{
    #region Constructors
    public GradeRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    #endregion

    #region Public members
    public IEnumerable<Grade> GetByStudentId(int studentId)
    {
        return Set.Where(g => g.StudentId == studentId);
    }

    public IEnumerable<Grade> GetByTeacherId(int teacherId)
    {
        return Set.Where(g => g.Class.TeacherId == teacherId)
            .OrderBy(g => g.StudentId);
    }
    #endregion
}

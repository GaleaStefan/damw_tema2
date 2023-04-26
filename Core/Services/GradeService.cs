using Core.Dtos;
using DataLayer.Entities;
using DataLayer.Repositories;

namespace Core.Services;

public class GradeService
{
    #region Fields
    private readonly ClassRepository _classRepository;
    private readonly GradeRepository _gradeRepository;
    private readonly UserRepository _userRepository;
    #endregion

    #region Constructors
    public GradeService(GradeRepository gradeRepository, UserRepository userRepository, ClassRepository classRepository)
    {
        _gradeRepository = gradeRepository;
        _userRepository = userRepository;
        _classRepository = classRepository;
    }
    #endregion

    #region Public members
    public string? AddGrade(AddGradeDto addGradeInformation, int teacherId)
    {
        var studentId = _userRepository.FindByEmail(addGradeInformation.StudentEmail)?.Id;
        if (studentId == null)
            return "Student not found";
        var @class = _classRepository.FindByCode(addGradeInformation.ClassCode);
        if (@class == null)
            return "Class not found";
        if (@class.TeacherId != teacherId)
            return "This is not your subject";
        _gradeRepository.Insert(new Grade
            { ClassId = @class.Id, StudentId = studentId.Value, Value = addGradeInformation.Value });
        return null;
    }

    public List<GradeViewDto> GetAll()
    {
        return _gradeRepository.GetAll().Select(g => new GradeViewDto
        {
            ClassCode = g.Class.Code,
            StudentEmail = g.Student.Email,
            Value = g.Value
        }).ToList();
    }

    public List<GradeViewDto> GetStudentGrades(int studentId)
    {
        return _gradeRepository.GetByStudentId(studentId)
            .Select(g => new GradeViewDto
            {
                ClassCode = g.Class.Code,
                StudentEmail = g.Student.Email,
                Value = g.Value
            })
            .ToList();
    }

    public List<GradeViewDto> GetTeacherGrades(int teacherId)
    {
        return _gradeRepository.GetByTeacherId(teacherId)
            .Select(g => new GradeViewDto
            {
                ClassCode = g.Class.Code,
                StudentEmail = g.Student.Email,
                Value = g.Value
            })
            .ToList();
    }
    #endregion
}

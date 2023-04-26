using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers;

[ApiController]
[Route("api/grades")]
public class GradesController : Controller
{
    #region Fields
    private readonly GradeService _gradeService;
    #endregion

    #region Constructors
    public GradesController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }
    #endregion

    #region Public members
    [Authorize(Roles = "Teacher")]
    [HttpPost("/add")]
    public IActionResult AddGrade(AddGradeDto addGradeInformation)
    {
        var userId = int.Parse(User.Claims.First(i => i.Type == "userId").Value);
        var error = _gradeService.AddGrade(addGradeInformation, userId);
        if (error != null)
            return BadRequest(error);
        return Ok();
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpGet("/get-all")]
    public IActionResult GetAll()
    {
        return Ok(_gradeService.GetAll());
    }

    [Authorize(Roles = "Student")]
    [HttpGet("/grades-student")]
    public IActionResult GetStudentGrades()
    {
        var userId = int.Parse(User.Claims.First(i => i.Type == "userId").Value);
        return Ok(_gradeService.GetStudentGrades(userId));
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("/grades-teacher")]
    public IActionResult GetTeacherGrades()
    {
        var userId = int.Parse(User.Claims.First(i => i.Type == "userId").Value);
        return Ok(_gradeService.GetTeacherGrades(userId));
    }
    #endregion
}

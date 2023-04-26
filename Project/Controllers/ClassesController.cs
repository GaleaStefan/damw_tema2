using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        #region Fields
        private readonly ClassService _classService;
        #endregion

        #region Constructors
        public ClassesController(ClassService classService)
        {
            _classService = classService;
        }
        #endregion

        #region Public members
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult Add(AddClassDto payload)
        {
            var userId = int.Parse(User.Claims.First(i => i.Type == "userId").Value);
            return _classService.Add(payload, userId) ? Ok() : BadRequest("Class cannot be added");
        }

        [AllowAnonymous]
        [HttpGet("get-all")]
        public ActionResult<List<ClassViewDto>> GetAll()
        {
            var result = _classService.GetAll();
            return Ok(result);
        }
        #endregion
    }
}

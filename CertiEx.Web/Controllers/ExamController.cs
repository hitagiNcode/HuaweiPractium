using CertiEx.Domain.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CertiEx.Business.Abstract;

namespace CertiEx.Web.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExamService<Exam> _exam;
        private readonly IQuestionService<Question> _question;
        private readonly IResultService<Result> _result;

        public ExamController(UserManager<IdentityUser> userManager, IExamService<Exam> examService,
            IQuestionService<Question> question, IResultService<Result> result)
        {
            _userManager = userManager;
            _exam = examService;
            _question = question;
            _result = result;
        }

        public IActionResult Index()
        {
            var loggedInUser = _userManager.GetUserAsync(User).Result;
            return View(loggedInUser);
        }

        [HttpGet]
        [Route("~/api/Exams")]
        public async Task<IActionResult> Exams()
        {           
            try
            {
                IEnumerable<Exam> lst = await _exam.GetExamList();               
                return Ok(lst.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpGet]
        [Route("~/api/Exam/{ExamID?}")]
        public async Task<IActionResult> Exam(int ExamID)
        {
            try
            {
                Exam exm = await _exam.GetExam(ExamID);
                return Ok(exm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "null";
        }
    }
}

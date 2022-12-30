using CertiEx.Business.Abstract;
using CertiEx.Domain.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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

        [HttpGet]
        [Route("~/api/Questions/{ExamID?}")]
        public async Task<IActionResult> Questions(int ExamID)
        {
            try
            {
                QnA _obj = await _question.GetQuestionList(ExamID);
                return Ok(_obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("~/api/Score")]       
        public async Task<IActionResult> Score(List<Option> objRequest)
        {
            int i = 0;
            bool IsCorrect = false;
            List<Result> objList = null;
            string _SessionID = null;
            try
            {               
                if (objRequest.Count > 0)
                {
                    _SessionID = Guid.NewGuid() + "-" + DateTime.Now;
                    objList = new List<Result>();
                    foreach (var item in objRequest)
                    {
                        if (item.AnswerID == item.SelectedOption)
                            IsCorrect = true;
                        else
                            IsCorrect = false;

                        Result obj = new Result()
                        {
                            CandidateID = item.CandidateID,
                            ExamID = item.ExamID,
                            QuestionID = item.QuestionID,
                            AnswerID = item.AnswerID,
                            SelectedOptionID = item.SelectedOption,
                            IsCorrent = IsCorrect,
                            SessionID= _SessionID,
                            CreatedBy = "SYSTEM",
                            CreatedOn = DateTime.Now
                        };
                        objList.Add(obj);
                    }
                    i = await _result.AddResult(objList);
                    await _result.InitCacheAsync();
                }
               
            }
            catch (Exception ex)
            {
                i = 0;
                throw new Exception(ex.Message, ex.InnerException);           
            }

            return Ok(i);
        }
        
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "null";
        }
    }
}

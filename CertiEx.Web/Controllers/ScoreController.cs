using CertiEx.Business.Abstract;
using CertiEx.Domain.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CertiEx.Web.Controllers
{
    [Authorize]
    public class ScoreController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IResultService<Result> _result;
        private readonly ICacheService _cache;

        public ScoreController(IResultService<Result> result, UserManager<IdentityUser> userManager, ICacheService cache)
        {
            _result = result;
            _userManager = userManager;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Result()
        {
            try
            {
                var objCandidate = _userManager.GetUserAsync(User).Result;

                var obj = await _result.GetAttemptHistory(objCandidate.Id);

                var userScores = await _cache.GetLeaderBoard();

                var scoreList = userScores.Select(r => new UserScoreShort() { Username = r.Item1, Score = r.Item2 }).ToList();

                var objRoot = new Root
                {
                    objCandidate = objCandidate,
                    objAttempt = obj.ToList(),
                    objLeaderboard = scoreList
                };
                return View(objRoot);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpPost]
        [Route("~/api/Report")]
        public async Task<IActionResult> Report(ReqReport argRpt)
        {
            try
            {
                var lst = await _result.ScoreReport(argRpt);
                return Ok(lst.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}

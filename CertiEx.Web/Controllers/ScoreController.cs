﻿using CertiEx.Business.Abstract;
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

        public ScoreController(IResultService<Result> result, UserManager<IdentityUser> userManager)
        {
            _result = result;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Result()
        {
            try
            {
                var _objCandidate = _userManager.GetUserAsync(User).Result;

                IEnumerable<QuizAttempt> _obj = await _result.GetAttemptHistory(_objCandidate.Id);
                Root objRoot = new Root(){
                    objCandidate= _objCandidate,
                    objAttempt = _obj.ToList() 
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
                IEnumerable<QuizReport> lst = await _result.ScoreReport(argRpt);
                return Ok(lst.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
using System.Security.Claims;
using CertiEx.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CertiEx.Web.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public ExamController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "null";
        }
    }
}

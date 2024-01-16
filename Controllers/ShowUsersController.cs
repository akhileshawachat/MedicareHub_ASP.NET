using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebAppDemoStudent.Models;

namespace WebAppDemoStudent.Controllers
{

    [Authorize(Roles = "admin")]
    public class ShowUsersController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public ShowUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

       
        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }


        /*[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityUser model)
        {
            if (!_userManager.User(model.UserName).GetAwaiter().GetResult())
            {
                _userManager.CreateAsync(new IdentityUser(model.UserName)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }*/

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var roleEntity = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleEntity == null)
            {
                return NotFound();
            }

            return View(roleEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_userManager.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Roles' is null.");
            }

            var userEntity = await _userManager.FindByIdAsync(id);
            if (userEntity != null)
            {
                var result = await _userManager.DeleteAsync(userEntity);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error, e.g., return a view with error messages
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}

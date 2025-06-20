using CrudApp2.Models;
using CrudApp2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudApp2.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserDataService _userDataService;

        public UserController(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public IActionResult Index()
        {
            var users = UserDataService.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userDataService.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = UserDataService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_userDataService.UpdateUser(user))
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = UserDataService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userDataService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using AspNetCore;

namespace UniversityManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //list all the roles created by users
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        // return view where allow user to add new
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(IdentityRole model)
        //{
        //    // avoid duplicate role
        //    if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
        //    {
        //        _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
        //    }
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            // avoid duplicate role
            if (!await _roleManager.RoleExistsAsync(model.Name))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
            }
            return RedirectToAction("Index");
        }
    }
}

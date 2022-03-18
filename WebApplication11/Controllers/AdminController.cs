using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    // [Authorize(Roles ="Administrator")]
    public class AdminController : Controller

    {
        //private readonly DatabaseContaxt _contaxt;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
      
        //public AdminController(RoleManager<IdentityRole> roleManager)
        //{
        //    this.roleManager = roleManager;
        //}

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            //_contaxt = contaxt;
        }

        //public async Task<IActionResult> Index()
        //{

        //    return View(await _contaxt.aspnetusers.ToListAsync());

        //}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectRoleModel role)
        {
            var RoleExist = await roleManager.RoleExistsAsync(role.RoleName);

            if (!RoleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return RedirectToAction("ListRoles" , "Admin");
        }
        public async Task<IActionResult> EditRole(string id)
        {
          var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role not Found";
            }
            else
            {
                var Model = new EditRole
                {
                    Id = role.Id,
                    RoleName = role.Name,
                };
                   foreach(var user in userManager.Users.ToList())
                {
                   
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        Model.Users.Add(user.UserName);
                    }
                }
                return View(Model);

                
            }
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRole model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleModel>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        //public IActionResult AssignRole()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> AssignRole(RegisterModel rgm, ProjectRoleModel prm,int id)
        //{
        //    // var UId = await roleManager.RegisterModel.FindAsync(Id);
        //    //var Member = await _contaxt.aspnetuserroles
        //    //   .FirstOrDefaultAsync(m => m.UserId == id);


        //    var obj = new UserRoleModel()

        //    {
        //        //  Id = 0,

        //        UserId = rgm.Id,
        //        RoleId = prm.Id,


        //    };
        //    _contaxt.aspnetuserroles.Add(obj);
        //    _contaxt.SaveChanges();
        //    return View(await _contaxt.aspnetroles.ToListAsync());
        //}


    }
}

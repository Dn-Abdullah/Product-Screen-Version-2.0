using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
   // [Authorize(Roles ="Administrator")]
    public class AdminController : Controller

    {
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
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
            return View();
        }
    }
}

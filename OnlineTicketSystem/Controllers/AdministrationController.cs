using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTicketSystem.Data;
using OnlineTicketSystem.Models;
using OnlineTicketSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public async Task<IActionResult> Company()
        {
            var company = await context.CompanyInformations.ToListAsync();
            return View(company);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterForCompany(Guid comid)
        {
            ViewBag.ComId = comid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForCompany(RegisterForCompanyVM model)
        {
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ComID= model.ComID                    
                };


                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    
                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administration");
                    //}

                    await signInManager.SignInAsync(user, isPersistent: false);


                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (context.CompanyInformations.Any(o => o.CompanyName == model.CompanyName))
                {
                    return BadRequest();
                }

                var company = new CompanyInformation
                {
                    ComID = Guid.NewGuid(),
                    CompanyName = model.CompanyName,                    
                    CreatedTime = DateTime.Now
                };

                context.Add(company);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = false,
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    ComID= Convert.ToString(company.ComID)                    
                };
               

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    context.SaveChanges();
                    var com = context.CompanyInformations.Find(company.ComID);
                    context.CompanyInformations.Attach(com);
                    com.CreatedBy = user.Id;
                    context.SaveChanges();

                    await userManager.AddToRoleAsync(user, "Company Admin");

                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administration");
                    //}

                    //await signInManager.SignInAsync(user, isPersistent: false);


                    return RedirectToAction("confirm", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }




        [HttpGet]
        [Authorize(Roles = "Super Admin, Company Admin")]
        public IActionResult RegisterWithRole()
        {
            //.Where(x => x.Name != "Super Admin")

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in roleManager.Roles.Where(x=>x.Name!="Super Admin"))
            {
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            }

            ViewBag.Roles = list;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Admin, Company Admin")]

        public async Task<IActionResult> RegisterWithRole(RegisterWithRole model)
        {
            var comid= HttpContext.Request.Cookies["ComID"];

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {

                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    ComID = comid

                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);

                    //await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("ListUsers", "Administration");

                }



            }

            return RedirectToAction("RegisterWithRole", "Administration");
        }





        [HttpGet]
        [Authorize(Roles = "Super Admin, Company Admin")]

        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles.Where(x=>x.Name!="Super Admin");
            return View(roles);
        }

        

        [HttpGet]
        [Authorize(Roles = "Super Admin, Company Admin")]

        public async Task<IActionResult> ListUsers()
        {
            var comid = HttpContext.Request.Cookies["ComID"];


            var users = await userManager.Users.Where(x=>x.ComID==comid).ToListAsync();

            if (users.Count == 0)
            {
                return View("NotFound");
            }

            var model = new List<EditUserViewModel>();
            // GetClaimsAsync retunrs the list of user Claims
            // var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var submodel = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = userRoles
                };
                model.Add(submodel);
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Super Admin, Company Admin")]

        public async Task<IActionResult> EditUser(string id)
        {
            ViewBag.userId = id;

            var user = await userManager.FindByIdAsync(id);
            ViewBag.userName = user.UserName;



            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new List<RoleUserViewModel>();

            foreach (var role in roleManager.Roles.Where(x=>x.Name!="Super Admin"))
            {

                var userRoles = await userManager.IsInRoleAsync(user, role.Name);

                var roleUserViewModel = new RoleUserViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (userRoles)
                {
                    roleUserViewModel.IsSelected = true;
                }
                else
                {
                    roleUserViewModel.IsSelected = false;
                }
                model.Add(roleUserViewModel);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin, Company Admin")]

        public async Task<IActionResult> EditUser(List<RoleUserViewModel> model, string id)
        {


            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var role = await roleManager.FindByIdAsync(model[i].RoleId);

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
                        return RedirectToAction("ListUsers");
                }
            }

            return RedirectToAction("ListUsers");
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;
using System.IO;

namespace MyInventory.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private const string FolderPath = "/assets/img/Uploads/";
        private InvDBContext db = new InvDBContext();

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {           
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            ViewBag.LocationId = new SelectList(db.Locations, "Loc_ID", "Location1");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, HttpPostedFileBase file, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    MiddleName = userViewModel.MiddleName,
                    LastName = userViewModel.LastName,
                    PhoneNumber = userViewModel.PhoneNumber,
                    JobTitle = userViewModel.JobTitle,
                    LocationId = userViewModel.LocationId,
                    Photo = "/Content/img/avatar.jpg",
                };

                if (file != null)
                {
                    file.InputStream.Read(new byte[file.ContentLength], 0, file.ContentLength);
                    var filename = "User_" + DateTime.Now.Ticks.ToString() + ".jpg";
                    if (!string.IsNullOrEmpty(userViewModel.Photo))
                    {
                        filename = userViewModel.Photo;
                    }
                    var filePath = Server.MapPath(path: FolderPath);

                    string savedFileName = Path.Combine(filePath, filename);
                    file.SaveAs(savedFileName);
                    user.Photo = savedFileName;
                }
                // Then create:
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            ViewBag.LocationId = db.Locations.ToList().Select(x => new SelectListItem()
            {
                Selected =(x.Loc_ID==user.LocationId),
                Text = x.Location1,
                Value = x.Loc_ID.ToString()
            });

            ViewBag.RoleId = RoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName=user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                JobTitle = user.JobTitle,
                LocationId = user.LocationId,
                Photo=user.Photo
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,UserName,FirstName,MiddleName,LastName,PhoneNumber,JobTitle,LocationId")] EditUserViewModel editUser, HttpPostedFileBase file, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                user.Photo = "/Content/img/avatar.jpg";

                if (user == null)
                {
                    return HttpNotFound();
                }

                if (file != null)
                {
                    file.InputStream.Read(new byte[file.ContentLength], 0, file.ContentLength);
                    var filename = "User_" + DateTime.Now.Ticks.ToString() + ".jpg";
                    if (!string.IsNullOrEmpty(editUser.Photo))
                    {
                        filename = editUser.Photo;
                    }
                    var filePath = Server.MapPath("/Content/img/");

                    string savedFileName = Path.Combine(filePath, filename);
                    file.SaveAs(savedFileName);
                    user.Photo = "/Content/img/" + filename;
                }

                user.UserName = editUser.UserName;
                user.Email = editUser.Email;

                user.FirstName = editUser.FirstName;
                user.MiddleName = editUser.MiddleName;
                user.LastName = editUser.LastName;
                user.PhoneNumber = editUser.PhoneNumber;
                user.JobTitle = editUser.JobTitle;
                user.LocationId = editUser.LocationId;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
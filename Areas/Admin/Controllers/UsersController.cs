using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using PagedList;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private DSTruyen db = new DSTruyen();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UsersController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        // GET: Admin/Users
        public ActionResult Index(string search, int? page)
        {
            var users = db.Users.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.UserName.Contains(search) || u.Email.Contains(search) || u.FullName.Contains(search));
                ViewBag.Search = search;
            }

            // Phân trang
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var userList = users.OrderBy(u => u.UserName).ToList();
            
            // Lấy roles cho mỗi user
            var userRoles = new Dictionary<string, IList<string>>();
            foreach (var user in userList)
            {
                userRoles[user.Id] = userManager.GetRoles(user.Id);
            }
            ViewBag.UserRoles = userRoles;

            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles.ToList();
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model, string role)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    RegisterDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Gán role
                    if (!string.IsNullOrEmpty(role))
                    {
                        await userManager.AddToRoleAsync(user.Id, role);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user.Id, "User");
                    }

                    TempData["SuccessMessage"] = "Tạo người dùng thành công!";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            ViewBag.Roles = roleManager.Roles.ToList();
            return View(model);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                CurrentRoles = userManager.GetRoles(user.Id).ToList()
            };

            ViewBag.Roles = roleManager.Roles.ToList();
            return View(model);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Email = model.Email;
                user.UserName = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Cập nhật roles
                    var currentRoles = await userManager.GetRolesAsync(user.Id);
                    var rolesToAdd = selectedRoles?.Except(currentRoles) ?? new string[] { };
                    var rolesToRemove = currentRoles.Except(selectedRoles ?? new string[] { });

                    await userManager.AddToRolesAsync(user.Id, rolesToAdd.ToArray());
                    await userManager.RemoveFromRolesAsync(user.Id, rolesToRemove.ToArray());

                    TempData["SuccessMessage"] = "Cập nhật người dùng thành công!";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            ViewBag.Roles = roleManager.Roles.ToList();
            return View(model);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserRoles = userManager.GetRoles(user.Id);
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Không cho phép xóa chính mình
            if (user.Id == User.Identity.GetUserId())
            {
                TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của chính mình!";
                return RedirectToAction("Index");
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Xóa người dùng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa người dùng!";
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
                roleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

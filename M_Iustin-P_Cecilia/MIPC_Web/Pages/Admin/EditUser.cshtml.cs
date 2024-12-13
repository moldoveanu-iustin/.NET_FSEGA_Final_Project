using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MIPC_Web.Pages.Admin
{
    [Authorize(Roles = "Client")]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public EditUserModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public UserInfo User { get; set; }

        public List<SelectListItem> RoleOptions { get; private set; }

        public class UserInfo
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        // GET: Edit page
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            User = new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "No Role"
            };

            // Dynamically fetch role options
            var rolesFromDb = await _roleManager.Roles.ToListAsync();
            RoleOptions = rolesFromDb.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            return Page();
        }

        // POST: Edit page (submit changes)
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = User.Email;

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!currentRoles.Contains(User.Role))
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to remove roles.");
                    return Page();
                }

                var addResult = await _userManager.AddToRoleAsync(user, User.Role);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to assign role.");
                    return Page();
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user.");
                return Page();
            }

            // After successful update, redirect to a meaningful page (e.g., dashboard or user list)
            return RedirectToPage("/Admin/UserList"); // or /Admin/Dashboard, depending on your app
        }
    }
}

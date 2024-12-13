using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIPC_Web.Pages.Admin
{
    [Authorize(Roles = "Client")]
    public class DashboardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DashboardModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public int TotalAccounts {  get; set; }
        public List<UserInfo> Users {  get; set; }

        public class UserInfo
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public async Task OnGetAsync()
        {
            TotalAccounts = _userManager.Users.Count();
            var userList = await _userManager.Users.ToListAsync();
            Users = new List<UserInfo>();

            foreach (var user in userList)
            {
                var role = await GetUserRole(user);
                Users.Add(new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = role
                });
            }
        }

        private async Task<string> GetUserRole(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "No Role";
        }
    }
}

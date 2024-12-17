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
    [Authorize(Roles = "Admin")]
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

        public int TotalUsers { get; set; }
        public int TotalClients { get; set; }
        public int TotalSoferi { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalCars { get; set; }
        public List<UserInfo> Users { get; set; }

        public class UserInfo
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public async Task OnGetAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            Users = new List<UserInfo>();
            TotalUsers = userList.Count;
            TotalClients = 0;
            TotalSoferi = 0;
            TotalAdmins = 0;

            foreach (var user in userList)
            {
                var role = await GetUserRole(user);
                Users.Add(new UserInfo
                {
                    Id = user.Id,
                    Email = user.UserName,
                    Role = role
                });
                if (role == "Client")
                {
                    TotalClients++;
                }
                else if (role == "Sofer")
                {
                    TotalSoferi++;
                }
                else if (role == "Admin")
                {
                    TotalAdmins++;
                }
            }
            Users = Users.OrderBy(u => u.Role).ToList();
            TotalCars = await _context.Masini.CountAsync();
        }

        private async Task<string> GetUserRole(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "No Role";
        }
    }
}

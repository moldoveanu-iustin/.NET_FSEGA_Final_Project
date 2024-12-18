using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MIPC_Web.Models;

namespace MIPC_Web.Pages
{
    public class NotificationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NotificationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Notificare> Notifications { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                Notifications = await _context.Notificari
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.Id)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostMarkAsReadAsync(int notificationId)
        {
            var notificare = await _context.Notificari.FindAsync(notificationId);
            if (notificare != null)
            {
                notificare.Status = "Citita";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMarkAsUnReadAsync(int notificationId)
        {
            var notificare = await _context.Notificari.FindAsync(notificationId);
            if (notificare != null)
            {
                notificare.Status = "Necitita";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}

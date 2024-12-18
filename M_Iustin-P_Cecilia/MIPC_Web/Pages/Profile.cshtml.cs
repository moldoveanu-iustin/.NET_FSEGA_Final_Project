using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MIPC_Web.Models;

namespace MIPC_Web.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IdentityUser CurrentUser { get; set; }
        public string CurrentRole { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Nume { get; set; }

        [BindProperty]
        public string Prenume { get; set; }

        [BindProperty]
        public string Telefon { get; set; }

        [BindProperty]
        public int CarId { get; set; } 

        public List<Masina> Cars { get; set; }
        public Masina CurrentCar { get; set; }
        public List<Rezervare> Reservations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            CurrentUser = user;

            var roles = await _userManager.GetRolesAsync(user);
            CurrentRole = roles.FirstOrDefault();

            if (CurrentRole == "Sofer")
            {
                var sofer = await _context.Soferi.FirstOrDefaultAsync(s => s.Id == user.Id);
                if (sofer != null)
                {
                    Nume = sofer.Nume;
                    Prenume = sofer.Prenume;
                    Telefon = sofer.Telefon;
                    CarId = sofer.MasinaId;
                    CurrentCar = await _context.Masini.FirstOrDefaultAsync(c => c.Id == sofer.MasinaId);
                }
                Cars = await _context.Masini.Where(c => c.PreluataDeSofer == false).ToListAsync();
            }

            if (CurrentRole == "Client")
            {

                Reservations = await _context.Rezervari
                    .Where(r => r.ClientId == CurrentUser.Id)
                    .Include(r => r.Sofer)
                    .ThenInclude(s => s.Masina)
                    .ToListAsync();
            }
            else if (CurrentRole == "Sofer")
            {
                Reservations = await _context.Rezervari
                    .Where(r => r.SoferId == CurrentUser.Id)
                    .Include(r => r.Sofer)
                    .ThenInclude(s => s.Masina)
                    .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var currentRole = roles.FirstOrDefault();

            var sofer = await _context.Soferi.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (sofer == null)
            {
                sofer = new Sofer
                {
                    Id = user.Id,
                    Email = user.UserName,
                    Nume = Nume,
                    Prenume = Prenume,
                    Telefon = Telefon,
                    MasinaId = CarId != 0 ? CarId : 0
                };
                _context.Soferi.Add(sofer);
                await _context.SaveChangesAsync();
            }
            else
            {
                sofer.Nume = Nume;
                sofer.Prenume = Prenume;
                sofer.Telefon = Telefon;
                if (CarId != 0)
                {
                    sofer.MasinaId = CarId;
                }

                _context.Soferi.Update(sofer);
                await _context.SaveChangesAsync();
            }

            if (CarId != 0)
            {
                var masina = await _context.Masini.FirstOrDefaultAsync(m => m.Id == CarId);
                if (masina != null)
                {
                    masina.PreluataDeSofer = true;
                    _context.Masini.Update(masina);
                    await _context.SaveChangesAsync();
                }
            }


            if (currentRole == "Client")
            {
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    user.PhoneNumber = PhoneNumber;
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update phone number.");
                        return Page();
                    }
                }
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostAcceptRideAsync(int rezervareId)
        {
            var rezervare = await _context.Rezervari.FindAsync(rezervareId);

            if (rezervare != null && rezervare.Status == "In asteptare")
            {
                rezervare.Status = "Acceptata";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeclineRideAsync(int rezervareId)
        {
            var rezervare = await _context.Rezervari.FindAsync(rezervareId);

            if (rezervare != null && rezervare.Status == "In asteptare")
            {
                rezervare.Status = "Respinsa";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRideAsync(int rezervareId)
        {
            var rezervare = await _context.Rezervari.FindAsync(rezervareId);

            if (rezervare != null && rezervare.Status == "In asteptare")
            {
                _context.Rezervari.Remove(rezervare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}

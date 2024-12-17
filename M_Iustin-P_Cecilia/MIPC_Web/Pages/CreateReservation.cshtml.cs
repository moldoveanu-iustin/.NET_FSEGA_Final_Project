using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIPC_Web.Models;
using System.Security.Claims;

namespace MIPC_Web.Pages
{
    public class CreateReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int CarId { get; set; }

        [BindProperty]
        public string LocatiePlecare { get; set; }

        [BindProperty]
        public string LocatieSosire { get; set; }

        [BindProperty]
        public double Distance { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public DateTime DataStartRezervare { get; set; }

        [BindProperty]
        public DateTime DataStopRezervare { get; set; }

        public decimal PretPerKm { get; private set; }

        public Masina Car { get; set; }
        public Sofer Sofer { get; set; }

        public void OnGet()
        {
            Car = _context.Masini.Find(CarId);
            if (Car != null)
            {
                PretPerKm = Car.PretPerKm;
                Sofer = _context.Soferi.FirstOrDefault(s => s.MasinaId == Car.Id);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "User not logged in.");
                return Page();
            }
            var userId = userIdClaim.Value;
            Car = _context.Masini.Find(CarId);
            if (Car == null)
            {
                ModelState.AddModelError(string.Empty, "Selected car not found.");
                return Page();
            }

            PretPerKm = Car.PretPerKm;
            Distance = Distance;
            Price = Price;
            var sofer = _context.Soferi.FirstOrDefault(s => s.MasinaId == CarId);
            if (sofer == null)
            {
                ModelState.AddModelError(string.Empty, "Driver not found for the selected car.");
                return Page();
            }
            var soferId = sofer.Id;
            var reservation = new Rezervare
            {
                ClientId = userId,
                DataRezervare = DateTime.Now,
                DataStartCursa = DataStartRezervare,
                DataStopCursa = DataStopRezervare,
                LocatiePlecare = LocatiePlecare,
                LocatieSosire = LocatieSosire,
                DistantaTotala = (decimal)Distance,
                PretTotal = Price,
                SoferId = soferId,
                Status = "In asteptare"
            };
            _context.Rezervari.Add(reservation);
            _context.SaveChanges();
            return RedirectToPage("/SuccesReservation", new { reservationId = reservation.Id });
        }
    }
}

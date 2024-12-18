using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIPC_Web.Models;
using MIPC_Web.Data;

namespace MIPC_Web.Pages
{
    public class SuccesReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SuccesReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int ReservationId { get; set; }

        public async Task<IActionResult> OnGetAsync(int reservationId)
        {
            ReservationId = reservationId;

            // Fetch the reservation
            var rezervare = await _context.Rezervari.FindAsync(reservationId);
            if (rezervare == null)
            {
                return NotFound(); // Handle invalid reservationId
            }

            var clientNotification = new Notificare
            {
                UserId = rezervare.ClientId,
                Descriere = $"Rezervarea dumneavoastra (ID: {reservationId}) a fost trimisa catre soferul masinii! Puteti vedea detaliile rezervarii pe pagina profilului!",
                Status = "Necitita",
                OraTrimitere = DateTime.Now
                
            };

            var soferNotification = new Notificare
            {
                UserId = rezervare.SoferId, // Replace with actual property name
                Descriere = $"A fost creeata o noua rezervare (ID: {reservationId}) pentru masina dumneavoastra! Puteti vedea detaliile rezervarii pe pagina profilului!",
                Status = "Necitita",
                OraTrimitere = DateTime.Now
            };

            // Add notifications to the database
            _context.Notificari.Add(clientNotification);
            _context.Notificari.Add(soferNotification);

            // Save changes
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}

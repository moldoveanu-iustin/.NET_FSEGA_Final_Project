using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MIPC_Web.Pages
{
    public class SuccesReservationModel : PageModel
    {
        public int ReservationId { get; set; }

        public void OnGet(int reservationId)
        {
            ReservationId = reservationId;
        }
    }
}

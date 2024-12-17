using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIPC_Web.Models;

namespace MIPC_Web.Pages.Admin
{
    public class AddCarModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddCarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CarInputModel Input { get; set; }

        public class CarInputModel
        {
            public string Marca { get; set; }
            public string Model { get; set; }
            public int AnFabricatie { get; set; }
            public string NumarInmatriculare { get; set; }
            public decimal PretPerKm { get; set; }
            public string Imagine1 { get; set; }
            public string Imagine2 { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var car = new Masina
            {
                Marca = Input.Marca,
                Model = Input.Model,
                AnFabricatie = Input.AnFabricatie,
                NumarInmatriculare = Input.NumarInmatriculare,
                PretPerKm = Input.PretPerKm,
                FirmaId = 1,
                imagine1 = Input.Imagine1,
                imagine2 = Input.Imagine2
            };

            _context.Masini.Add(car);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Dashboard");
        }
    }
}

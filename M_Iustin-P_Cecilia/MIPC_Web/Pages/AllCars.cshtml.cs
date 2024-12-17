using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MIPC_Web.Pages
{
    public class AllCarsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllCarsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CarViewModel> Cars { get; set; }

        public async Task OnGetAsync()
        {
            Cars = await (from car in _context.Masini
                          join driver in _context.Soferi on car.Id equals driver.MasinaId into carDrivers
                          from driver in carDrivers.DefaultIfEmpty()
                          select new CarViewModel
                          {
                              Id = car.Id,
                              Marca = car.Marca,
                              Model = car.Model,
                              AnFabricatie = car.AnFabricatie,
                              NumarInmatriculare = car.NumarInmatriculare,
                              PretPerKm = car.PretPerKm,
                              Imagine1 = car.imagine1,
                              Imagine2 = car.imagine2,
                              Sofer = driver != null ? new SoferViewModel
                              {
                                  Nume = driver.Nume,
                                  Prenume = driver.Prenume,
                                  Telefon = driver.Telefon,
                                  Email = driver.Email,
                                  PozaProfil = driver.PozaProfil
                              } : null
                          }).ToListAsync();
        }

        public class CarViewModel
        {
            public int Id { get; set; }
            public string Marca { get; set; }
            public string Model { get; set; }
            public int AnFabricatie { get; set; }
            public string NumarInmatriculare { get; set; }
            public decimal PretPerKm { get; set; }
            public string Imagine1 { get; set; }
            public string Imagine2 { get; set; }
            public SoferViewModel Sofer { get; set; }
        }
        public class SoferViewModel
        {
            public string Nume { get; set; }
            public string Prenume { get; set; }
            public string Telefon { get; set; }
            public string Email {  get; set; }
            public string PozaProfil {  get; set; }
        }
    }
}

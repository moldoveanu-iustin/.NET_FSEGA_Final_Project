using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIPC_Web.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Firma> Firme { get; set; }
    public DbSet<Masina> Masini { get; set; }
    public DbSet<Sofer> Soferi { get; set; }
    public DbSet<Client> Clienti { get; set; }
    public DbSet<Rezervare> Rezervari { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
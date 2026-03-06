using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Models;

namespace Gestao_Escala.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Escala> Escala { get; set; }
    }   
}
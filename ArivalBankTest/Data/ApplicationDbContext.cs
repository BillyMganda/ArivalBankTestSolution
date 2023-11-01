using ArivalBankTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ArivalBankTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }
    }
}

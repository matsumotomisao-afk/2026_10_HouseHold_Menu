using HouseHold.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseHold.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  //最新の記法
        {
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<SubjectName> SubjectNames { get; set; }
        public DbSet<Menu> Menus { get; set; }


    }

}

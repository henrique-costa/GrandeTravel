using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Microsoft.EntityFrameworkCore;
using TravelGrande.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TravelGrande.Services
{
    public class TravelGrandeDbContext : IdentityDbContext<MyUser>
    {
        public DbSet<TravelPackage> TblTravelPackage { get; set; }        
        public DbSet<ProviderProfile> TblProviderProfile { get; set; }
        public DbSet<CustomerProfile> TblCustomerProfile { get; set; }
        public DbSet<Booking> TblBooking { get; set; }        
        public DbSet<Feedback> TblFeedback { get; set; }


        //configure the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=TravelGrandeDb; Trusted_Connection=True");
        }
    }
}

using AplikacjaKurierska.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AplikacjaKurierska.API.Data

{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext>options):base(options){}

        public DbSet<Value>Values{get;set;}
        public DbSet<User> Users { get; set; }

        public DbSet<Modul> Moduls { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<PredictableDate> PredictableDates { get; set; }




    }
}
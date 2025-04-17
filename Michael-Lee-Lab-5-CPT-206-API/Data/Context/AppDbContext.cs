using StatesDatabaseClassLibrary;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Michael_Lee_Lab_5_CPT_206.Data.Context
{
    public class StatesContext : DbContext
    {
        public StatesContext(DbContextOptions<StatesContext> options) : base(options) { }

        public DbSet<State> States { get; set; }
    }
}

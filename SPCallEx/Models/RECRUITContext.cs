using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SPCallEx.Models
{
    public partial class RECRUITContext : DbContext
    {
        public RECRUITContext()
        {
        }

        public RECRUITContext(DbContextOptions<RECRUITContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=10.36.3.30;Database=RECRUIT;user id=sa;password=tnuacc_;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

        }
    }
}

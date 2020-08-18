using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models {
    public partial class PruebaContext : DbContext {
        public PruebaContext () { }

        public PruebaContext (DbContextOptions<PruebaContext> options) : base (options) { }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            OnModelCreatingPartial (modelBuilder);
        }

        partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
    }
}
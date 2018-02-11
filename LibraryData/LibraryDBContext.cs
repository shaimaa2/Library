using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryData
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions options) :base (options) { }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

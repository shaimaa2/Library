using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryData
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<Patron> patrons { get; set; }
    }
}

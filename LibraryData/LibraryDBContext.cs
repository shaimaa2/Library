﻿using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryData
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions options) :base (options) { }
        public DbSet<LibraryAsset> LibraryAssets { get; set; }
        public DbSet<LibraryBranch> LibraryBranchs { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Holds> Holds { get; set; }
        public DbSet<BranchHours> BranchHours { get; set; }

        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<CheckoutHistory> CheckoutHistorys { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

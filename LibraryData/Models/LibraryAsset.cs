using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public abstract class LibraryAsset
    {
        
        public int Id   { get; set; }

        [Required]
        public int Title   { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public Status Status { get; set; }

        public String ImageUrl { get; set; }

        public int NumberOfCopies { get; set; }

        public virtual LibraryBranch Location { get; set; }

    }
}

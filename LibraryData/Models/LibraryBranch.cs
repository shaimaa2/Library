using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
  public  class LibraryBranch
    {
        public int Id { get; set; }

        [Required]
        [StringLength (30,ErrorMessage ="Limit Branch Name 40 character")]
        public String Name { get; set; }

        [Required]
        public String Address { get; set; }
        [Required]
       public String Telephone { get; set; }

        public String Description { get; set; }
        public DateTime OpenDate { get; set; }



        public virtual IEnumerable<Patron> patrons { get; set; }

        public virtual IEnumerable<LibraryAsset> LibraryAssets { get; set; }

        public String ImageUrl { get; set; }

    }
}

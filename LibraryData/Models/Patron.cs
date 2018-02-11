using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
  public  class Patron
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public String TelephoneNumber { get; set; }

        public virtual LibraryCard LibraryCard { get; set; }

        public virtual LibraryBranch HomeLibraryBranch { get; set; }
    }
}

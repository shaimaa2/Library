using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Book : LibraryAsset
    {
        [Required]
        public int ISBN { get; set; }
        [Required]
        public String Author { get; set; }
        [Required]
        public String DeweyIndex { get; set; }
    }
}

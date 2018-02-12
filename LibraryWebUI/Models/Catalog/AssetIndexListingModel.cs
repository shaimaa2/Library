using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebUI.Models.Catalog
{
    public class AssetIndexListingModel
    {
       public int Id { get; set; }
        public String Title { get; set; }
        public String ImageUrl { get; set; }
        public String AuthorOrDirctor { get; set; }
        public String Type { get; set; }
        public String NumberOfCopies { get; set; }
        public String DeweyCallNumber { get; set; }

    }
}

using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interfaces
{
    public interface ILibraryAsset
    {
         IEnumerable<LibraryAsset> GetAll();
         void Add(LibraryAsset newAsset);

        LibraryAsset GetById(int id);

        String GetType(int id);
        String GetDeweyIndex(int id);
        String GetTitle(int id);
        String GetIsbn(int id);

        LibraryBranch GetCurrentlocation(int id);

        String GetDirectorOrAuthor(int id);


    }
}

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

        string GetType(int id);
        string GetDeweyIndex(int id);
        string GetTitle(int id);
        string GetIsbn(int id);

        LibraryBranch GetCurrentlocation(int id);

        string GetDirectorOrAuthor(int id);


    }
}

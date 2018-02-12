using LibraryData;
using LibraryData.Interfaces;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryDBContext _Context;

        LibraryAssetService(LibraryDBContext Context)
        {
            _Context = Context;
        }

        public void Add(LibraryAsset newAsset)
        {
            _Context.Add(newAsset);
            _Context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _Context.LibraryAssets
                .Include(Asset => Asset.Status)
                .Include(Asset => Asset.Location);
        }

        public LibraryAsset GetById(int id)
        {
            return _Context.LibraryAssets
                 .Include(asset => asset.Status)
                 .Include(asset => asset.Location)
                 .FirstOrDefault(asset => asset.Id == id);

            // return GetAll().FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentlocation(int id)
        {
            return GetById(id).Location;

            //  return _Context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;
        }

        public string GetDeweyIndex(int id)
        {
             // var isbook = _Context.LibraryAssets.OfType<Book>().Where(book => book.Id == id).Any();
            //Discriminator 
            if (_Context.Books.Any(book => book.Id == id))
            {
                return _Context.Books
                    .FirstOrDefault(book => book.Id == id).DeweyIndex;

            }
            else
            {
                return "";
            }
        }



        public string GetIsbn(int id)
        {
            if (_Context.Books.Any(book => book.Id == id))
            {
                return _Context.Books
                    .FirstOrDefault(book => book.Id == id).ISBN;

            }
            else
            {
                return "";
            }
        }

        public string GetTitle(int id)
        {
            return 
                _Context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == id)
                .Title;
        }

        public string GetType(int id)
        {
            var isbook = _Context.LibraryAssets.OfType<Book>().Where(book => book.Id == id);
            return isbook.Any() ? "Book" : "Video";


        }

        public string GetDirectorOrAuthor(int id)
        {
            var isbook = _Context.LibraryAssets.OfType<Book>().Where(book => book.Id == id).Any();

            return isbook ? _Context.Books.FirstOrDefault(b => b.Id == id).Author :
                            _Context.Videos.FirstOrDefault(b => b.Id == id).Director
                            ?? "Unknown";
        }
    }
}

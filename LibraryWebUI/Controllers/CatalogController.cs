using LibraryData.Interfaces;
using LibraryWebUI.Models.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebUI.Controllers
{
    public class CatalogController : Controller
    {

        private ILibraryAsset _asset;
        public CatalogController(ILibraryAsset asset)
        {
            _asset = asset;
        }
        public IActionResult Index()
        {
            var assetmodel = _asset.GetAll();
            var Listingresult = assetmodel
                .Select(result => new AssetIndexListingModel()
                {
                    Id = result.Id,
                    Title = result.Title,
                    ImageUrl =result.ImageUrl,
                    AuthorOrDirctor = _asset.GetDirectorOrAuthor(result.Id),
                    DeweyCallNumber = _asset.GetDeweyIndex(result.Id),
                    Type = _asset.GetType(result.Id)
                });
            var model = new AssetIndexModel()
            {
                Assets = Listingresult
            };
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var asset = _asset.GetById(id);
            var model = new AssetDetailModel
            {
                Id = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,

                Status = asset.Status.Name,

                DeweyCallNamber = _asset.GetDeweyIndex(id),
                AuthorOrDirector = _asset.GetDirectorOrAuthor(id),
                CurrentLocation = _asset.GetCurrentlocation(id).Name,
                ISBN = _asset.GetIsbn(id),
                Type = _asset.GetType(id)

            };
            return View(model);
        }
    }
}

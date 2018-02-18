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
        private ICheckOut _checkout;

        public CatalogController(ILibraryAsset asset, ICheckOut checkout)
        {
            _asset = asset;
            _checkout = checkout;
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
        public IActionResult Detail(int assetid)
        {
            var asset = _asset.GetById(assetid);

            var currentholds = _checkout.GetCurrentHolds(assetid)
                .Select( a =>  new AssetHoldModel
                {
                    HoldPlaced = _checkout.GetCurrentHoldPlaced(assetid),
                    PatronName = _checkout.GetCurrentHoldPatronName(assetid)
                });

                var model = new AssetDetailModel
                {
                        Id = assetid,
                        Title = asset.Title,
                        Year = asset.Year,
                        Cost = asset.Cost,
                        Status = asset.Status.Name,
                        ImageUrl = asset.ImageUrl,

                        DeweyCallNamber = _asset.GetDeweyIndex(assetid),
                        AuthorOrDirector = _asset.GetDirectorOrAuthor(assetid),
                        CurrentLocation = _asset.GetCurrentlocation(assetid).Name,
                        PatronName = _checkout.GetCurrentCheckoutPatron(assetid),
                        LatestCheckout = _checkout.GetLatestCheckout(assetid),
                        CheckoutHistory = _checkout.GetCheckOutHistory(assetid),
                        AssetHold = currentholds,

                        ISBN = _asset.GetIsbn(assetid),
                        Type = _asset.GetType(assetid)

                };
            return View(model);
        }
    }
}

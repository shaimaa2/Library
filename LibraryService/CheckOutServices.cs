using LibraryData;
using LibraryData.Interfaces;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryService
{
    public class CheckOutServices : ICheckOut
    {
        private LibraryDBContext _context;
        CheckOutServices(LibraryDBContext contaxt)
        {
            _context = contaxt;
        }

       public void  Add(CheckOut newcheckout)
        {
            _context.Add(newcheckout);
            _context.SaveChanges();
        }


        public IEnumerable<CheckOut> GetAll()
        {
            return _context.CheckOuts;
        }

        public CheckOut GetById(int checkoutid)
        {
           return GetAll()
                .FirstOrDefault(CheckOut => CheckOut.Id == checkoutid)
        }

        public IEnumerable<CheckoutHistory> GetCheckOutHistory(int libraryassetid)
        {
            return 
                _context.CheckoutHistorys
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == libraryassetid)
        }



       public IEnumerable<Holds> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public CheckOut GetLatestCheckout(int assetid)
        {
            return
                _context.CheckOuts
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryAsset.Id == assetid)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }
        void  MarkFound(int assetId)
        {
            var now = DateTime.Now;

            UpdateAssetStatus(assetId, "Available");
            RemoveExcistingCheckouts(assetId);
            CloseAnyExistingCheckoutHistory(assetId,now);
           
                _context.SaveChanges();

        }

        private void UpdateAssetStatus(int assetId, string v)
        {
            var item = _context.LibraryAssets
              .FirstOrDefault(asset => asset.Id == assetId);

            _context.Update(item);

            item.Status = _context.Status
                .FirstOrDefault(s => s.Name == v);
        }

        private void CloseAnyExistingCheckoutHistory(int assetId, DateTime now)
        {
            // close any existing checkout history

            var history =
                _context.CheckoutHistorys
                .FirstOrDefault(c => c.LibraryAsset.Id == assetId
                                    && c.CheckIn == null);
            if (history == null)
            {
                _context.Update(history);
                history.CheckIn = now;
            }
            }

        private void RemoveExcistingCheckouts(int assetId)
        {
            var checkout = _context.CheckOuts
                        .FirstOrDefault(c => c.LibraryAsset.Id == assetId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        void MarkLost(int assetId)
        {

            UpdateAssetStatus(assetId,"Lost");
            _context.SaveChanges();
        }

        void Plachold(int assetId, int LibraryCardId)
        {
            throw new NotImplementedException();
        }

        void CheckinItem(int assetId, int LibraryCardId)
        {
            var now = DateTime.Now;
            var item =
                _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == assetId);

            _context.Update(item);

            //remove any existing checkouts on the item
            //close any existing checkout history 
            //if there are holds on the item check out the item  to 
            //the librarycard with the earliest hold
            //otherwise update the item status to available

        }

        void ICheckOut.CheckoutItem(int assetId, int LibraryCardId)
        {
            throw new NotImplementedException();
        }

        string ICheckOut.GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        DateTime ICheckOut.GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();
        }

    }
}

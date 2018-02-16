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

        public CheckOutServices(LibraryDBContext contaxt)
        {

            _context = contaxt;
        }

        public void  Add(CheckOut newcheckout)
        {
            _context.Add(newcheckout);
            _context.SaveChanges();
        }

        public CheckOut GetById(int checkoutid)
        {
            return GetAll()
                 .FirstOrDefault(CheckOut => CheckOut.Id == checkoutid);
        }

        public IEnumerable<CheckoutHistory> GetCheckOutHistory(int libraryassetid)
        {
            return
                _context.CheckoutHistorys
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == libraryassetid);
        }



        public IEnumerable<Holds> GetCurrentHolds(int assetid)
        {
            return _context.Holds
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == assetid);
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
            var now = DateTime.Now;

            var asset = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetId);

            var card = _context.LibraryCards
                .FirstOrDefault(c => c.Id == LibraryCardId);

            var hold = new Holds
            {
                HoldPlaced = now,
                LibraryAsset = asset,
                LibraryCard = card
            };

            _context.Add(hold);

            _context.SaveChanges();


        }

        void CheckinItem(int assetId, int LibraryCardId)
        {
            var now = DateTime.Now;
            var item =
                _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == assetId);

            _context.Update(item);

            //remove any existing checkouts on the item
            RemoveExcistingCheckouts(assetId);

            //close any existing checkout history 
            CloseAnyExistingCheckoutHistory(assetId, now);


            //look for an existing holds in this item
            var currentholds = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == assetId);

            //if there are holds on the item check out the item  to 
            //the librarycard with the earliest hold
            if (currentholds.Any())
            {
                CheckOuttoearliesthold(assetId,currentholds);

            }

            //otherwise update the item status to available
            UpdateAssetStatus(assetId, "Available");
            _context.SaveChanges();

        }

        private void CheckOuttoearliesthold(int assetId, IQueryable<Holds> currentholds)
        {
            var earliesthold = currentholds
                .OrderBy(h => h.HoldPlaced)
                .FirstOrDefault();
            var card = earliesthold.LibraryCard;

            _context.Remove(earliesthold);
            _context.SaveChanges();
            CheckoutItem(assetId,card.Id);
        }

        void CheckoutItem(int assetId, int LibraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;

            }
            var item = _context.LibraryAssets
                .FirstOrDefault(l => l.Id == assetId);

            UpdateAssetStatus(assetId, "Checked Out");

            var card = _context.LibraryCards
                .FirstOrDefault(l => l.Id == LibraryCardId);

            var now = DateTime.Now;

            var checkout = new CheckOut
            {
                LibraryAsset = item,
                LibraryCard = card,
                Since = now,
                Untill = GetDefaultCheckoutTime(now)
            };
            _context.Add(checkout);

            var checkouthistory = new CheckoutHistory
            {
                LibraryAsset = item,
                LibraryCard = card,
                CheckOut = now

            };

            _context.Add(checkouthistory);
            _context.SaveChanges();
        }
        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
                return now.AddDays(30);
         }

        private bool IsCheckedOut(int assetId)
        {
            return
                _context.CheckOuts
                .Where(c => c.LibraryAsset.Id == assetId)
                .Any();
        }



        string GetCurrentHoldPatronName(int holdid)
        {
            var hold = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h =>h.LibraryCard)
                .FirstOrDefault(h => h.Id == holdid);

            var cardId = hold?.LibraryCard.Id;

            var patron = _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;

        }


        DateTime GetCurrentHoldPlaced(int holdid)
        {

           return 
                _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .FirstOrDefault(h => h.Id == holdid)
                .HoldPlaced;
        }

        IEnumerable<CheckOut> GetAll()
        {
            return _context.CheckOuts;
        }


        string ICheckOut.GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);

            if (checkout == null)
            {
                return "";
            }

            var CardId = checkout.LibraryCard.Id;

            var patron = _context.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == CardId);

            return patron.FirstName + " " + patron.LastName;

        }

        private CheckOut GetCheckoutByAssetId(int assetId)
        {
            return _context.CheckOuts
                .Include(co => co.LibraryCard)
                .Include(co => co.LibraryAsset)
                .FirstOrDefault(co => co.LibraryAsset.Id == assetId);
        }
    }
}

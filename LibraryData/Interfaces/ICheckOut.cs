using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interfaces
{
    public interface ICheckOut
    {
        IEnumerable<CheckOut> GetAll();
        IEnumerable <CheckoutHistory> GetCheckOutHistory(int libraryassetid);
        IEnumerable<Holds> GetCurrentHolds(int id);


        void Add(CheckOut newcheckout);
        void CheckoutItem(int assetId, int LibraryCardId);
        void CheckinItem(int assetId, int LibraryCardId);
        void Plachold(int assetId, int LibraryCardId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);

        CheckOut GetById(int checkoutid);
        CheckOut GetLatestCheckout(int checkoutid);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
    }
}

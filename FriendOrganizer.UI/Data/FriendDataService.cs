using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            yield return new Friend { FirstName = "Andrzej", LastName = "Nowak" };
            yield return new Friend { FirstName = "Przemek", LastName = "Stary" };
            yield return new Friend { FirstName = "Blazej", LastName = "Hutek" };
            yield return new Friend { FirstName = "Hubert", LastName = "Plis" };
        }
    }
}

using System.Data.Entity.Migrations;
using FriendOrganizer.Model;

namespace FriendOrganizer.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(f => f.FirstName,
                new Friend { FirstName = "Andrzej", LastName = "Nowak" },
                new Friend { FirstName = "Przemek", LastName = "Stary" },
                new Friend { FirstName = "Blazej", LastName = "Hutek" },
                new Friend { FirstName = "Hubert", LastName = "Plis" }
                );
        }   
    }
}

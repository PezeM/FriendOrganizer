using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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

            context.ProgrammingLanguages.AddOrUpdate(p => p.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "F#" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Java" }
                );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(p => p.Number,
                new FriendPhoneNumber { Number = "+48 123456789", FriendId = context.Friends.First().Id }
                );

            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Watching football",
                    DateFrom = new DateTime(2018, 4, 15),
                    DateTo = new DateTime(2018, 4, 15),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f=>f.FirstName == "Andrzej" && f.LastName == "Nowak"),
                        context.Friends.Single(f=>f.FirstName == "Przemek" && f.LastName == "Stary")
                    }
                });
        }
    }
}

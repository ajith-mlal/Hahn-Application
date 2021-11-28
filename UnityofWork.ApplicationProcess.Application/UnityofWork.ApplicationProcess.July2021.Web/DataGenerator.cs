using System;
using System.Collections.Generic;
using System.Linq;
using Hahn.ApplicationProcess.July2021.Data.Data;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.July2021.Web
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DBContext(
                serviceProvider.GetRequiredService<DbContextOptions<DBContext>>()))
            {

                if (context.UserProfiles.Any())
                {
                    return;
                }
                var assets1 = new List<Data.Entities.UserAssets>();
                assets1.Add(new Data.Entities.UserAssets()
                {
                    AssetId="bitcoin"
                });
                assets1.Add(new Data.Entities.UserAssets()
                {
                    AssetId = "ethereum"
                });
                var assets2 = new List<Data.Entities.UserAssets>();
                assets2.Add(new Data.Entities.UserAssets()
                {
                    AssetId = "tether"
                });

                var address1 = new Data.Entities.UserAddress()
                {
                    AddressLine1 = "xyz",
                    AddressLine2 = "abc",
                    AddressLine3 = "123",
                    PostalCode = "245346"
                };
                var address2 = new Data.Entities.UserAddress()
                {
                    AddressLine1 = "wer",
                    AddressLine2 = "ghj",
                    AddressLine3 = "456",
                    PostalCode = "86786"
                };

                context.UserProfiles.AddRange(
                    new Data.Entities.UserProfile
                    {
                        Age = 22,
                        Email = "jondoe@email.com",
                        FirstName = "Jon",
                        LastName = "Doe",
                        UserAssets=assets1,
                        UserAddress=address1
                    },
                    new Data.Entities.UserProfile
                    {
                        Age = 33,
                        Email = "markantony@email.com",
                        FirstName = "Mark",
                        LastName = "Antony",
                        UserAssets=assets2,
                        UserAddress=address2
                    }


                    );

                context.SaveChanges();
            }
        }
    }
}

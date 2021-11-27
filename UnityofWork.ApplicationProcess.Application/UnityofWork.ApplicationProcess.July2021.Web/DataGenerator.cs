using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                context.UserProfiles.AddRange(
                    new Data.Entities.UserProfile
                    {
                        Age = 22,
                        Email = "jondoe@email.com",
                        FirstName = "Jon",
                        LastName = "Doe",
                        UserAssets=assets1
                    },
                    new Data.Entities.UserProfile
                    {
                        Age = 33,
                        Email = "markantony@email.com",
                        FirstName = "Mark",
                        LastName = "Antony",
                        UserAssets=assets2
                    }


                    );

                context.SaveChanges();
            }
        }
    }
}

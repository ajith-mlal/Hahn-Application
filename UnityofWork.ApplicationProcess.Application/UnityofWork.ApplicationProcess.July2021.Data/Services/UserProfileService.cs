using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityofWork.ApplicationProcess.July2021.Data.Data;
using UnityofWork.ApplicationProcess.July2021.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace UnityofWork.ApplicationProcess.July2021.Data.Services
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<List<UserProfile>> All()
        {
            try
            {
                return await dbSet.Include(p=>p.UserAssets).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserProfileRepository));
                return new List<UserProfile>();
            }
        }

        public override async Task<bool> Update(UserProfile entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.UserId == entity.UserId)
                                                    .FirstOrDefaultAsync();

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                existingUser.Age = entity.Age;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserProfileRepository));
                return false;
            }
        }



        public override async Task<bool> Delete(int userId)
        {
            try
            {
                var exist = await dbSet.Where(x => x.UserId == userId)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserProfileRepository));
                return false;
            }
        }
    }
}

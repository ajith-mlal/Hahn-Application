using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.July2021.Data.Data;
using Hahn.ApplicationProcess.July2021.Data.Services;

namespace Hahn.ApplicationProcess.July2021.Data.Configuration
{
    public class UnitOfWork : IUnitofWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;

        public IUserProfileRepository UserProfile { get; private set; }

        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            UserProfile = new UserProfileRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

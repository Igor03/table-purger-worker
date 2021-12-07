using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JIigor.Projects.TablePurger.Database.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace JIigor.Projects.TablePurger.Database
{
    public class PurgerService
    {
        private readonly PurgerDataContext _purgerDataContext;

        public PurgerService(PurgerDataContext purgerDataContext)
        {
            _purgerDataContext = purgerDataContext;
        }

        public async Task<int> PurgeRecordAsync(int limitInMinutes, CancellationToken cancellationToken)
        {
            limitInMinutes = limitInMinutes == default ? 2 : limitInMinutes;

            // var db = _purgerDataContext.Database.GetDbConnection().Database;

            var records =
                await _purgerDataContext.PurgeableRecords.Where(
                        p => p.CreationDate <= DateTime.Now.AddMinutes(-1 * limitInMinutes))
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

            if (!records.Any()) return 0;

            _purgerDataContext.PurgeableRecords.RemoveRange(records);
            return await _purgerDataContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

        }
    }
}

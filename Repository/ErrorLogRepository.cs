using SkepERP.Data;
using SkepERP.Interfaces;
using SkepERP.Models;

namespace SkepERP.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {

        private readonly DataContext _context;

        public ErrorLogRepository(DataContext context)
        {
            _context = context;
        }

        public void SaveErrorLog(ErrorLog errorLog)
        {
            _context.ErrorLog.Add(errorLog);
            _context.SaveChanges();
        }

        public async Task SaveErrorLogAsync(ErrorLog errorLog)
        {
            _context.ErrorLog.Add(errorLog);
            await _context.SaveChangesAsync();
        }
    }
}

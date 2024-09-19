using SkepERP.Models;

namespace SkepERP.Interfaces
{
    public interface IErrorLogRepository
    {
        public void SaveErrorLog(ErrorLog errorLog);
        Task SaveErrorLogAsync(ErrorLog log);
    }
}

using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class SettingService
    {
        Exam21JanDBContext _db { get; }

        public SettingService(Exam21JanDBContext db)
        {
            _db = db;
        }
        public async Task<Setting> GetSettingAsync() =>
            await _db.Setting.FindAsync(1);
    }
}

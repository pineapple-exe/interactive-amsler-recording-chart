using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly InteractiveAmslerRecordingChartDbContext _context;

        public SessionRepository(InteractiveAmslerRecordingChartDbContext context)
        {
            _context = context;
        }

        public void AddSession(Session session)
        {
            _context.Add(session);
            _context.SaveChanges();
        }

        public IQueryable<Session> GetPreviousSessions(string name)
        {
            return _context.Sessions.Where(s => s.Name.ToLower() == name.ToLower()).Include(s => s.Coordinates).ThenInclude(c => c.Session);
        }
    }
}

using InteractiveAmslerRecordingChart.Domain.Entities;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Domain.Repositories
{
    public interface ISessionRepository
    {
        void AddSession(Session session);
        IQueryable<Session> GetPreviousSessions(string name);
    }
}

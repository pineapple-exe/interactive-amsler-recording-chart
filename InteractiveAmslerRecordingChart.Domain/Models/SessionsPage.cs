using System.Collections.Generic;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class SessionsPage
    {
        public List<SessionOutputModel> Sessions { get; }
        public int Total { get; }

        public SessionsPage(List<SessionOutputModel> sessions, int total)
        {
            Sessions = sessions;
            Total = total;
        }
    }
}

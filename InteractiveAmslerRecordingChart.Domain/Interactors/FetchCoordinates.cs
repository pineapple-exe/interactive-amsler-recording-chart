using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Domain.Models;
using InteractiveAmslerRecordingChart.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Domain.Interactors
{
    public class FetchCoordinates
    {
        private readonly ISessionRepository _sessionRepository;

        public FetchCoordinates(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public List<CoordinateModel> FetchOldCoordinates(string name = null)
        {
            IQueryable<Session> sessions = _sessionRepository.GetPreviousSessions();

            if (name != null)
            {
                if (!_sessionRepository.GetPreviousSessions().Any(s => s.Name.ToLower() == name.ToLower()))
                    return null;
                else
                    sessions = sessions.Where(s => s.Name.ToLower() == name.ToLower());
            }

            IEnumerable<Coordinate> coordinatesDistinctAndLatest = sessions
                .SelectMany(s => s.Coordinates).ToList()
                .GroupBy(c => new { c.X, c.Y }).ToList()
                .Select(g => g.OrderBy(c => c.Session.DateTime).Last());

            List<CoordinateModel> coordinateModels = coordinatesDistinctAndLatest
                .Select(c => new CoordinateModel(c.X, c.Y, c.VisualFieldStatus)).ToList();

            return coordinateModels;
        }
    }
}

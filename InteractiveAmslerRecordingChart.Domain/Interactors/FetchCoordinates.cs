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

        public List<CoordinateModel> FetchOldCoordinates(string name)
        {
            if (!_sessionRepository.GetPreviousSessions(name).Any()) return null;

            IEnumerable<Coordinate> coordinatesDistinctAndLatest = _sessionRepository.GetPreviousSessions(name)
                .SelectMany(s => s.Coordinates).ToList()
                .GroupBy(c => new { c.X, c.Y }).ToList()
                .Select(g => g.OrderBy(c => c.Session.DateTime).Last());

            List<CoordinateModel> coordinateModels = coordinatesDistinctAndLatest
                .Select(c => new CoordinateModel(c.X, c.Y, c.VisualFieldStatus)).ToList();

            return coordinateModels;
        }
    }
}

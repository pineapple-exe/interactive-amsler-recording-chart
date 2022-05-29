using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Domain.Models;
using InteractiveAmslerRecordingChart.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Domain.Interactors
{
    public class SessionInteractor
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionInteractor(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public void AddSession(SessionInputModel sessionInputModel)
        {
            List<Coordinate> coordinates = sessionInputModel.CoordinatesWithStatus
                .Select(s => new Coordinate()
                {
                    X = s.X,
                    Y = s.Y,
                    VisualFieldStatus = s.VisualFieldStatus

                }).ToList();

            Session session = new()
            {
                Name = sessionInputModel.Name,
                DateTime = sessionInputModel.DateTime.ToLocalTime(),
                Coordinates = coordinates
            };

            _sessionRepository.AddSession(session);
        }

        public List<SessionOutputModel> FetchRecords()
        {
            IQueryable<Session> sessions = _sessionRepository.GetPreviousSessions();
            List<SessionOutputModel> sessionModels = new();

            foreach (Session session in sessions)
            {
                List<Coordinate> coordinates = session.Coordinates;
                List<CoordinateModel> coordinateModels = coordinates.Select(c => new CoordinateModel(c.X, c.Y, c.VisualFieldStatus)).ToList();

                sessionModels.Add(new SessionOutputModel(session.Id, session.Name, coordinateModels, session.DateTime));
            }

            return sessionModels;
        }
    }
}

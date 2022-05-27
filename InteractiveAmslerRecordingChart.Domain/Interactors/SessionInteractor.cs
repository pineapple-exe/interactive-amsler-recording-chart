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
                DateTime = sessionInputModel.DateTime,
                Coordinates = coordinates
            };

            _sessionRepository.AddSession(session);
        }
    }
}

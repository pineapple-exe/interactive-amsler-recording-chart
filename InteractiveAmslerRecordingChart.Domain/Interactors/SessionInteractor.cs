using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Domain.Models;
using InteractiveAmslerRecordingChart.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using static InteractiveAmslerRecordingChart.Domain.Utils;

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

        public static VisualFieldProgressionModel CalculateProgression(List<Session> personalSessions, Session session)
        {
            int improvement = 0;
            int regression = 0;

            personalSessions = personalSessions.OrderBy(s => s.DateTime).ToList();

            if (personalSessions.Count > 1 && personalSessions.IndexOf(session) > 0)
            {
                Session previousSession = personalSessions[personalSessions.IndexOf(session) - 1];
                IEnumerable<IGrouping<(int X, int Y), Coordinate>> coordinatesGroups = session.Coordinates
                                                                                    .Concat(previousSession.Coordinates)
                                                                                    .GroupBy(c => (c.X, c.Y));

                foreach (IGrouping<(int X, int Y), Coordinate> g in coordinatesGroups)
                {
                    if (g.Count() > 1)
                    {
                        if (g.First().VisualFieldStatus == VisualFieldStatus.Clear && g.Last().VisualFieldStatus == VisualFieldStatus.Deviant)
                        {
                            improvement++;
                        }
                        else if (g.First().VisualFieldStatus == VisualFieldStatus.Deviant && g.Last().VisualFieldStatus == VisualFieldStatus.Clear)
                        {
                            regression++;
                        }
                    }
                }
            }
            return new VisualFieldProgressionModel(improvement, regression);
        }

        public List<SessionOutputModel> FetchRecords()
        {
            List<Session> sessions = _sessionRepository.GetSessions().ToList();
            List<SessionOutputModel> sessionModels = new();

            foreach (Session session in sessions)
            {
                List<Coordinate> coordinates = session.Coordinates;
                List<CoordinateModel> coordinateModels = coordinates.Select(c => new CoordinateModel(c.X, c.Y, c.VisualFieldStatus)).ToList();

                List<Session> personalSessions = sessions.Where(s => s.Name.ToLower() == session.Name.ToLower()).ToList();
                VisualFieldProgressionModel progressionModel = CalculateProgression(personalSessions, session);

                sessionModels.Add(new SessionOutputModel(session.Id, session.Name, coordinateModels, session.DateTime, progressionModel));
            }
            return sessionModels;
        }
    }
}

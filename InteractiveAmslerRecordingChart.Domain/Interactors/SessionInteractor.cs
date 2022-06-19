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

        private static List<Session> ApplyFilter(IQueryable<Session> sessionsPrimordialSoup, int pageIndex, int size, string name)
        {
            List<Session> sessions = new();

            if (string.IsNullOrEmpty(name))
            {
                sessions = sessionsPrimordialSoup.Skip(pageIndex * size).Take(size).ToList();
            }
            else
            {
                sessions.AddRange(sessionsPrimordialSoup.Where(s => s.Name.ToLower() == name.ToLower()));
                sessions = sessions.Skip(pageIndex * size).Take(size).ToList();
            }

            return sessions;
        }

        private static List<SessionOutputModel> MapSessions(List<Session> sessions)
        {
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

        public SessionsPage FetchSessions(int pageIndex, int size, string name)
        {
            IQueryable<Session> sessionsPrimordialSoup = _sessionRepository.GetSessions().OrderBy(s => s.Id);

            List<Session> sessions = ApplyFilter(sessionsPrimordialSoup, pageIndex, size, name);
            List<SessionOutputModel> sessionModels = MapSessions(sessions);

            return new SessionsPage(sessionModels, _sessionRepository.GetSessions().Count());
        }

        public SessionOutputModel FetchSession(int id)
        {
            return MapSessions(_sessionRepository.GetSessions().ToList()).FirstOrDefault(r => r.Id == id);
        }

        public int? FetchComparisonId(int currentId, TimeTravel timeTravel)
        {
            Session currentSession = _sessionRepository.GetSessions().Where(r => r.Id == currentId).Single();
            var personsSessions = _sessionRepository.GetSessions()
                                                    .Where(r => r.Name == currentSession.Name) //add filter for coordinate-sameness!
                                                    .OrderBy(s => s.DateTime).ToList();

            int indexOfCurrentSession = personsSessions.IndexOf(currentSession);
            int timeTravelLeap = 1;
            int timeTravelDirection = timeTravel == TimeTravel.Past ? timeTravelLeap * -1 : timeTravelLeap * 1;

            Session comparisonSessOrNull = personsSessions.SingleOrDefault(s => personsSessions.IndexOf(s) == indexOfCurrentSession + timeTravelDirection);

            return comparisonSessOrNull?.Id;
        }
    }
}

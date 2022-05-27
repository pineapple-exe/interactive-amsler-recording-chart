using InteractiveAmslerRecordingChart.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Domain.Repositories
{
    public interface ICoordinateRepository
    {
        void AddCoordinates(IEnumerable<Coordinate> coordinates);
        IQueryable<Coordinate> GetCoordinateRecord(IQueryable<int> sessionIds);
    }
}

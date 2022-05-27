using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveAmslerRecordingChart.Data.Repositories
{
    public class CoordinateRepository : ICoordinateRepository
    {
        private readonly InteractiveAmslerRecordingChartDbContext _context;

        public CoordinateRepository(InteractiveAmslerRecordingChartDbContext context)
        {
            _context = context;
        }

        public void AddCoordinates(IEnumerable<Coordinate> coordinates)
        {
            _context.AddRange(coordinates);
        }

        public IQueryable<Coordinate> GetCoordinateRecord(IQueryable<int> sessionIds)
        {
            return _context.Coordinates.Where(c => sessionIds.Contains(c.SessionId));
        }
    }
}

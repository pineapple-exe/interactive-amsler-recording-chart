using System;
using System.Collections.Generic;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class SessionOutputModel
    {
        public int Id { get; }
        public string Name { get; }
        public List<CoordinateModel> CoordinatesWithStatus { get; }
        public DateTimeOffset DateTime { get; }

        public SessionOutputModel(int id, string name, List<CoordinateModel> coordinatesWithStatus, DateTimeOffset dateTime)
        {
            Id = id;
            Name = name;
            CoordinatesWithStatus = coordinatesWithStatus;
            DateTime = dateTime;
        }
    }
}

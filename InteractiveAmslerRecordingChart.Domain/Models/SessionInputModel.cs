using System;
using System.Collections.Generic;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class SessionInputModel
    {
        public string Name { get; }
        public List<CoordinateModel> CoordinatesWithStatus { get; }
        public DateTimeOffset DateTime { get; }

        public SessionInputModel(string name, List<CoordinateModel> coordinatesWithStatus, DateTimeOffset dateTime)
        {
            Name = name;
            CoordinatesWithStatus = coordinatesWithStatus;
            DateTime = dateTime;
        }
    }
}

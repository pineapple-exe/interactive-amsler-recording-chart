using System;
using System.Collections.Generic;
using static InteractiveAmslerRecordingChart.Domain.Utils;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class SessionInputModel
    {
        public string Name { get; }
        public List<CoordinateModel> CoordinatesWithStatus { get; }
        public DateTime DateTime { get; }

        public SessionInputModel(string name, List<CoordinateModel> coordinatesWithStatus, DateTime dateTime)
        {
            Name = name;
            CoordinatesWithStatus = coordinatesWithStatus;
            DateTime = dateTime;
        }
    }
}

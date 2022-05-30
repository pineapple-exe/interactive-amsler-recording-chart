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
        public VisualFieldProgressionModel VisualFieldProgression { get; }

        public SessionOutputModel(int id, string name, List<CoordinateModel> coordinatesWithStatus, DateTimeOffset dateTime, VisualFieldProgressionModel visualFieldProgression)
        {
            Id = id;
            Name = name;
            CoordinatesWithStatus = coordinatesWithStatus;
            DateTime = dateTime;
            VisualFieldProgression = visualFieldProgression;
        }
    }
}

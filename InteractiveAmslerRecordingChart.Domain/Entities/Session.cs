using System;
using System.Collections.Generic;

namespace InteractiveAmslerRecordingChart.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public List<Coordinate> Coordinates { get; set; }
    }
}

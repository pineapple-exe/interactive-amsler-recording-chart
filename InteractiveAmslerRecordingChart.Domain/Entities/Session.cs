using System;
using System.Collections.Generic;

namespace InteractiveAmslerRecordingChart.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}

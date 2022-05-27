using static InteractiveAmslerRecordingChart.Domain.Utils;

namespace InteractiveAmslerRecordingChart.Domain.Entities
{
    public class Coordinate
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public VisualFieldStatus VisualFieldStatus { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

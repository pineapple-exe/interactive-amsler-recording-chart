using static InteractiveAmslerRecordingChart.Domain.Utils;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class CoordinateModel
    {
        public int X { get; }
        public int Y { get; }
        public VisualFieldStatus VisualFieldStatus { get; }

        public CoordinateModel(int x, int y, VisualFieldStatus visualFieldStatus)
        {
            X = x;
            Y = y;
            VisualFieldStatus = visualFieldStatus;
        }
    }
}

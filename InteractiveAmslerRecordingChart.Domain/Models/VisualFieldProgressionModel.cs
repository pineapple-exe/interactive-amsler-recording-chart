using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAmslerRecordingChart.Domain.Models
{
    public class VisualFieldProgressionModel
    {
        public int Improvement { get; }
        public int Regression { get; }

        public VisualFieldProgressionModel(int improvement, int regression)
        {
            Improvement = improvement;
            Regression = regression;
        }
    }
}

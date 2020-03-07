using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace GeometryPainting
{
    //Напишите здесь код, который заставит работать методы segment.GetColor и segment.SetColor
    public static class SegmentExtensions
    {
        private readonly static Dictionary<Segment, Color> allColors =
            new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment)
        {
            return allColors.ContainsKey(segment) ? allColors[segment] : Color.Black;
        }

        public static void SetColor(this Segment segment, Color color)
        {
            allColors[segment] = color;
        }
    }
}
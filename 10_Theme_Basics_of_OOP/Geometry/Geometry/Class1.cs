using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector secondVector)
        {
            return Geometry.Add(this, secondVector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt((segment.End.X - segment.Begin.X) *
                             (segment.End.X - segment.Begin.X) +
                             (segment.End.Y - segment.Begin.Y) *
                             (segment.End.Y - segment.Begin.Y));
        }

        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            return new Vector() { X = firstVector.X + secondVector.X, Y = firstVector.Y + secondVector.Y };
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            return Math.Abs((GetLength(new Segment() { Begin = segment.Begin, End = vector }) +
                GetLength(new Segment() { Begin = vector, End = segment.End })) -
                GetLength(segment)) < 0.000000001;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Shapes;

namespace Canvas.Helpers
{
    public class MathHelper
    {
        public static double DistanceFromPointA_B(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintParody.Models
{
    /// <summary>
    /// ArrayOfPoints class
    /// </summary>
    public class ArrayOfPoints
    {
        // Current dot in array
        int currentDot = 0;
        // Array of points (field)
        Point[] points;

        // Constructor (setting up an size of array)
        public ArrayOfPoints(int arraySize)
        {
            // If we are receiving 0 array size
            if (arraySize <= 0)
            {
                arraySize = 2;
            }
            // Filling our array
            points = new Point[arraySize];
        }

        // Creating our first point
        public void AddPointsToArray(int x, int y)
        {
            // If we are exceeding an array limit
            if (currentDot >= points.Length)
            {
                currentDot = 0;
            }
            // Adding new point
            points[currentDot] = new Point(x, y);
            currentDot++;
        }

        public void ResetPoints()
        {
            currentDot = 0;
        }

        public int GetAmountOfPoints()
        {
            return currentDot;
        }

        // We can draw using this method
        public Point[] ReturnPoints()
        {
            return points;
        }
    }
}

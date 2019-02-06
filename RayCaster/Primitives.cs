using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    public class Rect
    {
        private Rect()
        {

        }

        public Rect(float side) : this(side, side)
        {

        }

        public Rect(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public Rect(double width, double height) : this((float)width, (float)height)
        {

        }

        public Rect(Windows.Foundation.Rect rect) : this(rect.Width, rect.Height)
        {

        }

        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    public class Rectangle
    {
        public Rectangle(float side) : this(side, side)
        {

        }

        public Rectangle(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public Rectangle(double width, double height) : this((float)width, (float)height)
        {

        }

        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}

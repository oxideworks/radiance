using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    public class Rectangle : Polymer
    {
        public Rectangle(float side) : this(side, side)
        {

        }

        public Rectangle(float width, float height) : base(new[] { new Vector2d(0, 0), new Vector2d(width, 0), new Vector2d(width, height), new Vector2d(0, height) })
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

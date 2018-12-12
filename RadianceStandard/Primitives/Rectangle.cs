using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.Primitives
{
    public class Rectangle : Polymer
    {
        public Rectangle(float side)
            : this(side, side)
        {

        }

        public Rectangle(float width, float height)
            : base(VectorForRectangleCreator(width, height))
        {
            Width = width;
            Height = height;
        }

        public Rectangle(double width, double height)
            : this((float)width, (float)height)
        {

        }

        #region props

        public float Width { get; private set; }

        public float Height { get; private set; }

        #endregion

        #region helpers

        private static Vector[] VectorForRectangleCreator(float width, float height)
        {
            return new[] { new Vector(0, 0), new Vector(width, 0), new Vector(width, height), new Vector(0, height) };
        }

        #endregion

    }
}

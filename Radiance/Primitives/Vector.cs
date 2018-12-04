using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.Primitives
{
    /// <summary>
    /// 2D vector
    /// </summary>
    public class Vector
    {
        #region ctors

        public Vector(float a) : this(a, a)
        {

        }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region props

        public float X { get; }

        public float Y { get; }

        public float LengthSquared
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(LengthSquared);
            }
        }

        #endregion

        #region methods

        public Vector Normalize()
        {
            return this / Length;
        }

        #endregion

        #region explicit operations

        public static Vector operator /(Vector vector, float a)
        {
            return new Vector(vector.X / a, vector.Y / a);
        }

        public static Vector operator *(Vector vector, float a)
        {
            return new Vector(vector.X * a, vector.Y * a);
        }

        public static Vector operator *(float a, Vector vector)
        {
            return vector * a;
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return vector1 + vector2 * (-1);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RadianceStandard.Primitives
{
    public class Ray
    {
        public Ray(Vector originPoint, Vector guidingPoint)
        {
            Origin = originPoint;
            guide = guidingPoint;
        }

        private readonly Vector guide;

        public Vector Origin { get; }
        public Vector Direction { get => guide - Origin; }
    }
}

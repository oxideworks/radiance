using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Primitives
{
    public class Triangulation
    {
        #region Ctors
        public Triangulation()
        {
            cache = new Triangle[cacheSize, cacheSize];
            triangles = new List<Triangle>();
            points = new Polymer();
        }

        public Triangulation(IEnumerable<Triangle> triangles)
            : this()
        {
            foreach (var triangle in triangles)
                Add(triangle);
        }
        #endregion

        #region fields
        private int cacheSize = 2;
        private Triangle[,] cache;
        private readonly List<Triangle> triangles;
        private readonly Polymer points;
        #endregion

        #region Props

        #endregion

        #region Methods
        public Triangle Find(Vector point)
        {
            var close = cache[(int)(point.X / cacheSize), (int)(point.Y / cacheSize)];
            return close.FindFriend(point);
        }

        public void Add(Triangle triangle)
        {
            triangles.Add(triangle);
            points.AddRange(triangle.Polymer.Except(points));
#warning Add triangle to cache here.
            TryResizeCache();
        }
        #endregion

        #region privates
        private void TryResizeCache()
        {
            if (points.Count > cache.Length * GlobalConsts.CACHE_EXPANSION_CONSTANT)
                ResizeCache();
        }

        private void ResizeCache()
        {
            Triangle[,] newCache = new Triangle[cacheSize * 2, cacheSize * 2];
            for (int i = 0; i < cacheSize; i++)
                for (int j = 0; j < cacheSize; j++)
                {
                    newCache[i * 2, j * 2] = cache[i, j];
                    newCache[i * 2, j * 2 + 1] = cache[i, j];
                    newCache[i * 2 + 1, j * 2] = cache[i, j];
                    newCache[i * 2 + 1, j * 2 + 1] = cache[i, j];
                }
            cache = newCache;
            cacheSize *= 2;
            TryResizeCache();
        }
        #endregion
    }
}

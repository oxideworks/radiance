using System;
using System.Collections.Generic;

namespace RadianceStandard.Primitives
{
    public class Triangulation
    {
        #region Ctors
        public Triangulation()
            : this(new Triangle[0])
        {
            cache = new Triangle[cacheSize, cacheSize];
        }

        public Triangulation(IEnumerable<Triangle> triangles)
        {
            this.triangles = new List<Triangle>(triangles);
            //cache = new Triangle[cacheSize, cacheSize];
            TryResizeCache();
        }
        #endregion

        #region fields
        private int cacheSize = 2;
        private Triangle[,] cache;
        private List<Triangle> triangles;
        #endregion

        #region Props

        #endregion

        #region Methods
        public Triangle Find(Vector point)
        {
            var close = cache[(int)point.X / cacheSize, (int)point.Y / cacheSize];
            // close.FindFriend(point)...
            throw new NotImplementedException();
        }

        public void Add(Triangle triangle)
        {
            // Add...
            TryResizeCache();
            throw new NotImplementedException();
        }

        // Do we need to remove from cache?
        // NO.
        //public void Remove(Triangle triangle)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        #region privates
        private void TryResizeCache()
        {
            // Числа точек
            if (triangles.Count > cache.Length * GlobalConsts.CACHE_EXPANSION_CONSTANT)
                ResizeCache();
        }

        private void ResizeCache()
        {
            var newCache = new Triangle[cacheSize * 2, cacheSize * 2];
            // do resising...
            TryResizeCache();
        }
        #endregion
    }
}

using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Primitives
{
    public class Triangulation
    {
        #region Ctors
        private Triangulation()
        {
            cache = new Triangle[cacheSize, cacheSize];
            triangles = new List<Triangle>();
            points = new Polymer();
        }

        public Triangulation(IHardenedPolymer polymer)
            : this()
        {
            var huller = new ConvexHuller();
            var hull = huller.ComputeConvexHull(polymer);
            FillOuterHull(hull);
            var innerPoints = new Polymer(polymer.Except(hull));
#warning Add inner points here.
            FillInnerPoints(innerPoints);
            throw new NotImplementedException();
        }

        private void FillOuterHull(IHardenedPolymer hull)
        {
            var origin = hull.First();
            var triangles = new List<Triangle>();
            for (int i = 1; i < hull.Count - 1; i++)
                triangles.Add(new Triangle(origin, hull[i], hull[i + 1]));
            for (int i = 0; i < triangles.Count; i++)
            {
                var current = triangles[i];
                if (i - 1 >= 0)
                    current.Neighbours.Add(triangles[i - 1]);
                if (i + 1 < triangles.Count)
                    current.Neighbours.Add(triangles[i + 1]);
            }
#warning Try Flip foreach here.
        }

        private void FillInnerPoints(IHardenedPolymer points)
        {

        }
        #endregion

        #region fields
        private int cacheSize = 1;
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
        #endregion

        #region privates
        private void Add(Vector point)
        {
            var holder = Find(point);
            var newTriangles = new List<Triangle>();
            for (int i = 0, j = i; i < 3; j = i++)
                newTriangles.Add(new Triangle(point, holder.Polymer[i], holder.Polymer[j]));
#warning Add Neighbours to newTriangles here.
            foreach (var newTriangle in newTriangles)
                Add(newTriangle);
        }

        private void Add(Triangle triangle)
        {
            triangles.Add(triangle);
            points.AddRange(triangle.Polymer.Except(points));
#warning Add triangle to cache here.
            TryResizeCache();
        }

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

        private (Triangle C, Triangle D) Flip(Triangle A, Triangle B)
        {
            var mutual = A.Polymer.Intersect(B.Polymer).ToList();
            if (mutual.Count != 2)
                throw new Exception("This triangles don`t constitute quadrilateral!");
            var union = A.Polymer.Union(B.Polymer).ToList();

            Vector m0 = mutual[0];
            Vector m1 = mutual[1];
            var c = new Polymer(union.Except(new[] { m0 }));
            var d = new Polymer(union.Except(new[] { m1 }));
            var C = new Triangle(c);
            var D = new Triangle(d);

            var allNeighbours = A.Neighbours.Union(B.Neighbours).Except(new[] { A, B }).ToList();
            List<Triangle> findNeighbours(Func<Vector, bool> func)
                => allNeighbours.Where(n => n.Polymer.Any(func)).ToList();
            C.Neighbours = findNeighbours(p => p == m1);
            D.Neighbours = findNeighbours(p => p == m0);

            void handleNeighbours(Triangle @base, Triangle toAdd)
            {
                @base.Neighbours.Remove(A);
                @base.Neighbours.Remove(B);
                @base.Neighbours.Add(toAdd);
            }

            C.Neighbours.ForEach(x => handleNeighbours(x, C));
            D.Neighbours.ForEach(x => handleNeighbours(x, D));

            return (C, D);
        }

        private bool DelaunayCondition(Triangle triangle, Vector point)
        {
            return (triangle.Center - point).LengthSquared >= triangle.RadiusSquared;
        }

        private bool TryFlip(Triangle A, Triangle B, out (Triangle C, Triangle D) bundle)
        {
            bool tryFlip(Triangle a, Triangle b, out (Triangle C, Triangle D) pack)
            {
                foreach (var node in a.Polymer)
                {
                    if (!DelaunayCondition(b, node))
                    {
                        pack = Flip(a, b);
                        return true;
                    }
                }
                pack = (null, null);
                return false;
            }

            return tryFlip(A, B, out bundle) || tryFlip(B, A, out bundle);
        }
        // скорее всего он и станет TryFlip
        private bool FlipIfPossible(Triangle a, Triangle b, out (Triangle C, Triangle D) pack)
        {
            foreach (var node in a.Polymer)
            {
                if (!DelaunayCondition(b, node))
                {
                    pack = Flip(a, b);
                    return true;
                }
            }
            pack = (null, null);
            return false;
        }

        // Вроде как для такого названия один параметр логичен.
        private void Triangulate(Triangle A)
        {
            // Перебираем всех соседей
            foreach (Triangle triangle in A.Neighbours)
            {
                // Если флипнулось, то запускаем рекурсию для новых треугольников
                if (FlipIfPossible(A, triangle, out (Triangle C, Triangle D) res))
                {
                    Triangulate(res.C);
                    Triangulate(res.D);
                }
                // Если не флипнулось, то запускаем рекурсию для того же соседа, ведь
                // если одна из окруженостей правильно, не гарантирует правильность второй.
                // (если все ок, то эта рекурсия прервется сразу же, когда дойдет до нашего А, а если нет, то пойдет по всем
                // и у меня есть предположение что выполняться это все будет вечность).ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ
                else
                {
                    Triangulate(triangle);
                }
            }
        }
        #endregion
    }
}

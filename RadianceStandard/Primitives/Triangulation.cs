﻿using RadianceStandard.IRender;
using RadianceStandard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
            IHardenedPolymer hull = MakeHull(polymer);
            FillOuterHull(hull);
            IHardenedPolymer innerPoints = new Polymer(polymer.Except(hull));
#warning Add inner points here.
            FillInnerPoints(innerPoints);
            throw new NotImplementedException();
        }

        private IStaticRenderer staticRenderer;
        public Triangulation(IHardenedPolymer polymer, IDynamicRenderer renderer)
            : this()
        {
            // do same as Triangulation(IHardenedPolymer polymer)
            // but render steps

            staticRenderer = new StaticRenderer(renderer);
            staticRenderer.RenderText("Hello from hell.", new Vector(400));

            IHardenedPolymer hull = MakeHull(polymer);
#warning hull.ToSegments()
            staticRenderer.RenderSegments(hull.ToSegments(), "#ffff6b81");
            var hullString = hull.ToPythonList();
            var polymerString = polymer.ToPythonList();
            FillOuterHull(hull); // hull.Count = 30; polymer.Count = 32. Вопрос?
            DrawCurrentTriangulationState();

            //staticRenderer.
            //staticRenderer.render

        }

        private int counter = 0;
        private void DrawCurrentTriangulationState()
        {
            DumpToFile(counter.ToString());
            counter++;
            foreach (var triangle in triangles)
                staticRenderer.RenderSegments(triangle.Polymer.ToSegments(), "#ff7bed9f");
        }

        private static IHardenedPolymer MakeHull(IHardenedPolymer polymer)
        {
            var huller = new ConvexHuller();
            var hull = huller.ComputeConvexHull(polymer);
            return hull;
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
            // До этого момента все ОК.
#warning Try Flip foreach here.
            this.triangles.AddRange(triangles);

            // Запускаем Делоне
            foreach (var triangle in this.triangles.ToList()) // тут меняем лист по которому идем.
                if (this.triangles.Contains(triangle)) // Contains работает неправильно
                    DelaunayFrom(triangle);
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

        public async void DumpToFile(string filename)
        {
#warning UWP Сосать!
            var dir = $"Triangulation of {triangles.Count}";
            //if (!Directory.Exists(dir))
            //    Directory.CreateDirectory(dir);

            //using (var writer = new StreamWriter($"{dir}/{filename}.txt"))
            //    foreach (var triangle in triangles)
            //        writer.WriteLine(triangle.Polymer.ToPythonList());

            //Windows.Storage.StorageFolder rootFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder subFolder = await rootFolder.CreateFolderAsync(dir);
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
                throw new Exception("This triangles don't constitute quadrilateral!");
            var union = A.Polymer.Union(B.Polymer).ToList();
#warning Мы изменяем лист по которому проходим
            Vector m0 = mutual[0];
            Vector m1 = mutual[1];
            var c = new Polymer(union.Except(new[] { m0 }));
            var d = new Polymer(union.Except(new[] { m1 }));
            var C = new Triangle(c);
            var D = new Triangle(d);

            // До сюда все ОК.
            // Убрать старые
            triangles.Add(C);
            triangles.Add(D);

            // Добавить новые
            triangles.Remove(A);
            triangles.Remove(B);

            // Добавить новым соседей
            var allNeighbours = A.Neighbours.Union(B.Neighbours).Except(new[] { A, B }).ToList();
            List<Triangle> findNeighbours(Func<Vector, bool> func)
                => allNeighbours.Where(n => n.Polymer.Any(func)).ToList();
            C.Neighbours = findNeighbours(p => p == m1);
            D.Neighbours = findNeighbours(p => p == m0);

            // Обработать соседей
            void handleNeighbours(Triangle @base, Triangle toAdd)
            {
                @base.Neighbours.Remove(A);
                @base.Neighbours.Remove(B);
                @base.Neighbours.Add(toAdd);
            }

            C.Neighbours.ForEach(x => handleNeighbours(x, C));
            D.Neighbours.ForEach(x => handleNeighbours(x, D));

            // Добавить C к D и D к C
            C.Neighbours.Add(D);
            D.Neighbours.Add(C);

            return (C, D);
        }

#warning Rounding values here!
        private bool DelaunayCondition(Triangle triangle, Vector point)
        {
            //return (triangle.CircleCenter - point).LengthSquared >= triangle.CircleRadiusSquared;
            //return Math.Floor((triangle.CircleCenter - point).LengthSquared) >= Math.Floor(triangle.CircleRadiusSquared);
            return Math.Floor((triangle.CircleCenter - point).LengthSquared) >= Math.Floor(triangle.CircleRadiusSquared) - 1;
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
        private void DelaunayFrom(Triangle triangle)//, bool flag = true)
        {
            // Перебираем всех соседей
            foreach (Triangle neighbour in triangle.Neighbours)
            {
                DrawCurrentTriangulationState();
                //Thread.Sleep(1000);
                // Если флипнулось, то запускаем рекурсию для новых треугольников
                if (FlipIfPossible(triangle, neighbour, out (Triangle C, Triangle D) res))
                {
                    DelaunayFrom(res.C);
                    DelaunayFrom(res.D);
                }
                // Если не флипнулось, то запускаем рекурсию для того же соседа, ведь
                // если одна из окруженостей правильно, не гарантирует правильность второй.
                // (если все ок, то эта рекурсия прервется сразу же, когда дойдет до нашего 
                // А, а если нет, то пойдет по всем
                // и у меня есть предположение что выполняться это все будет вечность).ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ

#warning Она точно прервется когда все ок? Что значит все ок? Тут зациклилось.
                // Мы ведь уже проходимся по всем треугольникам
                //else
                //{
                //    ////if (flag)
                //    ////{
                //    DelaunayFrom(neighbour); //, flag = false);
                //    ////}
                //}
            }
        }
        #endregion
    }
}

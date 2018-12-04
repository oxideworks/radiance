using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    public class PointsSorter
    {
        public Vector2[] Sort(Vector2[] points, Vector2 center)
        {
            //var upper = points.OrderBy(x => x.X).ThenBy(x => x.Y).First();
            //var ordered = points.OrderByDescending(x => CalcAngle(upper, x));

            //var pts = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            //var o = pts.First();

            //var py = points.OrderBy(p => p.Y);
            //var y0 = py.First().Y;
            //var pys = py.Where(p => p.Y == y0).ToList();
            //var o = pys.OrderBy(p => p.X).First();

            //var pts = points.ToList();
            //pts.Remove(o);
            //var o = points.Aggregate((acc, p) => acc += p) / points.Length;


            //var center = points.Aggregate((acc, p) => acc += p) / points.Length;
            var top = new List<Vector2>();
            var bot = new List<Vector2>();
            foreach (var p in points)
            {
                var op = p - center;
                if (op.Y >= 0)
                    top.Add(op);
                else
                    bot.Add(op);
            }
            float cos(Vector2 p) => Math.Sign(p.X) * p.X * p.X / p.LengthSquared();
            top = top.OrderByDescending(cos).ToList();
            bot = bot.OrderBy(cos).ToList();

            var sorted = new List<Vector2>();
            //sorted.Add(center);
            foreach (var p in top)
                sorted.Add(p + center);
            foreach (var p in bot)
                sorted.Add(p + center);

            return sorted.ToArray();

            //var ordered = points.OrderByDescending(x => Math.Atan2(x.X, x.Y)).ToArray();
            //return ordered;


            //var ordered = points
            //    //.Skip(1)
            //    .OrderByDescending(p => 1 / Math.Tan((p.Y - o.Y) / (p.X - o.X))).ToList();
            //ordered.Insert(0, o);
            //return ordered.ToArray();
        }

        //private double Fraction(double x, double y)
        //{

        //}

        //public Vector2[] Sort(Vector2[] points, Vector2 origin)
        //{
        //    //var ordered = points.OrderByDescending(x => CalcAngle(upper, x));


        //    //var ordered = points.OrderByDescending(p => Math.Atan2(p.Y - origin.Y, p.X - origin.X)).ToArray();
        //    //return ordered;

        //    //var odererd = points.OrderBy(p=>Math.)
        //}

        //private double CalcAngle(Vector2 o, Vector2 p)
        //{
        //    var kek = (o.Y - p.Y) / (p.X - o.X);
        //    var angle = Math.Atan(kek);
        //    //var angle = Math.Atan2(p.Y - o.Y, p.X - o.X);
        //    if (angle < 0)
        //        angle += Math.PI * 2;
        //    return angle;
        //}
    }
}

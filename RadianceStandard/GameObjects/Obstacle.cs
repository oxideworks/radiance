using RadianceStandard.Exceptions;
using RadianceStandard.Primitives;
using System;
using System.Collections.Generic;

namespace RadianceStandard.GameObjects
{
    public class Obstacle : IObstacle
    {
        #region Ctors
        public Obstacle(Polymer polymer)
        {
            if (polymer.Count < 3) throw new InvalidNumberOfNodesException();
            this.polymer = polymer;
            segments = PairNodes();
        }

        private List<Segment> PairNodes()
        {
            var segments = new List<Segment>();
            for (int i = 0, j = Polymer.Count - 1; i < Polymer.Count; j = i++)
                segments.Add(new Segment(polymer[i], polymer[j]));
            return segments;
        }
        #endregion

        #region Properties
        public IHardenedPolymer Polymer { get => polymer; }
        public IReadOnlyList<Segment> Segments { get => segments; }
        #endregion

        #region Methods
        public bool Contains(Vector point)
        {
            bool flag = false;
            foreach (var segment in segments)
            {
                CrossingState state = CrossDown(point, segment);
                if (state == CrossingState.Break) return true;
                flag ^= (state == CrossingState.True) ? true : false;
            }
            return flag;
        }

        /// <summary>
        /// Partially contains obstacle
        /// </summary>
        /// <param name="obstacle"></param>
        /// <returns></returns>
        public bool Contains(IObstacle obstacle)
        {
            foreach (var node in obstacle.Polymer)
                if (Contains(node)) return true;
            return false;
        }

        public bool Intersects(IObstacle obstacle)
        {
            foreach (var masterSegment in Segments)
                foreach (var visitorSegment in obstacle.Segments)
                    if (masterSegment.TryFindCrossingPoint(visitorSegment, out Vector _))
                        return true;
            return false;
        }
        #endregion

        #region privates
        private enum CrossingState : sbyte
        {
            False = 0,
            True = 1,
            Break = -1
        }

        private readonly Polymer polymer;
        private readonly List<Segment> segments;

        private CrossingState CrossDown(Vector point, Segment segment)
        {
            var (x, y) = (point.X, point.Y);
            var (x1, y1) = (segment.A.X, segment.A.Y);
            var (x2, y2) = (segment.B.X, segment.B.Y);
            if (x1 == x && x2 == x && !IsNotOnSegment(y, y1, y2)) return CrossingState.Break;
            if (x1 == x2) return CrossingState.False;
            var ycross = (x - x1) * (y2 - y1) / (x2 - x1) + y1;
            if (Math.Abs(ycross - y) < GlobalConsts.EPSILON && !IsNotOnSegment(x, x1, x2)) return CrossingState.Break;
            if (IsNotOnSegment(x, x1, x2) || IsOnRightOfSegment(x, x1, x2)) return CrossingState.False;
            if (ycross > y) return CrossingState.False;
            return CrossingState.True;
        }

        private bool IsNotOnSegment(float coorToCheck, float coor1, float coor2)
        {
            return coorToCheck < Math.Min(coor1, coor2) || coorToCheck > Math.Max(coor1, coor2);
        }

        private bool IsOnLeftOfSegment(float coorToCheck, float coor1, float coor2)
        {
            return coorToCheck == Math.Min(coor1, coor2);
        }

        private bool IsOnRightOfSegment(float coorToCheck, float coor1, float coor2)
        {
            return coorToCheck == Math.Max(coor1, coor2);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Primitives
{
    public interface IHardenedPolymer : IReadOnlyList<Vector>
    {
        List<Segment> ToSegments();
        bool ContainsPoint(Vector point);
        string ToPythonList();
    }

    public class Polymer : List<Vector>, IHardenedPolymer
    {
        #region Ctors
        public Polymer()
        {

        }

        public Polymer(IEnumerable<Vector> nodes) : base(nodes)
        {

        }

        public Polymer(IHardenedPolymer polymer) : this((IEnumerable<Vector>)polymer)
        {

        }
        #endregion

        #region Methods
        public bool ContainsPoint(Vector point)
        {
            bool flag = false;
            foreach (var segment in ToSegments())
            {
                CrossingState state = CrossDown(point, segment);
                if (state == CrossingState.Break) return true;
                flag ^= (state == CrossingState.True) ? true : false;
            }
            return flag;
        }

#warning Нужно ToSegments() -> в immutable Segments
        public List<Segment> ToSegments()
        {
            var segments = new List<Segment>();
            for (int i = 0, j = Count - 1; i < Count; j = i++)
                segments.Add(new Segment(this[i], this[j]));
            return segments;
        }

        public string ToPythonList()
        {
            return $"[{string.Join(", ", this.Select(x => x.ToString().Replace(";", ",")))}]";
        }
        #endregion

        #region privates
        private enum CrossingState : sbyte
        {
            False = 0,
            True = 1,
            Break = -1
        }

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

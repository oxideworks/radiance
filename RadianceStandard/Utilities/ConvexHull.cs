using RadianceStandard.Exceptions;
using RadianceStandard.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace RadianceStandard.Utilities
{
    public class ConvexHull
    {
        #region Ctors
        public ConvexHull(IHardenedPolymer polymer)
        {
            if (polymer.Count < 3) throw new InvalidNumberOfNodesException("Ну вы децыбел.");
            Hull = GrahamScan(polymer);
        }
        #endregion

        #region Props
        public IHardenedPolymer Hull;
        #endregion

        #region privates
        private IHardenedPolymer GrahamScan(IHardenedPolymer polymer)
        {
            var angleSorter = new PolymerSorter();
            var origin = polymer.OrderBy(p => p.X).ThenBy(p => p.Y).First();
            var ordered = angleSorter.Sort(polymer, origin);
            var stack = new Stack<Vector>();
            stack.Push(ordered[0]);
            stack.Push(ordered[1]);
            stack.Push(ordered[2]);
            for (int i = 3; i < ordered.Count; i++)
            {
                while (!CCW(stack.Skip(1).First(), stack.Peek(), ordered[i]))
                    stack.Pop();
                stack.Push(ordered[i]);
            }

            var hull = new Polymer(stack);
            return hull;
        }

        private bool CCW(Vector p0, Vector p1, Vector pi)
        {
            var (x1, y1) = p0.ToTuple();
            var (x2, y2) = p1.ToTuple();
            var (x3, y3) = pi.ToTuple();
            var loc = (y2 - y1) * (x3 - x2) - (y3 * y2) * (x2 - x1);
            if (loc < 0) return true;
            else return false;
        }
        #endregion
    }
}

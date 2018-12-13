using RadianceStandard.Primitives;
using System;
using System.Linq;
using Windows.System;

namespace RadianceStandard.Utilities
{
    public class PolymerSorter
    {
        public Polymer Sort(IHardenedPolymer polymer, Vector origin)
        {
            var top = new Polymer();
            var bot = new Polymer();

            foreach (var node in polymer)
            {
                var op = node - origin;
                if (op.Y >= 0)
                    top.Add(op);
                else
                    bot.Add(op);
            }

            float cos(Vector point) => Math.Sign(point.X) * point.X * point.X / point.LengthSquared;
            top = new Polymer(top.OrderByDescending(cos));
            bot = new Polymer(bot.OrderBy(cos));

            var sorted = new Polymer();
            foreach (var p in top)
                sorted.Add(p + origin);
            foreach (var p in bot)
                sorted.Add(p + origin);

            return sorted;
        }
    }
}

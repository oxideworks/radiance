namespace RadianceStandard.Primitives
{
    public class Rectangle
    {
        #region Ctors
        public Rectangle(float side)
            : this(side, side)
        {

        }

        public Rectangle(float width, float height)
            : this(new Polymer(VectorForRectangleCreator(width, height)))
        {

        }

        public Rectangle(double width, double height)
            : this((float)width, (float)height)
        {

        }

        public Rectangle(IHardenedPolymer polymer)
        {
            Polymer = polymer;
        }
        #endregion

        #region Props
        public float Width { get => (Polymer[0] - Polymer[1]).Length; }
        public float Height { get => (Polymer[2] - Polymer[1]).Length; }
        public IHardenedPolymer Polymer { get; }
        #endregion

        #region privates
        private static Vector[] VectorForRectangleCreator(float width, float height)
        {
            return new[] { new Vector(0, 0), new Vector(width, 0),
                           new Vector(width, height), new Vector(0, height)
            };
        }
        #endregion

    }
}

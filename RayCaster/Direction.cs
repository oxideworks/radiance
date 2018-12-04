using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    public class MoveDirection
    {
        public MoveDirection()
        {
            directions = new Vector2[4];
        }

        private readonly Vector2[] directions;

        public Vector2 Build()
        {
            var lookDirection = Vector2.Zero;
            foreach (var direction in directions)
                lookDirection += direction;
            if (lookDirection.LengthSquared() > 0)
                return Vector2.Normalize(lookDirection);
            else
                return lookDirection;
        }

        public void CutUp()
        {
            directions[0] = Vector2.Zero;
        }

        public void OpenUp()
        {
            directions[0] = -Vector2.UnitY;
        }

        public void CutRight()
        {
            directions[1] = Vector2.Zero;
        }
        public void OpenRight()
        {
            directions[1] = Vector2.UnitX;
        }

        public void CutDown()
        {
            directions[2] = Vector2.Zero;
        }

        public void OpenDown()
        {
            directions[2] = Vector2.UnitY;
        }

        public void CutLeft()
        {
            directions[3] = Vector2.Zero;
        }

        public void OpenLeft()
        {
            directions[3] = -Vector2.UnitX;
        }
    }
}

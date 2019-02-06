using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace RayCaster
{
    public class Caster
    {
        public Caster() : this(0, 0)
        {

        }

        public Caster(float x, float y) : this(new Vector2(x, y))
        {

        }

        public Caster(Vector2 position)
        {
            Position = position;
        }

        public double X => Position.X;
        public double Y => Position.Y;
        public Vector2 Position;
        public MoveDirection MoveDirection = new MoveDirection();
        public Vector2 LookDirection = Vector2.Zero;

        private float speed = 7;
        private Vector2 mousePosition;

        public void OnKeyDown(VirtualKey key)
        {
            if (key == VirtualKey.W)
                MoveDirection.OpenUp();
            if (key == VirtualKey.S)
                MoveDirection.OpenDown();
            if (key == VirtualKey.A)
                MoveDirection.OpenLeft();
            if (key == VirtualKey.D)
                MoveDirection.OpenRight();
        }

        public void OnKeyUp(VirtualKey key)
        {
            if (key == VirtualKey.W)
                MoveDirection.CutUp();
            if (key == VirtualKey.S)
                MoveDirection.CutDown();
            if (key == VirtualKey.A)
                MoveDirection.CutLeft();
            if (key == VirtualKey.D)
                MoveDirection.CutRight();
        }

        public void OnMouseMoved(Vector2 mpos)
        {
            mousePosition = mpos;
        }

        private void RecalcLookDirection()
        {
            var dir = mousePosition - Position;
            if (dir.LengthSquared() > 0)
                dir = Vector2.Normalize(dir);
            LookDirection = dir;
        }

        public void Move()
        {
            Position += MoveDirection.Build() * speed;
        }

        public void Tick()
        {
            RecalcLookDirection();
            Move();
        }
    }
}

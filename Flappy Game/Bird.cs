using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy_Game
{
    public class Bird
    {
        private const float Gravity = 1500f;
        private const float JumpImpulse = -450f;
        private const int Size = 32;

        private readonly Texture2D texture;
        private readonly PipeManager pipes;
        private readonly float floorY;
        private readonly HashSet<Pipe> scoredPipes = new();

        private Vector2 position;
        private float velocityY;

        public bool Alive = true;
        public int Score;
        public float TimeAlive;
        public float DistanceFromGapCenter;
        public float DistanceFromFloor;

        public Bird(Texture2D circleTexture, Vector2 startPosition, PipeManager pipes, float floorY)
        {
            this.texture = circleTexture;
            this.position = startPosition;
            this.pipes = pipes;
            this.floorY = floorY;
            UpdateObservation();
        }

        private Vector2 Center => new Vector2(position.X + Size * 0.5f, position.Y + Size * 0.5f);
        private Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, Size, Size);

        public void Jump()
        {
            if (!Alive) return;
            velocityY = JumpImpulse;
        }

        public void Update(float dt)
        {
            if (!Alive) return;

            velocityY += Gravity * dt;
            position.Y += velocityY * dt;
            TimeAlive += dt;

            if (position.Y < 0f)
            {
                position.Y = 0f;
                velocityY = 0f;
            }
            if (position.Y + Size >= floorY)
            {
                position.Y = floorY - Size;
                Alive = false;
                return;
            }

            var bb = Bounds;
            float centerX = Center.X;
            foreach (var p in pipes.Pipes)
            {
                if (bb.Intersects(p.TopRect) || bb.Intersects(p.BottomRect))
                {
                    Alive = false;
                    return;
                }
                if (p.GapCenterX < centerX && scoredPipes.Add(p))
                    Score++;
            }

            UpdateObservation();
        }

        private void UpdateObservation()
        {
            var next = NextPipe();
            DistanceFromGapCenter = next != null ? next.GapCenterY - Center.Y : 0f;
            DistanceFromFloor = floorY - (position.Y + Size);
        }

        private Pipe NextPipe()
        {
            foreach (var p in pipes.Pipes)
                if (p.RightEdge >= position.X) return p;
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.Yellow);
        }
    }
}

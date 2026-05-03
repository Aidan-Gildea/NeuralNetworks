using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy_Game
{
    public class Pipe
    {
        public const int Width = 70;

        public float X;
        public float GapTopY;
        public float GapHeight;

        private readonly float floorY;

        public float GapBottomY => GapTopY + GapHeight;
        public float GapCenterX => X + Width * 0.5f;
        public float GapCenterY => GapTopY + GapHeight * 0.5f;
        public float RightEdge => X + Width;

        public Rectangle TopRect => new Rectangle((int)X, 0, Width, (int)GapTopY);
        public Rectangle BottomRect =>
            new Rectangle((int)X, (int)GapBottomY, Width, (int)(floorY - GapBottomY));

        public Pipe(float x, float gapTopY, float gapHeight, float floorY)
        {
            X = x;
            GapTopY = gapTopY;
            GapHeight = gapHeight;
            this.floorY = floorY;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            var green = new Color(70, 170, 80);
            spriteBatch.Draw(pixel, TopRect, green);
            spriteBatch.Draw(pixel, BottomRect, green);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy_Game
{
    public class PipeManager
    {
        public const float ScrollSpeed = 150f;
        public const float SpawnInterval = 1.8f;
        public const float GapHeight = 160f;
        public const float MinGapTop = 80f;

        public readonly List<Pipe> Pipes = new();

        private readonly int screenW;
        private readonly int screenH;
        private readonly int floorHeight;
        private readonly float floorY;
        private readonly Random rng = new();
        private float spawnTimer;

        public PipeManager(int screenW, int screenH, int floorHeight)
        {
            this.screenW = screenW;
            this.screenH = screenH;
            this.floorHeight = floorHeight;
            this.floorY = screenH - floorHeight;
        }

        public void Reset()
        {
            Pipes.Clear();
            spawnTimer = 0f;
        }

        public void Update(float dt)
        {
            spawnTimer -= dt;
            if (spawnTimer <= 0f)
            {
                spawnTimer = SpawnInterval;
                float maxTop = screenH - floorHeight - GapHeight - 60f;
                float gapTop = MathHelper.Lerp(MinGapTop, maxTop, (float)rng.NextDouble());
                Pipes.Add(new Pipe(screenW + 20f, gapTop, GapHeight, floorY));
            }

            foreach (var p in Pipes) p.X -= ScrollSpeed * dt;
            Pipes.RemoveAll(p => p.RightEdge < -10f);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            foreach (var p in Pipes) p.Draw(spriteBatch, pixel);
        }
    }
}

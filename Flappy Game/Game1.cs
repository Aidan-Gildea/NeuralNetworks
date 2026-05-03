using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeuralNetworks.Feed_Forward_neural_Network;

namespace Flappy_Game
{
    public class Game1 : Game
    {
        public const int ScreenW = 800;
        public const int ScreenH = 600;
        public const int FloorHeight = 60;
        public const int PopulationSize = 50;
        public const int BirdDiameter = 32;

        private static readonly Color Sky = new Color(110, 180, 230);
        private static readonly Color Ground = new Color(120, 90, 50);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;
        private Texture2D circle;

        private List<Bird> birds;
        private PipeManager pipes;

        public IReadOnlyList<Bird> Birds => birds;
        public PipeManager Pipes => pipes;
        public float FloorY => ScreenH - FloorHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = ScreenW,
                PreferredBackBufferHeight = ScreenH,
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            circle = MakeCircleTexture(BirdDiameter);

            ResetGame();
        }

        // Generates a circular texture to represent the bird.
        private Texture2D MakeCircleTexture(int diameter)
        {
            var tex = new Texture2D(GraphicsDevice, diameter, diameter);
            var data = new Color[diameter * diameter];
            float r = diameter * 0.5f;
            float r2 = r * r;
            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    float dx = x - r + 0.5f;
                    float dy = y - r + 0.5f;
                    data[y * diameter + x] = (dx * dx + dy * dy <= r2)
                        ? Color.White
                        : Color.Transparent;
                }
            }
            tex.SetData(data);
            return tex;
        }

        
        private void ResetGame()
        {
            pipes = new PipeManager(ScreenW, ScreenH, FloorHeight);
            birds = new List<Bird>(PopulationSize);
            for (int i = 0; i < PopulationSize; i++)
            {
                birds.Add(new Bird(circle, new Vector2(150f, ScreenH * 0.4f), pipes, FloorY));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            pipes.Update(dt);
            foreach (var b in birds)
                b.Update(dt);

            if (AllDead())
                FlappyDied();

            base.Update(gameTime);
        }

        private bool AllDead()
        {
            foreach (var b in birds)
                if (b.Alive) return false;
            return true;
        }

        private void FlappyDied()
        {
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Sky);

            spriteBatch.Begin();

            spriteBatch.Draw(pixel,
                new Rectangle(0, ScreenH - FloorHeight, ScreenW, FloorHeight), Ground);

            pipes.Draw(spriteBatch, pixel);
            foreach (var b in birds)
                b.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

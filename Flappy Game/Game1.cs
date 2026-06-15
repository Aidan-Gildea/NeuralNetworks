using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NeuralNetworks.Feed_Forward_neural_Network;
using System;
using System.Collections.Generic;
using System.Net;

namespace Flappy_Game
{
    public class Game1 : Game
    {
        public const int ScreenW = 800;
        public const int ScreenH = 600;
        public const int FloorHeight = 60;
        public const int PopulationSize = 10000;
        public const int BirdDiameter = 32;
        public const float BirdStartX = 150f;
        public const double MutationRate = 0.05;
        public const int FastForwardMultiplier = 5;

        private static readonly Color Sky = new Color(110, 180, 230);
        private static readonly Color Ground = new Color(120, 90, 50);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;
        private Texture2D circle;
        private SpriteFont font;

        private List<Bird> birds;
        private PipeManager pipes;
        private readonly Random rng = new();
        private KeyboardState prevKeyboard;
        private int min = -1;
        private int max = 1;

        public int BirdsAlive { get; private set; }

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

            // Generates a circular texture to represent each bird.
            circle = new Texture2D(GraphicsDevice, BirdDiameter, BirdDiameter);
            var circleData = new Color[BirdDiameter * BirdDiameter];
            float r = BirdDiameter * 0.5f;
            float r2 = r * r;
            for (int y = 0; y < BirdDiameter; y++)
            {
                for (int x = 0; x < BirdDiameter; x++)
                {
                    float dx = x - r + 0.5f;
                    float dy = y - r + 0.5f;
                    circleData[y * BirdDiameter + x] = (dx * dx + dy * dy <= r2)
                        ? Color.White
                        : Color.Transparent;
                }
            }
            circle.SetData(circleData);

            font = Content.Load<SpriteFont>("Font");

            pipes = new PipeManager(ScreenW, ScreenH, FloorHeight);

            birds = new List<Bird>(PopulationSize);
            for (int i = 0; i < PopulationSize; i++)
            {
                Bird bird = new Bird(circle, new Vector2(BirdStartX, ScreenH * 0.4f), FloorY);
                bird.InitializeBrain(rng, min, max);
                birds.Add(bird);
            }
            BirdsAlive = birds.Count;
        }

        private void ResetGame()
        {
            pipes.Reset();
            foreach (var b in birds)
                b.Reset();
            BirdsAlive = birds.Count;
        }

        // Will perform genetic mutation and crossover on the bird population.
        private void NaturalSelection()
        {
            // sort birds by distance traveled, descending

            
            PriorityQueue<Bird, float> sorted = new();

            foreach (var b in birds) 
            {
                sorted.Enqueue(b, -b.DistanceTraveled);
            }

            // select top 10% of population as parents for next generation
            int topCount = PopulationSize / 10;
            int midCount = PopulationSize / 2;

            List<Bird> topbirds = new();
            for (int i = 0; i < topCount; i++)
            {
                topbirds.Add(sorted.Dequeue());
            }

            List<Bird> midbirds = new();
            for (int i = 0; i < midCount; i++)
            {
                midbirds.Add(sorted.Dequeue());
            }
            
            List<Bird> bumbirds = new();
            // select top three
            while (sorted.Count != 0)
            {
                bumbirds.Add(sorted.Dequeue());
            }

            foreach(var bird in bumbirds) 
            {
                bird.Brain.Randomize(rng, min, max);
            }

            // crossover and mutate the rest of the population

            foreach(var bird in midbirds)
            {
                Crossover(topbirds[rng.Next(topbirds.Count)].Brain, bird.Brain, rng);
                Mutate(bird.Brain);
            }
            foreach (var bird in bumbirds)
            {
                Mutate(bird.Brain);
            }   


            List<Bird> newGen = new();
            newGen.AddRange(topbirds);
            newGen.AddRange(midbirds);
            newGen.AddRange(bumbirds);  

            birds = newGen;

        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            // Press R to wipe the population and trigger a normal generation reset.
            if (keyboard.IsKeyDown(Keys.R) && !prevKeyboard.IsKeyDown(Keys.R))
            {
                foreach (var b in birds) b.Alive = false;
            }

            // Hold Space to fast-forward the simulation.
            int ticks = keyboard.IsKeyDown(Keys.Space) ? FastForwardMultiplier : 1;
            prevKeyboard = keyboard;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int t = 0; t < ticks; t++)
            {
                pipes.Update(dt);
                var nextPipe = pipes.NextPipe(BirdStartX);

                int alive = 0;
                foreach (var b in birds)
                {
                    b.Update(dt, nextPipe);
                    if (b.Alive) alive++;
                }
                BirdsAlive = alive;

                if (BirdsAlive == 0)
                {
                    NaturalSelection();
                    ResetGame();
                }
            }

            base.Update(gameTime);
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

            spriteBatch.DrawString(font, $"Alive: {BirdsAlive} / {PopulationSize}",
                new Vector2(10, 10), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Nudges every weight and bias in the network by a random offset in [-MutationRate, +MutationRate].
        private void Mutate(FFNN ffnn)
        {
            foreach (var layer in ffnn.layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.Bias += (rng.NextDouble() * 2 - 1) * MutationRate;
                    foreach (var dendrite in neuron.Dendrites)
                    {
                        dendrite.Weight += (rng.NextDouble() * 2 - 1) * MutationRate;
                    }
                }
            }
        }

        private void Crossover(FFNN winner, FFNN loser, Random random)
        {
            for (int i = 0; i < winner.layers.Length; i++)
            {
                //References to the Layers
                Layer winLayer = winner.layers[i];
                Layer childLayer = loser.layers[i];

                int cutPoint = random.Next(winLayer.Neurons.Length); //calculate a cut point for the layer
                bool flip = random.Next(2) == 0; //randomly decide which side of the cut point will come from winner

                //Either copy from 0->cutPoint or cutPoint->Neurons.Length from the winner based on the flip variable
                for (int j = (flip ? 0 : cutPoint); j < (flip ? cutPoint : winLayer.Neurons.Length); j++)
                {
                    //References to the Neurons
                    Neuron winNeuron = winLayer.Neurons[j];
                    Neuron childNeuron = childLayer.Neurons[j];

                    //Copy the winners Weights and Bias into the loser/child neuron
                    winNeuron.CopyTo(childNeuron);
                }
            }
        }
    }
}

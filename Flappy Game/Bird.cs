using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NeuralNetworks.Feed_Forward_neural_Network;
using NeuralNetworks.Perceptron;
using System;


namespace Flappy_Game
{
    public class Bird
    {
        private const float Gravity = 1500f;
        private const float JumpImpulse = -450f;
        private const int Size = 32;

        private readonly Texture2D texture;
        private readonly float floorY;
        private readonly Vector2 startPosition;
        private Vector2 position;
        private float velocityY;

        public bool Alive = true;
        public float DistanceTraveled;
        public float TimeAlive;
        public double DistanceFromGapCenter;
        public double DistanceFromFloor;

        public ActivationFunction activationFunction = new(ActivationFunctions.Sigmoid, ActivationFunctions.SigmoidDerivative);
        public ErrorFunction errorFunction = new(ErrorFunctions.MSE, ErrorFunctions.MSEDerivative);

        public NeuralNetworks.Feed_Forward_neural_Network.FFNN Brain;

        

        public Bird(Texture2D circleTexture, Vector2 startPosition, float floorY)
        {
            this.texture = circleTexture;
            this.startPosition = startPosition;
            this.position = startPosition;
            this.floorY = floorY;

            // a 2 4 1 neural network with sigmoid activation and MSE error function
            Brain = new(activationFunction, errorFunction, 2, 4, 1);
        }

        public void InitializeBrain(Random random, double min, double max)
        {
            Brain.Randomize(random, min, max);
        }

        public float X => position.X;

        public void Reset()
        {
            position = startPosition;
            velocityY = 0f;
            Alive = true;
            DistanceTraveled = 0f;
            TimeAlive = 0f;
            DistanceFromGapCenter = 0;
            DistanceFromFloor = 0;
        }

        private Vector2 Center => new Vector2(position.X + Size * 0.5f, position.Y + Size * 0.5f);
        private Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, Size, Size);

        public void Jump()
        {
            if (!Alive) return;
            velocityY = JumpImpulse;
        }

        public void Update(float dt, Pipe nextPipe)
        {
            if (!Alive) return;

            double[] outputs = Brain.Compute(NormalizeObservations(400f, 508f));
            if (outputs[0] >= 0.5)
            {
                Jump();
            }

            velocityY += Gravity * dt;
            position.Y += velocityY * dt;
            TimeAlive += dt;
            DistanceTraveled += PipeManager.ScrollSpeed * dt;

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

            if (nextPipe != null)
            {
                var bb = Bounds;
                if (bb.Intersects(nextPipe.TopRect) || bb.Intersects(nextPipe.BottomRect))
                {
                    Alive = false;
                    return;
                }
            }

            UpdateObservation(nextPipe);
        }

        private void UpdateObservation(Pipe nextPipe)
        {
            DistanceFromGapCenter = nextPipe != null ? nextPipe.GapCenterY - Center.Y : 0;
            DistanceFromFloor = floorY - (position.Y + Size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Alive) return;
            spriteBatch.Draw(texture, Bounds, Color.Yellow);
        }

        public double[] NormalizeObservations(float maxGapDistance, float maxFloorDistance)
        {
            double[] returnvalues = new double[2];
            returnvalues[0] = DistanceFromGapCenter / maxGapDistance;
            returnvalues[1] = DistanceFromFloor / maxFloorDistance;
            return returnvalues;
        }
    }
}

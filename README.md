# Neural Networks from Scratch in C#

Classical neural networks and game AI implemented from first principles in C#. No ML libraries: every neuron, weight update, and search tree here is written by hand. The goal was to understand the math by building it, the same philosophy as my [custom ISA and CPU](https://github.com/Aidan-Gildea/SimpleISA): work one layer below the abstraction you normally get for free.

## What's inside

| Project | What it is | Key files |
|---|---|---|
| **Perceptron** | Single-layer perceptron with mutation-based learning, plus a line-of-best-fit regression demo. Activation, error, normalization, and random-init functions are split into swappable modules. | `NeuralNetworks/Perceptron/Perceptron.cs`, `LineOfBestFitPerceptron.cs`, `ActivationFunctions.cs`, `ErrorFunctions.cs` |
| **Feed-Forward Neural Network** | A multi-layer network built object by object: neurons hold dendrites, dendrites hold weights, layers hold neurons. Nothing is a matrix until you understand why it should be. | `NeuralNetworks/Feed Forward neural Network/Neuron.cs`, `Dendrite.cs`, `Layer.cs`, `FFNN.cs` |
| **Backpropagation** | Console demo that trains the feed-forward network with backpropagation (gradient descent via the chain rule). | `Backpropogation/Program.cs` |
| **Hill Climber** | A black-box optimizer: mutate, keep improvements, discard regressions. Useful as a baseline to appreciate what gradients buy you. | `NeuralNetworks/Hill Climber/HillClimber.cs` |
| **MiniMax** | Game-tree search with alpha-beta pruning. | `MiniMax/Program.cs` |
| **Genetic Flappy Bird** | A population of networks learns Flappy Bird through neuroevolution, rendered live in MonoGame. Watch generations go from faceplanting into the first pipe to playing indefinitely. | `Flappy Game/Bird.cs`, `Game1.cs`, `PipeManager.cs` |

<!-- TODO: add a gameplay GIF of the genetic Flappy Bird here -->

## How the network is built

The feed-forward implementation deliberately avoids matrix shortcuts. A `Neuron` owns its incoming `Dendrite` connections, each `Dendrite` owns a weight, and a `Layer` is a collection of neurons. A forward pass walks this object graph. Training happens two ways: the hill climber mutates weights blindly and keeps what works, while the backpropagation project computes exact gradients and follows them. Implementing both made the difference between them concrete: one is search, the other is calculus.

## Running it

Requires the [.NET SDK](https://dotnet.microsoft.com/download). Each project is a standalone console app inside one solution (`NeuralNetworks.sln`), so you can also open everything in Visual Studio or Rider.

```bash
git clone https://github.com/Aidan-Gildea/NeuralNetworks.git
cd NeuralNetworks

dotnet run --project Backpropogation     # train the MLP with backprop
dotnet run --project MiniMax             # game-tree search demo
dotnet run --project "Flappy Game"       # genetic Flappy Bird (MonoGame window)
```

The Flappy Bird project restores MonoGame automatically through NuGet on first build.

## Related projects

- [SimpleISA](https://github.com/Aidan-Gildea/SimpleISA): a custom 32-bit instruction set with an assembler, disassembler, emulator, and a gate-level CPU in Logisim.
- [robot-arm-3dof](https://github.com/Aidan-Gildea/robot-arm-3dof): a 3-DOF robot arm with inverse kinematics derived from first principles.

## License

MIT

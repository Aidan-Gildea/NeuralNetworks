using System.Security.Cryptography.X509Certificates;

namespace MiniMax
{
    // assume maxxer always goes first
    // alpha beta visualizer: https://raphsilva.github.io/utilities/minimax_simulator/#
    public enum BoardState 
    {
        xWin,   // 
        oWin,   // terminal states
        Tie,    // 

        Ongoing // not terminal
    }
    public interface IGameState<T> where T : IGameState<T> // T has to also inherit from igamestate
    {
        public BoardState State { get; }
        public bool IsMaxxer { get; set; }
        T[] GetChildren();

    }

    public class TicTacToeState : IGameState<TicTacToeState>
    {
        public int[,] board;

        // -- interface properties
        public BoardState State { get; private set; }
        public bool IsMaxxer { get; set; }

        public int Value { get; set; }

        List<TicTacToeState> Children { get; set; }
        
        // -- 

        public TicTacToeState(int[,] board, bool isMaxxer) 
        {
            if (board == null) 
            {
                board = new int[3,3] 
                {
                    {0,0,0},
                    {0,0,0},
                    {0,0,0}
                };
            }

            this.board = board;
            IsMaxxer = isMaxxer;

            State = GetState(this.board);
        }

        public TicTacToeState[] GetChildren()
        {
            List<TicTacToeState> states = new();
            // 0 for empty, 1 for maxxer (x) and -1 for minner (o)
            for(int y = 0; y < 3; y++) 
            {
                for(int x = 0; x < 3; x++) 
                {
                    if (board[x,y] == 0)
                    {
                        int[,] newBoard = new int[3, 3];
                        board.MyCopyTo(newBoard);

                        newBoard[x, y] = IsMaxxer ? 1 : -1;

                        states.Add(new(newBoard, !IsMaxxer));
                    }
                }
            }

            return states.ToArray();
        }

        public void DefineTree()
        {
            // base case: the game is over. Score it now — a win can happen
            // before the board is full, so we must check State here rather
            // than waiting until there are no children left to generate.
            if (State != BoardState.Ongoing)
            {
                Children = new();
                Value = Score();
                return;
            }

            Children = GetChildren().ToList();

            List<int> childValues = new();
            foreach (var child in Children)
            {
                child.DefineTree();
                childValues.Add(child.Value);
            }

            // maxxer (x) maximizes, minner (o) minimizes
            Value = IsMaxxer ? childValues.Max() : childValues.Min();
        }

        // +ve favours x, -ve favours o. All wins score the same regardless of
        // how deep they are, so among equally-winning moves the AI just takes
        // whichever it happens to see first — an arbitrary winning move.
        private int Score()
        {
            return State switch
            {
                BoardState.xWin => 1,
                BoardState.oWin => -1,
                _ => 0
            };
        }

        // pick the child that's best for whoever is to move at this node.
        // assumes DefineTree() has already populated Children + their Values.
        public TicTacToeState BestMove()
        {
            TicTacToeState best = null;
            foreach (var child in Children)
            {
                if (best == null
                    || (IsMaxxer && child.Value > best.Value)
                    || (!IsMaxxer && child.Value < best.Value))
                {
                    best = child;
                }
            }
            return best;
        }

        private BoardState GetState(int[,] board)
        {
            // 1 = maxxer (x), -1 = minner (o), 0 = empty
            // check each row and column for a line of three
            for (int i = 0; i < 3; i++)
            {
                // row i
                if (board[0, i] != 0 && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return board[0, i] == 1 ? BoardState.xWin : BoardState.oWin;

                // column i
                if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return board[i, 0] == 1 ? BoardState.xWin : BoardState.oWin;
            }

            // both diagonals
            if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return board[0, 0] == 1 ? BoardState.xWin : BoardState.oWin;

            if (board[2, 0] != 0 && board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
                return board[2, 0] == 1 ? BoardState.xWin : BoardState.oWin;

            // no winner: it's a tie if the board is full, otherwise still ongoing
            foreach (int cell in board)
            {
                if (cell == 0)
                    return BoardState.Ongoing;
            }

            return BoardState.Tie;
        }
    }
    internal class Program
    {
        // tictactoe: the human is X (goes first), the minimax AI is O
        static void Main(string[] args)
        {
            int[,] board = new int[3, 3];
            bool xToMove = true; // x (the human) moves first

            Console.WriteLine("You are X, the AI is O.");
            Console.WriteLine("Choose a cell with the keys 1-9:\n");
            Console.WriteLine(" 1 | 2 | 3 \n 4 | 5 | 6 \n 7 | 8 | 9 \n");

            while (true)
            {
                var state = new TicTacToeState(board, xToMove);
                PrintBoard(board);

                // stop as soon as someone has won or the board is full
                if (state.State != BoardState.Ongoing)
                {
                    Console.WriteLine(state.State switch
                    {
                        BoardState.xWin => "You win!",
                        BoardState.oWin => "The AI wins!",
                        _ => "It's a tie!"
                    });
                    break;
                }

                if (xToMove)
                {
                    int index = ReadHumanMove(board);
                    board[index % 3, index / 3] = 1; // x
                }
                else
                {
                    Console.WriteLine("AI is thinking...");
                    state.DefineTree();           // build + score the game tree
                    board = state.BestMove().board; // play the optimal child
                }

                xToMove = !xToMove;
            }
        }

        // returns a 0-8 cell index for an empty square chosen by the player
        static int ReadHumanMove(int[,] board)
        {
            while (true)
            {
                Console.Write("Your move (1-9): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int cell) && cell >= 1 && cell <= 9)
                {
                    int index = cell - 1;
                    if (board[index % 3, index / 3] == 0)
                        return index;

                    Console.WriteLine("That cell is already taken.");
                }
                else
                {
                    Console.WriteLine("Please enter a number from 1 to 9.");
                }
            }
        }

        static void PrintBoard(int[,] board)
        {
            string Symbol(int v) => v == 1 ? "X" : v == -1 ? "O" : " ";

            Console.WriteLine();
            for (int y = 0; y < 3; y++)
            {
                Console.WriteLine($" {Symbol(board[0, y])} | {Symbol(board[1, y])} | {Symbol(board[2, y])} ");
                if (y < 2) Console.WriteLine("---+---+---");
            }
            Console.WriteLine();
        }
    }

    public static class ArrayExtensions 
    {
        public static void MyCopyTo(this int[,] array, int[,] other)
        {
            for(int y = 0; y < other.GetLength(0); y++) 
            {
                for(int x = 0; x < other.GetLength(1); x++) 
                {
                    other[y, x] = array[y, x];
                }
            }
        }
    }

    public class MiniMax
    {
        public TicTacToeState start;

        // note that by default x will be the player and o will be the ai
        // x will be dictated to go first if isMax = True
        // x will be dictated to go second if IsMax = False


        public MiniMax(int[,] startBoard, bool isMax) 
        {
            start = new(startBoard, isMax);
            start.DefineTree();

        }


    }
}

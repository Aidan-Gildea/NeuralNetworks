namespace MonteCarloTreeSearch;


public enum BoardState
{
    xWin,   // 
    oWin,   // terminal states
    Tie,    // 
    Ongoing
}

// Every game state must conform to this
public interface IGameState 
{
    BoardState GetState(); // get the score for your current state
    List<IGameState> GetMoves();
    bool IsTerminal() => GetState() != BoardState.Ongoing;
    IGameState ApplyMove(int index); // applies a checkmark at a given position by applying the opposite player's mark to the board and returning a new state
    bool CurrentPlayer;
    IGameState Clone(); // returns a copy of the current state
}

public class TicTacToeState : IGameState
{
    public int[] board;
    public bool CurrentPlayer { get; set; }


    public TicTacToeState(int[] board, bool currentPlayer)
    {
        this.board = board;
        CurrentPlayer = currentPlayer;
    }
    public BoardState GetState()
    {
        // evaluate the board here


        return BoardState.Ongoing;
    }
    
    
    public List<IGameState> GetMoves()
    {
        List<IGameState> moves = new List<IGameState>();
        // get all moves
        for(int i = 0; i < 9; i++) 
        {
            try
            {
                moves.Add(ApplyMove(i + 1));
            }
            catch (Exception e)
            {
                // invalid move, do nothing
            }

        }
        return moves;
    }


    public IGameState ApplyMove(int index) // index is 1-9
    {
        TicTacToeState MoveState = this.Clone();
        if(MoveState.board[index - 1] != 0) 
        {
            throw new Exception("Invalid move");
        }
        MoveState.board[index - 1] = CurrentPlayer ? 1 : 2;

        return MoveState;
    }
    public IGameState Clone()
    {
        return new TicTacToeState(board.Clone(), CurrentPlayer);
    }
}
public class MCTS_Node 
{
    int n;
    int w;
    List<IGameState> PossibleMoves;
    IGameState CurrentState;

}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

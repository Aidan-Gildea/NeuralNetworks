namespace TicTacToeAI
{
    public partial class Form1 : Form
    {
        // The board. '\0' = empty, 'X' = a placed mark.
        private readonly char[,] grid = new char[3,3];

        // The game loop: ticks continuously and polls for input each frame.
        private readonly System.Windows.Forms.Timer gameLoop = new();

        // Tracks the mouse between ticks so we act once per click, not every
        // tick the button is held down.
        private bool mouseWasDown = false;

        public Form1()
        {
            InitializeComponent();

            gameLoop.Interval = 16; // ~60 ticks per second
            gameLoop.Tick += GameLoop_Tick;
            gameLoop.Start();
        }

        // Runs every tick. Detects a fresh left-click and, if it landed on an
        // enabled button, marks it with an X and disables it.
        private void GameLoop_Tick(object? sender, EventArgs e)
        {
            bool mouseIsDown = MouseButtons == MouseButtons.Left;

            // Only act on the up -> down transition (one action per click).
            if (mouseIsDown && !mouseWasDown)
            {
                Point cursor = PointToClient(Cursor.Position);
                if (GetChildAtPoint(cursor) is Button button && button.Enabled)
                {
                    string[] parts = ((string)button.Tag!).Split(',');
                    int row = int.Parse(parts[0]);
                    int col = int.Parse(parts[1]);

                    grid[row, col] = 'X';
                    
                    // now I call the minimax tree predict next move, outputs a game state next move
                }
            }


            // always update button appearances
            for(int y = 0; y < grid.GetLength(0); y++) 
            {
                for(int x = 0; x < grid.GetLength(1); x++) 
                {

                }
            }

            mouseWasDown = mouseIsDown;
        }
    }
}

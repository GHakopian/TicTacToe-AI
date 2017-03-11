using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class Board : Panel
    {
        public delegate void LogEventHandler(int logNumber, string log);
        public event LogEventHandler logRequest;
        public IPlayer Player1;
        public IPlayer Player2;
        public Tile[][] tiles;
        private int Division;

        public Board()
        {
            tiles = new Tile[Division][];
            //Controls.Add(new Box(0,0,50,50));
        }

        public void startGame(IPlayer player1, IPlayer player2)
        {

            Player1 = player1;
            player1.init(this);
            Player1.MoveRequest += onPlayerMoveRequest;
            player1.LogRequest += onLogRequest;
            player1.WinRequest += onPlayerWinRequest;
            Player2 = player2;
            player2.init(this);
            Player2.MoveRequest += onPlayerMoveRequest;
            player2.LogRequest += onLogRequest;
            player2.WinRequest += onPlayerWinRequest;
            player1.hasTurn = true;
            
        }

        private void onPlayerWinRequest(object sender,Tile tile, List<Tile[]> winningTileRows)
        {
            IPlayer movedPlayer = sender as IPlayer;
            if (movedPlayer.Token == Token.X) { tile.markX(); } else { tile.markO(); }
            foreach (Tile[] tileRow in winningTileRows)
            {
                if (tileRow.Length > 3)
                {
                    for (int i = 0; i < tileRow.Length; i++)
                    {
                        Tile currentTile = tileRow[i];
                        if (currentTile.Token != movedPlayer.Token)
                        {
                            break;
                        }
                    }
                    
                    showWinningRow(tileRow);
                    MessageBox.Show(movedPlayer.Name + " won the match!");
                    break;
                }
            }
        }

        private void showWinningRow(Tile[] tileRow)
        {
            Player1.Stop();
            Player2.Stop();
            foreach (Tile tile in tileRow)
            {
                tile.Bold();
            }
        }

        private void onLogRequest(object sender, string log)
        {

            IPlayer movedPlayer = sender as IPlayer;
            if (movedPlayer.Name.Equals(Player1.Name))
            {
                if (logRequest != null) { logRequest(1, log); }
            }
            else
            {
                if (logRequest != null) { logRequest(2, log); }
            }
        }

        private void onPlayerMoveRequest(object sender, Tile tile, Token token)
        {
            IPlayer movedPlayer = sender as IPlayer;
            if (token == Token.X) { tile.markX(); } else { tile.markO(); }
            if (hasAvailableMoves())
            {
                if (movedPlayer.Name.Equals(Player1.Name))
                {
                    Player2.hasTurn = true;
                }
                else
                {
                    Player1.hasTurn = true;
                }
            }
            else {
                MessageBox.Show("Its a draw!");
            }
        }

        public bool hasAvailableMoves()
        {
            for (int y = 0; y < tiles.Length; y++)
            {
                for (int x = 0; x < tiles[y].Length; x++)
                {
                    if (tiles[y][x].Token == Token.Empty)
                    { return true; }
                }
            }
            return false;
        }

        public void resetBoard(int division)
        {
            Division = division;
            Controls.Clear();
            tiles = new Tile[division][];
            var boxHeight = Height / division;
            var boxWidth = Width / division;
            for (int y = 0; y < division; y++)
            {
                tiles[y] = new Tile[division];
                for (int x = 0; x < division; x++)
                {
                    var tile = new Tile(x * boxWidth, y * boxHeight, boxHeight, boxWidth);
                    tile.gridIndex = new Point(x, y);
                    tiles[y][x] = tile;
                    Controls.Add(tile);
                }
            }
        }
    }
}

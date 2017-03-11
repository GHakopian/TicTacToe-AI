using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    class HumanPlayer : IPlayer
    {
        private List<Tile[]> possibleWinRows; // rows that already have 3 on a row and only need a 4th one


        public Board Board { get; set; }

        public Token EnemyToken { get; set; }

        public bool hasTurn { get; set; }

        public string Name { get; set; }

        public Token Token { get; set; }

        public event LogEventHandler LogRequest;
        public event MoveEventHandler MoveRequest;
        public event WinEventHandler WinRequest;

        public HumanPlayer(string name, Token token, Board board)
        {
            Name = name;
            Token = token;
            Board = board;
            for (int i = 0; i < Board.tiles.Length; i++)
            {
                for (int j = 0; j < Board.tiles[i].Length; j++)
                {
                    Tile tile = Board.tiles[i][j];
                    tile.MouseEnter += onMouseEnter;
                    tile.MouseLeave += onMouseLeave;
                    tile.Click += onClick;
                }
            }
        }

        public void init(Board board)
        {
            Board = board;
            for (int i = 0; i < Board.tiles.Length; i++)
            {
                for (int j = 0; j < Board.tiles[i].Length; j++)
                {
                    Tile tile = Board.tiles[i][j];
                    tile.MouseEnter += onMouseEnter;
                    tile.MouseLeave += onMouseLeave;
                    tile.Click += onClick;
                }
            }
        }

        private void onClick(object sender, EventArgs e)
        {
            var tile = sender as Tile;
            CollectBoardInformation();
            if (possibleWinRows.Count > 0) {
                requestWin(tile,possibleWinRows);
            }
            requestMove(tile);
        }

        private void onMouseLeave(object sender, EventArgs e)
        {
            var tile = sender as Tile;
            tile.BackColor = Color.LightGray;
        }

        private void onMouseEnter(object sender, EventArgs e)
        {
            var tile = sender as Tile;
            tile.BackColor = Color.Yellow;
        }
        public void Stop()
        {
            for (int i = 0; i < Board.tiles.Length; i++)
            {
                for (int j = 0; j < Board.tiles[i].Length; j++)
                {
                    Tile tile = Board.tiles[i][j];
                    tile.MouseEnter -= onMouseEnter;
                    tile.MouseLeave -= onMouseLeave;
                    tile.Click -= onClick;
                }
            }
        }

        private void requestWin(Tile tile, List<Tile[]> possibleWinRows)
        {
            if (WinRequest != null)
            {
                Board.Invoke(WinRequest, new object[] { this, tile, possibleWinRows });
            }
            hasTurn = false;
        }


        private void requestMove(Tile tile)
        {
            if (MoveRequest != null)
            {
                Board.Invoke(MoveRequest, new object[] { this, tile, Token });
            }
            hasTurn = false;
        }


        private void checkTileRow(Tile[] tileRow)
        {
            Token enemyToken = this.Token == Token.X ? Token.O : Token.X;
            uint ownTokenCount = 0;
            uint enemyTokenCount = 0;
            uint emptyTokenCount = 0;
            for (int i = 0; i < tileRow.Length; i++)
            {
                Tile currentTile = tileRow[i];
                if (currentTile.Token == this.Token)
                    ownTokenCount++;
                else if (currentTile.Token == enemyToken)
                    enemyTokenCount++;
                else
                    emptyTokenCount++;
            }

            // check for win lose situations
            if (ownTokenCount == 3 && enemyTokenCount == 0)
                possibleWinRows.Add(tileRow);
        }


        private Tile[] getNorthernTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == 0)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                for (int i = y; i > y - 4; i--)
                {
                    if (i > -1)
                    {
                        tiles.Add(Board.tiles[i][x]);
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        private Tile[] getEasternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (x == Board.tiles[y].Length - 1)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                for (int i = x; i < x + 4; i++)
                {
                    if (i < Board.tiles[y].Length)
                    {
                        tiles.Add(Board.tiles[y][i]);
                    }
                    else { break; }
                }
                return tiles.ToArray();
            }
        }

        private Tile[] getSouthernTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == Board.tiles.Length - 1)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                for (int i = y; i < y + 4; i++)
                {
                    if (i < Board.tiles.Length)
                    {
                        tiles.Add(Board.tiles[i][x]);
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        private Tile[] getWesternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (x == 0)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                for (int i = x; i > x - 4; i--)
                {
                    if (i > -1)
                    {
                        tiles.Add(Board.tiles[y][i]);
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        // diagonal
        private Tile[] getNorthEasternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == 0)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                int j = x;
                for (int i = y; i > y - 4; i--)
                {
                    if (i > -1 && j < Board.tiles[y].Length)
                    {
                        tiles.Add(Board.tiles[i][j]);
                        j++;
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        private Tile[] getSouthEasternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == Board.tiles.Length - 1)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                int j = x;
                for (int i = y; i < y + 4; i++)
                {
                    if (i < Board.tiles.Length && j < Board.tiles[y].Length)
                    {
                        tiles.Add(Board.tiles[i][j]);
                        j++;
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        private Tile[] getSouthWesternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == Board.tiles.Length - 1)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                int j = x;
                for (int i = y; i < y + 4; i++)
                {
                    if (j > -1 && i < Board.tiles.Length)
                    {
                        tiles.Add(Board.tiles[i][j]);
                        j--;
                    }
                    else { break; }
                }
                return tiles.ToArray();
            }
        }

        private Tile[] getNorthWesternTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            var baseTile = Board.tiles[y][x];
            if (y == 0)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                int j = x;
                for (int i = y; i > y - 4; i--)
                {
                    if (i > -1 && j > -1)
                    {
                        tiles.Add(Board.tiles[i][j]);
                        j--;
                    }
                    else { break; }

                }
                return tiles.ToArray();
            }
        }

        private void CollectTileInformation(int x, int y)
        {
            var baseTile = Board.tiles[y][x];
            Tile[] tilesNorth = getNorthernTiles(x, y);
            Tile[] tilesEast = getEasternTiles(x, y);
            Tile[] tilesSouth = getSouthernTiles(x, y);
            Tile[] tilesWest = getWesternTiles(x, y);
            Tile[] tilesNorthEast = getNorthEasternTiles(x, y);
            Tile[] tilesSouthEast = getSouthEasternTiles(x, y);
            Tile[] tilesSouthWest = getSouthWesternTiles(x, y);
            Tile[] tilesNorthWest = getNorthWesternTiles(x, y);

            checkTileRow(tilesNorth);
            checkTileRow(tilesEast);
            checkTileRow(tilesSouth);
            checkTileRow(tilesWest);
            checkTileRow(tilesNorthEast);
            checkTileRow(tilesSouthEast);
            checkTileRow(tilesSouthWest);
            checkTileRow(tilesNorthWest);
        }

        private void CollectBoardInformation()
        {
            possibleWinRows = new List<Tile[]>();
            for (int y = 0; y < Board.tiles.Length; y++)
            {
                Tile[] xRow = Board.tiles[y];
                for (int x = 0; x < xRow.Length; x++)
                {
                    CollectTileInformation(x, y);
                }
            }
        }

    }
}

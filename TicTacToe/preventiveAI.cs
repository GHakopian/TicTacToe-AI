using System;
using System.Collections.Generic;
using System.Threading;


namespace TicTacToeGame
{
    public class preventiveAI : IPlayer
    {
        public event MoveEventHandler MoveRequest;
        public event LogEventHandler LogRequest;
        public event WinEventHandler WinRequest;
        private List<Tile[]> possibleWinRows; // rows that already have 3 on a row and only need a 4th one
        private List<Tile[]> possibleLoseRows; // rows where the enemy already has 3 and only needs a 4th one
        private List<Tile[]> possibleAttackRows3; // rows that can become rows of 3
        private List<Tile[]> possibleAttackRows2; // rows that can become rows of 2
        private List<Tile[]> possibleDefenceRows3; // enemy rows that can become rows of 3
        private List<Tile[]> possibleDefenceRows2; // enemy rows that can become rows of 2

        public Board Board { get; set; }

        public bool hasTurn { get; set; }
        public Token Token { get; set; }
        public Token EnemyToken { get; set; }
        public string Name { get; set; }
        private bool stop;
        private Thread thread;
        public preventiveAI(Board board, Token token, string name)
        {
            stop = false;
            possibleWinRows = new List<Tile[]>();
            possibleLoseRows = new List<Tile[]>();
            possibleAttackRows3 = new List<Tile[]>();
            possibleAttackRows2 = new List<Tile[]>();
            possibleDefenceRows3 = new List<Tile[]>();
            possibleDefenceRows2 = new List<Tile[]>();

            Token = token;
            EnemyToken = this.Token == Token.X ? Token.O : Token.X;
            Board = board;
            Name = name;
            thread = new Thread(play);
            thread.Start();
            
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
            else if (enemyTokenCount == 3 && ownTokenCount == 0)
                possibleLoseRows.Add(tileRow);
            else if (emptyTokenCount > 0 && tileRow.Length > 3) // check for other priority moves
            {
                // attack
                if (ownTokenCount == 2 && enemyTokenCount == 0)
                    possibleAttackRows3.Add(tileRow);
                else if(ownTokenCount > 1)
                    possibleAttackRows2.Add(tileRow);
                //defence
                if (enemyTokenCount == 2 && ownTokenCount == 0)
                    possibleDefenceRows3.Add(tileRow);
                else if (enemyTokenCount > 1)
                    possibleDefenceRows2.Add(tileRow);
            }
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
            else {
                for (int i = y; i > y-4; i--)
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
            if (x == Board.tiles[y].Length-1)
            {
                tiles.Add(baseTile);
                return tiles.ToArray();
            }
            else
            {
                for (int i = x; i < x+4; i++)
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
                for (int i = y; i < y+4; i++)
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
                for (int i = x; i > x-4; i--)
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
            Tile[] tilesNorth = getNorthernTiles(x,y);
            Tile[] tilesEast = getEasternTiles(x,y);
            Tile[] tilesSouth = getSouthernTiles(x, y);
            Tile[] tilesWest = getWesternTiles(x,y);
            Tile[] tilesNorthEast = getNorthEasternTiles(x,y);
            Tile[] tilesSouthEast = getSouthEasternTiles(x, y);
            Tile[] tilesSouthWest = getSouthWesternTiles(x,y);
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
            possibleLoseRows = new List<Tile[]>();
            possibleAttackRows3 = new List<Tile[]>();
            possibleAttackRows2 = new List<Tile[]>();
            possibleDefenceRows3 = new List<Tile[]>();
            possibleDefenceRows2 = new List<Tile[]>();
            for (int y = 0; y < Board.tiles.Length; y++)
            {
                Tile[] xRow = Board.tiles[y];
                for (int x = 0; x < xRow.Length; x++)
                {
                    CollectTileInformation(x,y);
                }
            }
        }

        private Tile GetWinOrLoseMove(List<Tile[]> rowList, Token token)
        {
            if (rowList.Count > 0)
            {
                foreach (Tile[] tileRow in rowList)
                {
                    uint tileCount = 0;
                    Tile winningTile = null;
                    for (int i = 0; i < tileRow.Length; i++)
                    {
                        Tile currentTile = tileRow[i];
                        if (currentTile.Token == token)
                            tileCount++;
                        else if (currentTile.Token == Token.Empty)
                            winningTile = currentTile;
                        else
                            break;
                    }
                    if (tileCount > 2 && winningTile != null) { return winningTile; } 
                }
            }
            return null;
        }

        private Tile getDefenceOrOffenceMove(List<Tile[]> rowList, int rowCount, Token token)
        {
            
            if (rowList.Count > 0)
            {
                foreach (Tile[] tileRow in rowList)
                {
                    uint tileCount = 0;
                    Tile defenceTile = null;
                    for (int i = 0; i < tileRow.Length; i++)
                    {
                        Tile currentTile = tileRow[i];
                        if (currentTile.Token == token)
                        {
                            tileCount++;
                        }
                        else if (currentTile.Token == Token.Empty)
                        {
                            //if (defenceTile != null) { tileCount = 0; }
                            defenceTile = currentTile;
                        }
                        else {
                            tileCount = 0;
                        }
                    }
                    if (tileCount == rowCount && defenceTile != null)
                    {
                        return defenceTile;
                    }
                }
            }
            return null;
        }

        private Tile getRandomMove()
        {
            Random random = new Random();
            while (true)
            {
                int X = random.Next(0, Board.tiles[0].Length);
                int Y = random.Next(0, Board.tiles.Length );
                Tile randomTile = Board.tiles[Y][X];
                if (randomTile.Token == Token.Empty)
                {
                    return randomTile;
                }
            }
        }

        public void play()
        {
            while (!stop)
            {
                try
                {
                    if (hasTurn)
                    {
                        Thread.Sleep(TicTacToeMain.AiDelayMs);
                        CollectBoardInformation();

                        var tile = GetWinOrLoseMove(possibleWinRows, this.Token);
                        if (tile != null) {
                            requestWin(tile, possibleWinRows);
                            requestLog("got me a winning move");
                        }
                        if (!hasTurn) { continue; }

                        tile = GetWinOrLoseMove(possibleLoseRows, EnemyToken);
                        if (tile != null) { requestMove(tile); requestLog("got me a critical defence move"); }
                        if (!hasTurn) { continue; }

                        tile = getDefenceOrOffenceMove(possibleDefenceRows3, 2, EnemyToken);
                        if (tile != null) { requestMove(tile); requestLog("got me a defence 3 move"); }
                        if (!hasTurn) { continue; }

                        tile = getDefenceOrOffenceMove(possibleAttackRows3, 2, this.Token);
                        if (tile != null) { requestMove(tile); requestLog("got me a attack 3 move"); }
                        if (!hasTurn) { continue; }

                        tile = getDefenceOrOffenceMove(possibleDefenceRows2, 1, EnemyToken);
                        if (tile != null) { requestMove(tile); requestLog("got me a defence 2 move"); }
                        if (!hasTurn) { continue; }

                        tile = getDefenceOrOffenceMove(possibleAttackRows2, 1, this.Token);
                        if (tile != null) { requestMove(tile); requestLog("got me a attack 2 move"); }
                        if (!hasTurn) { continue; }

                        tile = getRandomMove();
                        if (tile != null) { requestMove(tile); requestLog("got me a random move"); }
                        if (!hasTurn) { continue; }
                    }
                }
                catch (Exception ex)
                {
                    requestLog("error "+ex.Message);
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
        private void requestLog(string log)
        {
            if (LogRequest != null)
            {
                Board.Invoke(LogRequest, new object[] { this, log });
            }
        }

        public void Stop()
        {
            stop = true;
            thread.Abort();
        }

        public void init(Board board)
        {
            Board = board;
        }
    }

    
}

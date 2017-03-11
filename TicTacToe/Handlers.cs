using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public delegate void MoveEventHandler(object sender, Tile tile, Token token);
    public delegate void LogEventHandler(object sender, string log);
    public delegate void WinEventHandler(object sender, Tile tile, List<Tile[]> winningTileRows);
}

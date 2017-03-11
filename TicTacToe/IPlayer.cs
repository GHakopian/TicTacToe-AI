using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public interface IPlayer
    {
        event MoveEventHandler MoveRequest;
        event LogEventHandler LogRequest;
        event WinEventHandler WinRequest;

        Board Board { get; set; }
        bool hasTurn { get; set; }
        Token Token { get; set; }
        Token EnemyToken { get; set; }
        string Name { get; set; }
        void Stop();
        void init(Board board);
    }
}

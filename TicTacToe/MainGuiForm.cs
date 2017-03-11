using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class TicTacToeMain : Form
    {
        IPlayer player1;
        IPlayer player2;
        public static int AiDelayMs = 1000;
        private int boardSize = 5;
        public TicTacToeMain()
        {
            InitializeComponent();
            gameBoard.logRequest += onLogRequest;
            aiDelayBox.Value = 1000;
            boardSizeBox.Value = 5;
        }

        private void onLogRequest(int logNumber, string log)
        {
            if (logNumber == 1)
            {
                logBox1.Text += Environment.NewLine;
                logBox1.Text += Environment.NewLine;
                logBox1.Text += log;
            }
            else {
                logBox2.Text += Environment.NewLine;
                logBox2.Text += Environment.NewLine;
                logBox2.Text += log;
            }
        }

        private void AiGameBtnClick(object sender, EventArgs e)
        {
            gameBoard.resetBoard(boardSize);
            if (player1 != null) { player1.Stop(); }
            if (player2 != null) { player2.Stop(); }
            player1 = new preventiveAI(gameBoard, Token.X, "AI1");
            player2 = new preventiveAI(gameBoard, Token.O, "AI2");
            gameBoard.startGame(player1, player2);
        }

        private void humanGameBtnClick(object sender, EventArgs e)
        {
            gameBoard.resetBoard(boardSize);
            if (player1 != null) { player1.Stop(); }
            if (player2 != null) { player2.Stop(); }
            player1 = new preventiveAI(gameBoard, Token.X, "AI");
            player2 = new HumanPlayer("Human", Token.O, gameBoard);
            gameBoard.startGame(player1, player2);
        }

        private void onAiDelayChanged(object sender, EventArgs e)
        {
            AiDelayMs = (int)aiDelayBox.Value;
        }

        private void onBoardSizeValueChanged(object sender, EventArgs e)
        {
            boardSize = (int)boardSizeBox.Value;
        }
    }
}

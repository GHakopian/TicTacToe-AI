using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public enum Token { X,O,Empty }

    public class Tile : PictureBox
    {
        public Token Token;
        public Point gridIndex;
        private bool bold;
        public Tile(int x, int y, int width, int height)
        {
            bold = false;
            Token = Token.Empty;
            Size = new Size(width,height);
            Location = new Point(x,y);
            Paint += new PaintEventHandler(onPaint);
            
            BackColor = Color.LightGray;
        }

        public void markX()
        {
            Token = Token.X;
            Refresh();
        }

        public void Bold()
        {
            bold = true;
            Refresh();
        }

        public void markO()
        {
            Token = Token.O;
            Refresh();
        }



        private void onPaint(object sender, PaintEventArgs e)
        {
            // not to self > this is called every time the box needs to redraw
            var g = e.Graphics;
            var pen = new Pen(Color.Black, 5);
            if (bold) { BackColor = Color.Green; }
            g.DrawRectangle(pen, this.ClientRectangle);
            int paddingX = (int)(Size.Width * 0.2);
            int paddingY = (int)(Size.Height * 0.2);
            switch (Token)
            {
                case Token.X:
                    pen.Color = Color.Red;
                    g.DrawLine(pen, new Point(paddingX, paddingY),new Point(Size.Width - paddingX, Size.Height - paddingY));
                    g.DrawLine(pen, new Point(Size.Width - paddingX, paddingY), new Point(paddingX, Size.Height - paddingY));
                    break;
                case Token.O:
                    pen.Color = Color.Blue;
                    g.DrawEllipse(pen, paddingX, paddingY, Size.Width - paddingX*2, Size.Height - paddingY*2);
                    break;
            }
            
        }
    }
}

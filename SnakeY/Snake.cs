using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnakeY
{
    public enum Side 
    {
        Left,
        Right,
        Top,
        Bottom
    }
    internal sealed class Snake
    {
        public Snake(int startX,int startY,int speed) 
        {
            PositionX = startX;
            PositionY = startY;
            Speed = speed;
        }

        public Ellipse ellipse { get; set; }

        public int PositionX;

        public int PositionY;
        public int Speed { get; set; }


        public Side MovingSide { get; set; }

        public Point Move() 
        {
            int oldPositionX = PositionX;
            int oldPositionY = PositionY;
            switch (this.MovingSide) 
            {
                case Side.Top:
                    PositionY -= 10;
                    break;
                case Side.Bottom:
                    PositionY += 10;
                    break;
                case Side.Left:
                    PositionX -= 10;
                    break;
                case Side.Right:
                    PositionX += 10;
                    break;
            }

            

            return new Point(oldPositionX, oldPositionY);
           
        }
        
        public Point TailMove(Point Way)
        {
            int oldPositionX = PositionX;
            int oldPositionY = PositionY;
            PositionX = (int)Way.X;
            PositionY=(int)Way.Y;
            return (new Point(oldPositionX,oldPositionY));
        }


    }
    //class SnakeTail
    //{
    //  public  SnakeTail(int Xpoz,int Ypoz)
    //    {
    //        positionX = Xpoz;
    //        positionY = Ypoz;
    //    }
    //    public int positionX { get; set; }
    //    public int positionY { get; set; }
    //}
}

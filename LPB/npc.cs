using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPB
{
    class npc
    {
        Random rnd = new Random();
        public int X;
        public int Y;
        public double Xto;
        public double Yto;
        public int Rotation;
        public npc(String type, int x, int y, int rotation)
        {

            if (type == "player")
            {
                //X = rnd.Next(0, 100);
                //Y = rnd.Next(0, 290);
                Rotation = 180;
            }
            else
            {
                //X = rnd.Next(200, 290);
                //Y = rnd.Next(0, 290);
                Rotation = 0;
            }

            X = x;
            Y = y;
            Xto = X;
            Yto = Y;
        }

        public void left()
        {
            Rotation = Rotation - 10;
            if (Rotation < 0)
            {
                Rotation = Rotation + 360;
            }
            Rotation = Rotation % 360;
        }
        public void right()
        {
            Rotation = Rotation + 10;
            Rotation = Rotation % 360;
        }
        public void move()
        {
            Xto = Xto + 3 * Math.Cos(Rotation * Math.PI / 180);
            Yto = Yto + 3 * Math.Sin(Rotation * Math.PI / 180);
            if (Xto > 290)
            {
                Xto = 290;
            }
            else if (Xto < 0)
            {
                Xto = 0;
            }
            if (Yto > 290)
            {
                Yto = 290;
            }
            else if (Yto < 0)
            {
                Yto = 0;
            }
            X = (int)Xto;
            Y = (int)Yto;
        }
    }
}

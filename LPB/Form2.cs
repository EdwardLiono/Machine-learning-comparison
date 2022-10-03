using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Random rnd = new Random();

        int[,] Player_data = new int[25, 3];
        int[,] Enemy_data = new int[25, 3];
        Bitmap Player = new Bitmap(LPB.Resource1.Player, 10,10);
        Bitmap Enemy = new Bitmap(LPB.Resource1.Enemy, 10, 10);
        npc[] a = new npc[25];
        npc[] b = new npc[25];
        int scorePlayer;
        int scoreEnemy;
        bool end;
        public double[,] moveweight = new double[30, 2];
        public double[,] moveweightE = new double[30, 2];
        int tick = 0;
        int code;
        bool battlemode = false;
        public int fitness;
        int commandleft = 0;
        int commandright = 0;
        int leftcode = 0;
        int rightcode = 0;
        bool aileft = true;
        //int lefta1 = 0;
        //int lefta2 = 0;
        //int leftb1 = 0;
        //int leftb2 = 0;
        //public bool done=false;
        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 25; i++)
            {
                int X = rnd.Next(190, 290);
                int Y = rnd.Next(0, 290);
                //X = 50;
                //Y = 200;
                int Rotation = rnd.Next(0, 360);
                a[i] = new npc("player", X, Y, Rotation);
            }
            for (int i = 0; i < 25; i++)
            {
                int X = rnd.Next(0, 100);
                int Y = rnd.Next(0, 290);
                //X = 250;
                //Y = 0;
                int Rotation = rnd.Next(0, 360);
                b[i] = new npc("enemy", X, Y, Rotation);
            }
            label1.Text = "0";
            label2.Text = "0";
            for (int i = 0; i < 30; i++)
            {
                moveweight[i, 0] = rnd.Next(0, 1000);
                moveweight[i, 1] = i;
            }

        }
        private void move1(int PC,int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = b[j].X;
                    y = b[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
            if (deg >= a[PC].Rotation - 180 && deg <= a[PC].Rotation - 30)
            {
                command(D, PC);
                end = true;
            }
            else if (deg >= a[PC].Rotation + 180 && deg <= a[PC].Rotation + 330)
            {
                command(D, PC);
                end = true;
            }
        }
        private void move1e(int PC,int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = a[j].X;
                    y = a[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
            if (deg >= b[PC].Rotation - 180 && deg <= b[PC].Rotation - 30)
            {
                command(D, PC);
                //b[PC].left();
                end = true;
                //leftb1++;
            }
            else if (deg >= b[PC].Rotation + 180 && deg <= b[PC].Rotation + 330)
            {
                command(D, PC);
                //b[PC].left();
                end = true;
                //leftb2++;
            }
        }
        private void move2(int PC, int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = b[j].X;
                    y = b[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
            if (deg >= a[PC].Rotation + 30 && deg <= a[PC].Rotation + 180)
            {
                command(D, PC);
                //a[PC].right();
                end = true;
            }
            else if (deg >= a[PC].Rotation - 330 && deg <= a[PC].Rotation - 180)
            {
                command(D, PC);
                //a[PC].right();
                end = true;
            }
        }
        private void move2e(int PC, int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = a[j].X;
                    y = a[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
            if (deg >= b[PC].Rotation + 30 && deg <= b[PC].Rotation + 180)
            {
                command(D, PC);
                //b[PC].right();
                end = true;
            }
            else if (deg >= b[PC].Rotation - 330 && deg <= b[PC].Rotation - 180)
            {
                command(D, PC);
                //b[PC].right();
                end = true;
            }
        }
        private void move3(int PC, int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = b[j].X;
                    y = b[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
            if (deg >= a[PC].Rotation - 30 && deg <= a[PC].Rotation + 30)
            {
                command(D, PC);
                end = true;
            }
            else if (deg >= a[PC].Rotation + 330 && deg <= a[PC].Rotation + 390)
            {
                command(D, PC);
                end = true;
            }
        }
        private void move3e(int PC, int D)
        {
            double Distance = 0;
            double x = 0;
            double y = 0;
            double distance2 = 10000;
            for (int j = 0; j < 25; j++)
            {
                Distance = Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2));
                if (distance2 > Distance)
                {
                    x = a[j].X;
                    y = a[j].Y;
                    distance2 = Distance;
                }
            }
            double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
            if (deg >= b[PC].Rotation - 30 && deg <= b[PC].Rotation + 30)
            {
                command(D, PC);
                end = true;
            }
            else if (deg >= b[PC].Rotation + 330 && deg <= b[PC].Rotation + 390)
            {
                command(D, PC);
                end = true;
            }
        }
        private void move4(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
            }
            if (AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation - 180 && deg <= a[PC].Rotation)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 180 && deg <= a[PC].Rotation + 360)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
            }
        }
        private void move4e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
            }
            if (AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation - 180 && deg <= b[PC].Rotation)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 180 && deg <= b[PC].Rotation + 360)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
            }
        }
        private void move5(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
            }
            if (AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation && deg <= a[PC].Rotation + 180)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 360 && deg <= a[PC].Rotation + 540)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
            }
        }
        private void move5e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
            }
            if (AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation && deg <= b[PC].Rotation + 180)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 360 && deg <= b[PC].Rotation + 540)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
            }
        }
        private void move6(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
                if (Math.Sqrt(Math.Pow(a[PC].X - a[j].X, 2) + Math.Pow(a[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE > AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation - 180 && deg <= a[PC].Rotation)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 180 && deg <= a[PC].Rotation + 360)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
            }
        }
        private void move6e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
                if (Math.Sqrt(Math.Pow(b[PC].X - b[j].X, 2) + Math.Pow(b[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE > AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation - 180 && deg <= b[PC].Rotation)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 180 && deg <= b[PC].Rotation + 360)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
            }
        }
        private void move7(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
                if (Math.Sqrt(Math.Pow(a[PC].X - a[j].X, 2) + Math.Pow(a[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE > AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation && deg <= a[PC].Rotation + 180)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 360 && deg <= a[PC].Rotation + 540)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
            }
        }
        private void move7e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
                if (Math.Sqrt(Math.Pow(b[PC].X - b[j].X, 2) + Math.Pow(b[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE > AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation && deg <= b[PC].Rotation + 180)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 360 && deg <= b[PC].Rotation + 540)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
            }
        }
        private void move8(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
                if (Math.Sqrt(Math.Pow(a[PC].X - a[j].X, 2) + Math.Pow(a[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation - 180 && deg <= a[PC].Rotation - 30)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 180 && deg <= a[PC].Rotation + 330)
                {
                    command(D, PC);
                    //a[PC].left();
                    end = true;
                }
            }
        }
        private void move8e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
                if (Math.Sqrt(Math.Pow(b[PC].X - b[j].X, 2) + Math.Pow(b[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation - 180 && deg <= b[PC].Rotation - 30)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 180 && deg <= b[PC].Rotation + 330)
                {
                    command(D, PC);
                    //b[PC].left();
                    end = true;
                }
            }
        }
        private void move9(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
                if (Math.Sqrt(Math.Pow(a[PC].X - a[j].X, 2) + Math.Pow(a[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation + 30 && deg <= a[PC].Rotation + 180)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 390 && deg <= a[PC].Rotation + 540)
                {
                    command(D, PC);
                    //a[PC].right();
                    end = true;
                }
            }
        }
        private void move9e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
                if (Math.Sqrt(Math.Pow(b[PC].X - b[j].X, 2) + Math.Pow(b[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation + 30 && deg <= b[PC].Rotation + 180)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 390 && deg <= b[PC].Rotation + 540)
                {
                    command(D, PC);
                    //b[PC].right();
                    end = true;
                }
            }
        }
        private void move10(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(a[PC].X - b[j].X, 2) + Math.Pow(a[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + b[j].X;
                    y = y + b[j].Y;
                }
                if (Math.Sqrt(Math.Pow(a[PC].X - a[j].X, 2) + Math.Pow(a[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(a[PC].Y - y, x - a[PC].X) / Math.PI * 180)) % 360;
                if (deg >= a[PC].Rotation - 30 && deg <= a[PC].Rotation + 30)
                {
                    command(D, PC);
                    end = true;
                }
                else if (deg >= a[PC].Rotation + 330 && deg <= a[PC].Rotation + 390)
                {
                    command(D, PC);
                    end = true;
                }
            }
        }
        private void move10e(int PC, int D)
        {
            int x = 0;
            int y = 0;
            int AmountE = 0;
            int AmountP = 0;
            for (int j = 0; j < 25; j++)
            {
                if (Math.Sqrt(Math.Pow(b[PC].X - a[j].X, 2) + Math.Pow(b[PC].Y - a[j].Y, 2)) < 50)
                {
                    AmountE++;
                    x = x + a[j].X;
                    y = y + a[j].Y;
                }
                if (Math.Sqrt(Math.Pow(b[PC].X - b[j].X, 2) + Math.Pow(b[PC].Y - b[j].Y, 2)) < 50)
                {
                    AmountP++;
                }
            }
            if (AmountE < AmountP && AmountE >= 1)
            {
                x = x / AmountE;
                y = y / AmountE;
                double deg = (360 - (Math.Atan2(b[PC].Y - y, x - b[PC].X) / Math.PI * 180)) % 360;
                if (deg >= b[PC].Rotation - 30 && deg <= b[PC].Rotation + 30)
                {
                    command(D, PC);
                    end = true;
                }
                else if (deg >= b[PC].Rotation + 330 && deg <= b[PC].Rotation + 390)
                {
                    command(D, PC);
                    end = true;
                }
            }
        }
        private void enemy_dead(int number)
        {
            //MessageBox.Show("");
            int X = rnd.Next(0, 100);
            int Y = rnd.Next(0, 290);
            //X = 250;
            //Y = 0;
            int Rotation = rnd.Next(0, 360);
            b[number] = new npc("enemy", X, Y, Rotation);
            scorePlayer = scorePlayer + 10;
            label2.Text = scorePlayer.ToString();

        }
        private void player_dead(int number)
        {
            //MessageBox.Show("");
            int X = rnd.Next(190, 290);
            int Y = rnd.Next(0, 290);
            //X = 50;
            //Y = 200;
            int Rotation = rnd.Next(0, 360);
            a[number] = new npc("player", X, Y, Rotation);
            scoreEnemy = scoreEnemy + 10;
            label1.Text = scoreEnemy.ToString();

        }
        private void AI()
        {
            end = false;

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (end == true)
                    {
                        end = false;
                        break;
                    }
                    if (moveweight[j, 1] == 0)
                    {
                        move1(i,1);
                    }
                    else if (moveweight[j, 1] == 1)
                    {
                        move2(i,1);
                    }
                    else if (moveweight[j, 1] == 2)
                    {
                        move3(i,1);
                    }
                    else if (moveweight[j, 1] == 3)
                    {
                        move4(i,1);
                    }
                    else if (moveweight[j, 1] == 4)
                    {
                        move5(i,1);
                    }
                    else if (moveweight[j, 1] == 5)
                    {
                        move6(i,1);
                    }
                    else if (moveweight[j, 1] == 6)
                    {
                        move7(i,1);
                    }
                    else if (moveweight[j, 1] == 7)
                    {
                        move8(i,1);
                    }
                    else if (moveweight[j, 1] == 8)
                    {
                        move9(i,1);
                    }
                    else if (moveweight[j, 1] == 9)
                    {
                        move10(i,1);
                    }
                    else if (moveweight[j, 1] == 10)
                    {
                        move1(i, 2);
                    }
                    else if (moveweight[j, 1] == 11)
                    {
                        move2(i, 2);
                    }
                    else if (moveweight[j, 1] == 12)
                    {
                        move3(i, 2);
                    }
                    else if (moveweight[j, 1] == 13)
                    {
                        move4(i, 2);
                    }
                    else if (moveweight[j, 1] == 14)
                    {
                        move5(i, 2);
                    }
                    else if (moveweight[j, 1] == 15)
                    {
                        move6(i, 2);
                    }
                    else if (moveweight[j, 1] == 16)
                    {
                        move7(i, 2);
                    }
                    else if (moveweight[j, 1] == 17)
                    {
                        move8(i, 2);
                    }
                    else if (moveweight[j, 1] == 18)
                    {
                        move9(i, 2);
                    }
                    else if (moveweight[j, 1] == 19)
                    {
                        move10(i,2);
                    }
                    else if (moveweight[j, 1] == 20)
                    {
                        move1(i, 3);
                    }
                    else if (moveweight[j, 1] == 21)
                    {
                        move2(i, 3);
                    }
                    else if (moveweight[j, 1] == 22)
                    {
                        move3(i, 3);
                    }
                    else if (moveweight[j, 1] == 23)
                    {
                        move4(i, 3);
                    }
                    else if (moveweight[j, 1] == 24)
                    {
                        move5(i, 3);
                    }
                    else if (moveweight[j, 1] == 25)
                    {
                        move6(i, 3);
                    }
                    else if (moveweight[j, 1] == 26)
                    {
                        move7(i, 3);
                    }
                    else if (moveweight[j, 1] == 27)
                    {
                        move8(i, 3);
                    }
                    else if (moveweight[j, 1] == 28)
                    {
                        move9(i, 3);
                    }
                    else if (moveweight[j, 1] == 29)
                    {
                        move10(i, 3);
                    }
                }


                for (int j = 0; j < 10; j++)
                {
                    if (end == true)
                    {
                        end = false;
                        break;
                    }
                    if (moveweightE[j, 1] == 0)
                    {
                        move1e(i,1);
                    }
                    else if (moveweightE[j, 1] == 1)
                    {
                        move2e(i,1);
                    }
                    else if (moveweightE[j, 1] == 2)
                    {
                        move3e(i,1);
                    }
                    else if (moveweightE[j, 1] == 3)
                    {
                        move4e(i,1);
                    }
                    else if (moveweightE[j, 1] == 4)
                    {
                        move5e(i,1);
                    }
                    else if (moveweightE[j, 1] == 5)
                    {
                        move6e(i,1);
                    }
                    else if (moveweightE[j, 1] == 6)
                    {
                        move7e(i,1);
                    }
                    else if (moveweightE[j, 1] == 7)
                    {
                        move8e(i,1);
                    }
                    else if (moveweightE[j, 1] == 8)
                    {
                        move9e(i,1);
                    }
                    else if (moveweightE[j, 1] == 9)
                    {
                        move10e(i,1);
                    }
                    else if (moveweightE[j, 1] == 10)
                    {
                        move1e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 11)
                    {
                        move2e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 12)
                    {
                        move3e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 13)
                    {
                        move4e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 14)
                    {
                        move5e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 15)
                    {
                        move6e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 16)
                    {
                        move7e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 17)
                    {
                        move8e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 18)
                    {
                        move9e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 19)
                    {
                        move10e(i, 2);
                    }
                    else if (moveweightE[j, 1] == 20)
                    {
                        move1e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 21)
                    {
                        move2e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 22)
                    {
                        move3e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 23)
                    {
                        move4e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 24)
                    {
                        move5e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 25)
                    {
                        move6e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 26)
                    {
                        move7e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 27)
                    {
                        move8e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 28)
                    {
                        move9e(i, 3);
                    }
                    else if (moveweightE[j, 1] == 29)
                    {
                        move10e(i, 3);
                    }
                }

            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < 25; i++)
            {
                g.TranslateTransform(a[i].X + Player.Width / 2, a[i].Y + Player.Height / 2);
                g.RotateTransform(a[i].Rotation);
                g.TranslateTransform(0 - a[i].X - Player.Height / 2, 0 - a[i].Y - Player.Height / 2);
                g.DrawImage(Player, a[i].X, a[i].Y);
                g.TranslateTransform(a[i].X + Player.Width / 2, a[i].Y + Player.Height / 2);
                g.RotateTransform(0 - a[i].Rotation);
                g.TranslateTransform(0 - a[i].X - Player.Width / 2, 0 - a[i].Y - Player.Height / 2);
                //g.DrawLine(new Pen(Color.Black), a[i].X, a[i].Y, a[i].X+10, a[i].Y + 10);
                //g.DrawEllipse(new Pen(Color.Black), a[i].X, a[i].Y, 10, 10);

            }
            for (int i = 0; i < 25; i++)
            {
                g.TranslateTransform(b[i].X + Enemy.Width / 2, b[i].Y + Enemy.Height / 2);
                g.RotateTransform(b[i].Rotation);
                g.TranslateTransform(0 - b[i].X - Enemy.Width / 2, 0 - b[i].Y - Enemy.Height / 2);
                g.DrawImage(Enemy, b[i].X, b[i].Y);
                g.TranslateTransform(b[i].X + Enemy.Width / 2, b[i].Y + Enemy.Height / 2);
                g.RotateTransform(0 - b[i].Rotation);
                g.TranslateTransform(0 - b[i].X - Enemy.Width / 2, 0 - b[i].Y - Enemy.Height / 2);
                //g.DrawLine(new Pen(Color.Black), b[i].X, b[i].Y, b[i].X+10, b[i].Y+10);
                //g.DrawEllipse(new Pen(Color.Black), b[i].X, b[i].Y, 10, 10);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.X.ToString() + " - " + e.Y.ToString());
            //a[0].move();
            //this.Invalidate();
            ////this.Invalidate();
        }

        private void Form2_Scroll(object sender, ScrollEventArgs e)
        {


        }

        private void Form2_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //a[0].left();
            //this.Invalidate();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.A)
            //{
            //    a[0].left();
            //} else if (e.KeyCode == Keys.D)
            //{
            //    a[0].right();
            //} else if (e.KeyCode == Keys.W)
            //{
            //    a[0].move();
            //}
        }
        public void setcode(int code1)
        {
            code = code1;
        }
        public void start()
        {
            timer1.Start();
        }
        public void setweight(double[,] weight)
        {
            for (int i = 0; i < 30; i++)
            {
                moveweight[i, 0] = weight[i, code];
                moveweight[i, 1] = i;
            }
        }
        public void setEweight(double[,] weight)
        {
            for (int i = 0; i < 30; i++)
            {
                moveweightE[i, 0] = weight[i, code];
                moveweightE[i, 1] = i;
            }
        }
        public void Sort()
        {
            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 29 - i; j++)
                {
                    double temp;
                    if (moveweight[j, 0] < moveweight[j + 1, 0])
                    {
                        temp = moveweight[j, 0];
                        moveweight[j, 0] = moveweight[j + 1, 0];
                        moveweight[j + 1, 0] = temp;
                        temp = moveweight[j, 1];
                        moveweight[j, 1] = moveweight[j + 1, 1];
                        moveweight[j + 1, 1] = temp;
                    }
                    if (moveweightE[j, 0] < moveweightE[j + 1, 0])
                    {
                        temp = moveweightE[j, 0];
                        moveweightE[j, 0] = moveweightE[j + 1, 0];
                        moveweightE[j + 1, 0] = temp;
                        temp = moveweightE[j, 1];
                        moveweightE[j, 1] = moveweightE[j + 1, 1];
                        moveweightE[j + 1, 1] = temp;
                    }
                }
            }
            //MessageBox.Show(""+moveweightE[0, 1]);
            //MessageBox.Show("" + moveweightE[1, 1]);
            //MessageBox.Show("" + moveweightE[2, 1]);
        }
        public void battle()
        {
            battlemode = true;
            timer1.Interval = 50;
        }
        public void setlabel(string left, string right)
        {
            label3.Visible = true;
            label3.Text = left;
            label4.Visible = true;
            label4.Text = right;
        }
        private void command(int command1, int code1)
        {
            if (aileft == true)
            {
                aileft = false;
                commandleft = command1;
                leftcode = code1;
            }
            else if (aileft == false)
            {
                aileft = true;
                commandright = command1;
                rightcode = code1;
                if (commandleft == 1)
                {
                    a[leftcode].left();
                }
                else if (commandleft == 2)
                {
                    a[leftcode].right();
                }
                if (commandright == 1)
                {
                    b[rightcode].left();
                }
                else if (commandright == 2)
                {
                    b[rightcode].right();
                }
                commandleft = 0;
                commandright = 0;
                leftcode = 0;
                rightcode = 0;
            }

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            //MessageBox.Show("Test");
            tick++;
            for (int i = 0; i < 25; i++)
            {
                a[i].move();
                b[i].move();
            }
            AI();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (Math.Sqrt(Math.Pow((a[i].X + 5) - (b[j].X + 5), 2) + Math.Pow((a[i].Y + 5) - (b[j].Y + 5), 2)) <= 10)
                    {
                        double deg = (360 - (Math.Atan2(a[i].Y - b[j].Y, b[j].X - a[i].X) / Math.PI * 180)) % 360;
                        double deg2 = (360 - (Math.Atan2(b[j].Y - a[i].Y, a[i].X - b[j].X) / Math.PI * 180)) % 360;
                        //MessageBox.Show(""+deg+" "+a[i].Rotation);
                        if (deg >= a[i].Rotation - 45 && deg <= a[i].Rotation + 45)
                        {
                            //deg = (360 - (Math.Atan2(b[j].Y - a[i].Y, a[i].X - b[j].X) / Math.PI * 180)) % 360;
                            if (deg2 >= b[j].Rotation - 45 && deg2 <= b[j].Rotation + 45)
                            {
                                player_dead(i);

                            }
                            else if (deg2 >= b[j].Rotation + 315 && deg2 <= b[j].Rotation + 405)
                            {
                                player_dead(i);
                            }
                            enemy_dead(j);
                        }
                        else if (deg >= a[i].Rotation + 315 && deg <= a[i].Rotation + 405)
                        {
                            //deg = (360 - (Math.Atan2(b[j].Y - a[i].Y, a[i].X - b[j].X) / Math.PI * 180)) % 360;
                            if (deg2 >= b[j].Rotation - 45 && deg2 <= b[j].Rotation + 45)
                            {
                                player_dead(i);
                            }
                            else if (deg2 >= b[j].Rotation + 315 && deg2 <= b[j].Rotation + 405)
                            {
                                player_dead(i);
                            }
                            enemy_dead(j);
                        }
                        else if (deg2 >= b[j].Rotation - 45 && deg2 <= b[j].Rotation + 45)
                        {
                            player_dead(i);
                        }
                        else if (deg2 >= b[j].Rotation + 315 && deg2 <= b[j].Rotation + 405)
                        {
                            player_dead(i);
                        }
                    }
                }
            }
            this.Invalidate();
            if (tick <= 300)
            {
                timer1.Start();
            }
            else if (battlemode == false)
            {
                for (int i = 0; i < 29; i++)
                {
                    for (int j = 0; j < 29 - i; j++)
                    {
                        double temp;
                        if (moveweight[j, 1] > moveweight[j + 1, 1])
                        {
                            temp = moveweight[j, 0];
                            moveweight[j, 0] = moveweight[j + 1, 0];
                            moveweight[j + 1, 0] = temp;
                            temp = moveweight[j, 1];
                            moveweight[j, 1] = moveweight[j + 1, 1];
                            moveweight[j + 1, 1] = temp;
                        }
                    }
                }
                //MessageBox.Show(moveweight[0,0].ToString());
                fitness = scorePlayer - (scoreEnemy / 2);
                Form1 parent = this.MdiParent as Form1;
                parent.calldone(code);
                this.Close();
                //((Form1)this.MdiParent).calldone(code);
            }
            else
            {
                //MessageBox.Show(lefta1 + "   " + lefta2 + "   " + leftb1 + "   " + leftb2);
                MessageBox.Show(scoreEnemy + "   " + scorePlayer);
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

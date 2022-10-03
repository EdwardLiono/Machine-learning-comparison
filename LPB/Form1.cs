using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LPB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        string EAtype;
        int done = 0;
        Form2[] a = new Form2[50];

        double[,] moveweight = new double[30, 50];
        double[,] moveweight1 = new double[30, 50];
        double[,] moveweightsort = new double[30, 2];
        double[,] moveweightERandom = new double[30, 50];
        double[,] moveweightright = new double[30, 2];
        double[,] moveweightleft = new double[30, 2];
        int[] fitness = new int[50];
        int[] fitness1 = new int[50];
        int[] code1 = new int[50];
        int[] visit = new int[25];
        int[] onlookerbee = new int[25];
        bool initialize = true;
        bool skip1 = false;
        bool skip2 = false;
        bool skip3 = false;
        bool sameg=true;
        string left = "";
        string right = "";
        int iteration = 0;
        private void newTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form2[] a= new Form2[50];
            //for (int i = 0; i < 50; i++)
            //{
            //    a[i] = new Form2();
            //    a[i].MdiParent = this;
            //    a[i].Show();
            //}



        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = "0";
            for (int i = 0; i < 50; i++)
            {
                moveweightERandom[0, i] = 114;
                moveweightERandom[1, i] = 647;
                moveweightERandom[2, i] = 3;
                moveweightERandom[3, i] = 425;
                moveweightERandom[4, i] = 185;
                moveweightERandom[5, i] = 131;
                moveweightERandom[6, i] = 911;
                moveweightERandom[7, i] = 971;
                moveweightERandom[8, i] = 835;
                moveweightERandom[9, i] = 448;
                moveweightERandom[10, i] = 751;
                moveweightERandom[11, i] = 10;
                moveweightERandom[12, i] = 857;
                moveweightERandom[13, i] = 413;
                moveweightERandom[14, i] = 617;
                moveweightERandom[15, i] = 670;
                moveweightERandom[16, i] = 439;
                moveweightERandom[17, i] = 355;
                moveweightERandom[18, i] = 784;
                moveweightERandom[19, i] = 55;
                moveweightERandom[20, i] = 935;
                moveweightERandom[21, i] = 552;
                moveweightERandom[22, i] = 554;
                moveweightERandom[23, i] = 876;
                moveweightERandom[24, i] = 77;
                moveweightERandom[25, i] = 520;
                moveweightERandom[26, i] = 197;
                moveweightERandom[27, i] = 873;
                moveweightERandom[28, i] = 158;
                moveweightERandom[29, i] = 555;
            }

        }

        private void continueTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        public void calldone(int code)
        {
            done++;
            if (initialize == true)
            {
                for (int i = 0; i < 25; i++)
                {
                    visit[i] = 0;
                }
                for (int i = 0; i < 30; i++)
                {
                    moveweight[i, code] = a[code].moveweight[i, 0];
                    //moveweight1[i, code] = a[code].moveweight[i, 0];
                }

                fitness[code] = a[code].fitness;
            }
            else
            {
                fitness1[code] = a[code].fitness;


            }
            if (iteration <= 1000 && sameg==true)
            {
                if (done == 50 && initialize == true || done == 25 && EAtype == "ABC" && initialize == false || done == 50 && EAtype == "Wolf Colony" || done == 50 && EAtype == "Firefly" || done == 50 && EAtype == "SOS")
                {
                    initialize = false;
                    done = 0;
                    for (int i = 0; i < 50; i++)
                    {
                        code1[i] = i;
                    }
                    if (EAtype == "ABC")
                    {
                        ABC();
                    }
                    else if (EAtype == "Wolf Colony")
                    {
                        wolf_colony();
                    }
                    else if (EAtype == "Firefly")
                    {
                        firefly();
                    }
                    else if (EAtype == "SOS")
                    {
                        SOS();
                    }
                }
            }

        }
        private void ABC()
        {
            if (skip1 == false)
            {
                skip1 = true;
                for (int i = 0; i < 25; i++)
                {
                    for (int l = 0; l < 30; l++)
                    {
                        moveweight1[l, i] = moveweight[l, i];
                    }
                    int j = rnd.Next(0, 30);
                    int k = rnd.Next(0, 25);
                    double temp = rnd.NextDouble() * rnd.Next(-1, 1);
                    moveweight1[j, i] = moveweight[j, i] + (temp * (moveweight[j, i] - moveweight[j, k]));
                }
                for (int i = 0; i < 25; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }

            }
            else if (skip2 == false)
            {
                //MessageBox.Show("test");
                skip2 = true;
                int sum = 0;
                for (int i = 0; i < 25; i++)
                {
                    if (fitness[i] < fitness1[i])
                    {
                        fitness[i] = fitness1[i];
                        visit[i] = 0;
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, i] = moveweight1[j, i];
                        }
                    }
                    else
                    {
                        visit[i]++;
                    }
                    sum = sum + fitness[i];
                }
                for (int i = 25; i < 50; i++)
                {
                    int roullete = rnd.Next(0, sum + 1);
                    int sum1 = 0;

                    for (int l = 0; l < 25; l++)
                    {
                        sum1 = sum1 + fitness[l];
                        if (sum1 > roullete)
                        {
                            onlookerbee[i - 25] = l;
                        }
                    }
                    int j = rnd.Next(0, 30);
                    int k = rnd.Next(0, 25);
                    double temp = rnd.NextDouble() * rnd.Next(-1, 1);
                    moveweight1[j, i] = moveweight[j, onlookerbee[i - 25]] + temp * (moveweight[j, onlookerbee[i - 25]] - moveweight[j, k]);
                }
                for (int i = 25; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setEweight(moveweightERandom);
                    a[i].setweight(moveweight);
                    a[i].Sort();
                    a[i].start();
                }

            }
            else
            {
                skip1 = false;
                skip2 = false;
                for (int i = 25; i < 50; i++)
                {
                    if (fitness[onlookerbee[i - 25]] < fitness1[i])
                    {
                        fitness[onlookerbee[i - 25]] = fitness1[i];
                        visit[onlookerbee[i - 25]] = 0;
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, onlookerbee[i - 25]] = moveweight1[j, i];
                        }
                    }
                    else
                    {
                        visit[onlookerbee[i - 25]]++;
                    }
                }
                for (int i = 0; i < 25; i++)
                {
                    if (visit[i] > 250)
                    {
                        visit[i] = 0;
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, i] = rnd.Next(0, 1000);
                        }

                    }
                }
                iteration++;
                label1.Text = iteration.ToString();

                if (iteration % 10 == 0)
                {
                    int fitnesmax = -100;
                    int code2 = 0;
                    for (int i = 0; i < 50; i++)
                    {
                        if (fitnesmax < fitness[i])
                        {
                            code2 = i;
                            fitnesmax = fitness[i];
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        moveweightsort[i, 0] = moveweight[i, code2];
                        moveweightsort[i, 1] = i;
                    }
                    for (int i = 0; i < 29; i++)
                    {
                        for (int j = 0; j < 29 - i; j++)
                        {
                            double temp;
                            if (moveweightsort[j, 0] < moveweightsort[j + 1, 0])
                            {
                                temp = moveweightsort[j, 0];
                                moveweightsort[j, 0] = moveweightsort[j + 1, 0];
                                moveweightsort[j + 1, 0] = temp;
                                temp = moveweightsort[j, 1];
                                moveweightsort[j, 1] = moveweightsort[j + 1, 1];
                                moveweightsort[j + 1, 1] = temp;
                            }
                        }
                    }
                    bool same = true;
                    int[] pastweight=new int[30];
                    int pastgen;
                    using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABCfitness.txt", true))
                    {
                        sw.WriteLine(fitnesmax);
                    }
                    using (StreamReader sr = new StreamReader("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABC.txt"))
                    {
                        string s;
                        s = sr.ReadLine();
                        pastgen = Convert.ToInt32(s);
                        for (int i = 0; i < 30; i++)
                        {
                            s = sr.ReadLine();
                            pastweight[i] = Convert.ToInt32(s);
                            if (pastweight[i] != moveweightsort[i, 1])
                            {
                                same = false;
                                break;
                            }
                        }
                        if (iteration-pastgen==30)
                        {
                            sameg = false;
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABC.txt", false))
                    {
                        if (same == false)
                        {
                            sw.WriteLine(iteration);
                            for (int i = 0; i < 30; i++)
                            {
                                sw.WriteLine(moveweightsort[i, 1]);
                            }
                        }
                        else
                        {
                            sw.WriteLine(pastgen);
                            for (int i = 0; i < 30; i++)
                            {
                                sw.WriteLine(pastweight[i]);
                            }
                        }
                        sw.WriteLine(iteration);
                        sw.WriteLine(code2);
                        for (int i = 0; i < 25; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                sw.WriteLine(moveweight[j, i]);
                            }
                            sw.WriteLine(visit[i]);
                        }
                        for (int i = 25; i < 50; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                sw.WriteLine(moveweight[j, i]);
                            }
                            sw.WriteLine("");
                        }
                        
                    }
                }
                ABC();
            }
        }


        private void wolf_colony()
        {
            double b = 2 - 2 * ((iteration + 1) / 1000);
            fitness = fitness1;
            for (int i = 0; i < 49; i++)
            {
                for (int j = 0; j < 49 - i; j++)
                {
                    int temp;
                    if (fitness[j] < fitness[j + 1])
                    {
                        temp = fitness[j];
                        fitness[j] = fitness[j + 1];
                        fitness[j + 1] = temp;
                        temp = code1[j];
                        code1[j] = code1[j + 1];
                        code1[j + 1] = code1[j];
                    }
                }
            }

            for (int i = 0; i < 50; i++)
            {
                int mutation = rnd.Next(0, 50);
                if (mutation == 0 && i != code1[0] && i != code1[1] && i != code1[2])
                {
                    for (int j = 0; j < 30; j++)
                    {
                        moveweight[j, code1[i]] = rnd.Next(0, 1000);
                    }
                }
                else
                {
                    for (int j = 0; j < 30; j++)
                    {
                        double da = Math.Abs(2 * rnd.NextDouble() * moveweight[j, code1[0]] - moveweight[j, i]);
                        double db = Math.Abs(2 * rnd.NextDouble() * moveweight[j, code1[1]] - moveweight[j, i]);
                        double dt = Math.Abs(2 * rnd.NextDouble() * moveweight[j, code1[2]] - moveweight[j, i]);
                        double x1 = moveweight[j, code1[0]] - (2 * b * rnd.NextDouble()-b) * da;
                        double x2 = moveweight[j, code1[1]] - (2 * b * rnd.NextDouble()-b) * db;
                        double x3 = moveweight[j, code1[2]] - (2 * b * rnd.NextDouble()-b) * dt;
                        moveweight[j, i] = (x1 + x2 + x3) / 3;
                    }
                }

            }
            for (int i = 0; i < 49; i++)
            {
                for (int j = 0; j < 49 - i; j++)
                {
                    int temp;
                    if (code1[j] > code1[j + 1])
                    {
                        temp = fitness[j];
                        fitness[j] = fitness[j + 1];
                        fitness[j + 1] = temp;
                        temp = code1[j];
                        code1[j] = code1[j + 1];
                        code1[j + 1] = code1[j];
                    }
                }
            }
            iteration++;
            label1.Text = iteration.ToString();
            if (iteration % 10 == 0)
            {
                int fitnesmax = -100;
                int code2 = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (fitnesmax < fitness[i])
                    {
                        code2 = i;
                        fitnesmax = fitness[i];
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    moveweightsort[i, 0] = moveweight[i, code2];
                    moveweightsort[i, 1] = i;
                }
                for (int i = 0; i < 29; i++)
                {
                    for (int j = 0; j < 29 - i; j++)
                    {
                        double temp;
                        if (moveweightsort[j, 0] < moveweightsort[j + 1, 0])
                        {
                            temp = moveweightsort[j, 0];
                            moveweightsort[j, 0] = moveweightsort[j + 1, 0];
                            moveweightsort[j + 1, 0] = temp;
                            temp = moveweightsort[j, 1];
                            moveweightsort[j, 1] = moveweightsort[j + 1, 1];
                            moveweightsort[j + 1, 1] = temp;
                        }
                    }
                }
                bool same = true;
                int[] pastweight = new int[30];
                int pastgen;
                using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolffitness.txt", true))
                {
                    sw.WriteLine(fitnesmax);
                }
                using (StreamReader sr = new StreamReader("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolf.txt"))
                {
                    string s;
                    s = sr.ReadLine();
                    pastgen = Convert.ToInt32(s);
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        pastweight[i] = Convert.ToInt32(s);
                        if (pastweight[i] != moveweightsort[i, 1])
                        {
                            same = false;
                            break;
                        }
                    }
                    if (iteration - pastgen == 30)
                    {
                        sameg = false;
                    }
                }
                using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolf.txt", false))
                {
                    if (same == false)
                    {
                        sw.WriteLine(iteration);
                        for (int i = 0; i < 30; i++)
                        {
                            sw.WriteLine(moveweightsort[i, 1]);
                        }
                    }
                    else
                    {
                        sw.WriteLine(pastgen);
                        for (int i = 0; i < 30; i++)
                        {
                            sw.WriteLine(pastweight[i]);
                        }
                    }
                    sw.WriteLine(iteration);
                    sw.WriteLine(code1[0]);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            sw.WriteLine(moveweight[j, i]);
                        }
                    }
                }
            }
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setweight(moveweight);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            //a[1].Show();
        }


        
       
        private void firefly()
        {
            fitness = fitness1;
            double gamma = rnd.NextDouble();
            double alpha = rnd.NextDouble();
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (fitness[i] < fitness[j])
                    {
                        int index = rnd.Next(0, 30);
                        double beta = Math.Pow(Math.E, -1 * gamma * (Math.Abs(moveweight[index, j] - moveweight[index, i])));
                        moveweight[index, i] += beta * (moveweight[index, j] - moveweight[index, i]) + (alpha * (rnd.NextDouble() - 0.5));

                    }
                }
            }


            iteration++;
            label1.Text = iteration.ToString();
            if (iteration % 10 == 0)
            {
                int fitnesmax = -100;
                int code2 = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (fitnesmax < fitness[i])
                    {
                        code2 = i;
                        fitnesmax = fitness[i];
                    }
                }
                bool same = true;
                int[] pastweight = new int[30];
                int pastgen;
                for (int i = 0; i < 30; i++)
                {
                    moveweightsort[i, 0] = moveweight[i, code2];
                    moveweightsort[i, 1] = i;
                }
                for (int i = 0; i < 29; i++)
                {
                    for (int j = 0; j < 29 - i; j++)
                    {
                        double temp;
                        if (moveweightsort[j, 0] < moveweightsort[j + 1, 0])
                        {
                            temp = moveweightsort[j, 0];
                            moveweightsort[j, 0] = moveweightsort[j + 1, 0];
                            moveweightsort[j + 1, 0] = temp;
                            temp = moveweightsort[j, 1];
                            moveweightsort[j, 1] = moveweightsort[j + 1, 1];
                            moveweightsort[j + 1, 1] = temp;
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Fireflyfitness.txt", true))
                {
                    sw.WriteLine(fitnesmax);
                }
                using (StreamReader sr = new StreamReader("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Firefly.txt"))
                {
                    string s;
                    s = sr.ReadLine();
                    pastgen = Convert.ToInt32(s);
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        pastweight[i] = Convert.ToInt32(s);
                        if (pastweight[i] != moveweightsort[i, 1])
                        {
                            same = false;
                            break;
                        }
                    }
                    if (iteration - pastgen == 30)
                    {
                        sameg = false;
                    }
                }
                using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Firefly.txt", false))
                {
                    if (same == false)
                    {
                        sw.WriteLine(iteration);
                        for (int i = 0; i < 30; i++)
                        {
                            sw.WriteLine(moveweightsort[i, 1]);
                        }
                    }
                    else
                    {
                        sw.WriteLine(pastgen);
                        for (int i = 0; i < 30; i++)
                        {
                            sw.WriteLine(pastweight[i]);
                        }
                    }
                    sw.WriteLine(iteration);
                    sw.WriteLine(code2);
                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            sw.WriteLine(moveweight[j, i]);
                        }
                    }
                }
            }
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setweight(moveweight);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
        }

        private void SOS()
        {

            if (skip1 == false)
            {
                int fitnesmax = -100;
                int code2 = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (fitnesmax < fitness[i])
                    {
                        code2 = i;
                        fitnesmax = fitness[i];
                    }
                }
                moveweight1 = moveweight;
                skip1 = true;
                for (int i = 0; i < 50; i++)
                {
                    int process = rnd.Next(0, 50);
                    while (process == i)
                    {
                        process = rnd.Next(0, 50);
                    }
                    int Bf1 = rnd.Next(1, 3);
                    int Bf2 = rnd.Next(1, 3);
                    double[] mutualfactor = new double[30];
                    for (int j = 0; j < 30; j++)
                    {
                        mutualfactor[j] = (moveweight1[j, i] + moveweight1[j, process]) / 2;
                        moveweight1[j, i] = moveweight1[j, i] + rnd.NextDouble() * (moveweight1[j, code2] - (mutualfactor[j] * Bf1));
                        moveweight1[j, process] = moveweight1[j, process] + rnd.NextDouble() * (moveweight1[j, code2] - (mutualfactor[j] * Bf2));
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight1);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
            }
            else if (skip2 == false)
            {
                skip2 = true;
                for (int i = 0; i < 50; i++)
                {
                    if (fitness1[i] > fitness[i])
                    {
                        fitness[i] = fitness1[i];
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, i] = moveweight1[j, i];
                        }
                    }
                }
                int fitnesmax = -100;
                int code2 = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (fitnesmax < fitness[i])
                    {
                        code2 = i;
                        fitnesmax = fitness[i];
                    }
                }
                moveweight1 = moveweight;
                for (int i = 0; i < 50; i++)
                {

                    int process = rnd.Next(0, 50);
                    while (process == i)
                    {
                        process = rnd.Next(0, 50);
                    }
                    double random = rnd.Next(-1, 2);
                    while (random == 0)
                    {
                        random = rnd.Next(-1, 2);
                    }
                    random = random * rnd.NextDouble();
                    for (int j = 0; j < 30; j++)
                    {
                        moveweight1[j, i] = moveweight1[j, i] + (random * (moveweight1[j, code2] - moveweight1[j, process]));
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight1);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
            }
            else if (skip3 == false)
            {
                skip3 = true;
                for (int i = 0; i < 50; i++)
                {
                    if (fitness1[i] > fitness[i])
                    {
                        fitness[i] = fitness1[i];
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, i] = moveweight1[j, i];
                        }
                    }
                }
                moveweight1 = moveweight;
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        moveweight1[j, i] = rnd.Next(0, 1000);
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight1);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
            }
            else
            {
                skip1 = false;
                skip2 = false;
                skip3 = false;
                for (int i = 0; i < 50; i++)
                {
                    if (fitness1[i] > fitness[i])
                    {
                        fitness[i] = fitness1[i];
                        for (int j = 0; j < 30; j++)
                        {
                            moveweight[j, i] = moveweight1[j, i];
                        }
                    }
                }
                int fitnesmax = -100;
                int code2 = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (fitnesmax < fitness[i])
                    {
                        code2 = i;
                        fitnesmax = fitness[i];
                    }
                }

                iteration++;
                label1.Text = iteration.ToString();
                if (iteration % 10 == 0)
                {
                    bool same = true;
                    int[] pastweight = new int[30];
                    int pastgen;
                    for (int i = 0; i < 30; i++)
                    {
                        moveweightsort[i, 0] = moveweight[i, code2];
                        moveweightsort[i, 1] = i;
                    }
                    for (int i = 0; i < 29; i++)
                    {
                        for (int j = 0; j < 29 - i; j++)
                        {
                            double temp;
                            if (moveweightsort[j, 0] < moveweightsort[j + 1, 0])
                            {
                                temp = moveweightsort[j, 0];
                                moveweightsort[j, 0] = moveweightsort[j + 1, 0];
                                moveweightsort[j + 1, 0] = temp;
                                temp = moveweightsort[j, 1];
                                moveweightsort[j, 1] = moveweightsort[j + 1, 1];
                                moveweightsort[j + 1, 1] = temp;
                            }
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOSfitness.txt", true))
                    {
                        sw.WriteLine(fitnesmax);
                    }
                    using (StreamReader sr = new StreamReader("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOS.txt"))
                    {
                        string s;
                        s = sr.ReadLine();
                        pastgen = Convert.ToInt32(s);
                        for (int i = 0; i < 30; i++)
                        {
                            s = sr.ReadLine();
                            pastweight[i] = Convert.ToInt32(s);
                            if (pastweight[i] != moveweightsort[i, 1])
                            {
                                same = false;
                                break;
                            }
                        }
                        if (iteration - pastgen == 30)
                        {
                            sameg = false;
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOS.txt", false))
                    {

                        if (same == false)
                        {
                            sw.WriteLine(iteration);
                            for (int i = 0; i < 30; i++)
                            {
                                sw.WriteLine(moveweightsort[i, 1]);
                            }
                        }
                        else
                        {
                            sw.WriteLine(pastgen);
                            for (int i = 0; i < 30; i++)
                            {
                                sw.WriteLine(pastweight[i]);
                            }
                        }
                        sw.WriteLine(iteration);
                        sw.WriteLine(code2);
                        for (int i = 0; i < 50; i++)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                sw.WriteLine(moveweight[j, i]);
                            }
                        }
                    }
                }
                SOS();
            }
        }

        private void enemyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        

        private void playerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aBCToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Form2[] a = new Form2[50];
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            EAtype = "ABC";
        }

        private void wolfColonyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            EAtype = "Wolf Colony";
        }

        private void fireflyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            EAtype = "Firefly";
        }

        private void sOSToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            EAtype = "SOS";
        }

        private void aBCToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABC.txt"))
            {
                string s;
                for (int i = 0; i < 31; i++)
                {
                    s = sr.ReadLine();
                }
                s = sr.ReadLine();
                iteration = Convert.ToInt32(s);
                s = sr.ReadLine();
                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        s = sr.ReadLine();
                        moveweight[j, i] = Convert.ToDouble(s);
                    }
                    s = sr.ReadLine();
                    visit[i] = Convert.ToInt32(s);
                }
                for (int i = 25; i < 50; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        s = sr.ReadLine();
                        moveweight[j, i] = Convert.ToDouble(s);
                    }
                    s = sr.ReadLine();
                }
            }
            for (int i = 0; i < 25; i++)
            {
                a[i] = new Form2();
                a[i].MdiParent = this;
                //this.LayoutMdi(MdiLayout.TileHorizontal);
                a[i].Show();
                a[i].setcode(i);
                a[i].setweight(moveweight);
                a[i].setEweight(moveweightERandom);
                a[i].Sort();
                a[i].start();
            }
            EAtype = "ABC";
            initialize = false;
            label1.Text = iteration.ToString();
        }

        private void wolfColonyToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolf.txt"))
            {
                string s;
                for (int i = 0; i < 31; i++)
                {
                    s = sr.ReadLine();
                }
                s = sr.ReadLine();
                iteration = Convert.ToInt32(s);
                s = sr.ReadLine();
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        s = sr.ReadLine();
                        moveweight[j, i] = Convert.ToDouble(s);
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
                EAtype = "Wolf Colony";
                initialize = false;
                label1.Text = iteration.ToString();
            }
        }

        private void fireflyToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Firefly.txt"))
            {
                string s;
                s = sr.ReadLine();
                for (int i = 0; i < 31; i++)
                {
                    s = sr.ReadLine();
                }
                iteration = Convert.ToInt32(s);
                s = sr.ReadLine();
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        s = sr.ReadLine();
                        moveweight[j, i] = Convert.ToDouble(s);
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
                EAtype = "Firefly";
                initialize = false;
                label1.Text = iteration.ToString();
            }
        }

        private void sOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOS.txt"))
            {
                string s;
                for (int i = 0; i < 31; i++)
                {
                    s = sr.ReadLine();
                }
                s = sr.ReadLine();
                s = sr.ReadLine();
                int code = Convert.ToInt32(s);
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        s = sr.ReadLine();
                        moveweight[j, i] = Convert.ToDouble(s);
                    }
                }

                for (int i = 0; i < 50; i++)
                {
                    a[i] = new Form2();
                    a[i].MdiParent = this;
                    //this.LayoutMdi(MdiLayout.TileHorizontal);
                    a[i].Show();
                    a[i].setcode(i);
                    a[i].setweight(moveweight);
                    a[i].setEweight(moveweightERandom);
                    a[i].Sort();
                    a[i].start();
                }
                EAtype = "SOS";
                initialize = false;
                label1.Text = iteration.ToString();
            }
        }

        private void startToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (left == "ABC")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABC.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                        s = sr.ReadLine();
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightleft[i, 0] = Convert.ToDouble(s);
                        moveweightleft[i, 1] = i;
                    }
                }
            }
            else if (left == "Wolf Colony")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolf.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }

                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightleft[i, 0] = Convert.ToDouble(s);
                        moveweightleft[i, 1] = i;
                    }
                }
            }
            else if (left == "Firefly")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Firefly.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightleft[i, 0] = Convert.ToDouble(s);
                        moveweightleft[i, 1] = i;
                    }
                }
            }
            else if (left == "SOS")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOS.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightleft[i, 0] = Convert.ToDouble(s);
                        moveweightleft[i, 1] = i;
                    }
                }
            }


            if (right == "ABC")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\ABC.txt"))
                {
                    string s;
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                        s = sr.ReadLine();
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightright[i, 0] = Convert.ToDouble(s);
                        moveweightright[i, 1] = i;
                    }
                }
            }
            else if (right == "Wolf Colony")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Wolf.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightright[i, 0] = Convert.ToDouble(s);
                        moveweightright[i, 1] = i;
                    }
                }
            }
            else if (right == "Firefly")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\Firefly.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightright[i, 0] = Convert.ToDouble(s);
                        moveweightright[i, 1] = i;
                    }
                }
            }
            else if (right == "SOS")
            {
                using (StreamReader sr = File.OpenText("C:\\Users\\Edward\\source\\repos\\LPB\\LPB\\SOS.txt"))
                {
                    string s;
                    for (int i = 0; i < 31; i++)
                    {
                        s = sr.ReadLine();
                    }
                    s = sr.ReadLine();
                    s = sr.ReadLine();
                    int code = Convert.ToInt32(s);
                    for (int i = 0; i < code - 1; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            s = sr.ReadLine();
                        }
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        s = sr.ReadLine();
                        moveweightright[i, 0] = Convert.ToDouble(s);
                        moveweightright[i, 1] = i;
                    }
                }
            }

            Form2 battle = new Form2();
            battle.MdiParent = this;
            //this.LayoutMdi(MdiLayout.TileHorizontal);
            battle.Show();
            battle.battle();
            battle.setEweight(moveweightright);
            battle.setweight(moveweightleft);
            battle.Sort();
            battle.setlabel(left, right);
            battle.start();
        
        }

        private void aBCToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            left = "ABC";
        }

        private void wolfToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            left = "Wolf Colony";
        }

        private void fireflyToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            left = "Firefly";
        }

        private void sOSToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            left = "SOS";
        }

        private void aBCToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            right = "ABC";
        }

        private void wolfColonyToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            right = "Wolf Colony";
        }

        private void fireflyToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            right = "Firefly";
        }

        private void sOSToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            right = "SOS";
        }
    }
}

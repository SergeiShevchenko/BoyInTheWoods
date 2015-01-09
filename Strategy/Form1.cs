using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Strategy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            l.LoadAMap("bigmap.txt");
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            pictureBox1.Width = diametr * l.PixWidth;
            pictureBox1.Height = diametr * l.PixHeight;
            pictureBox1.Invalidate();
            MoveTimer.Interval = 50;
            MoveTimer.Tick += new EventHandler(MoveTimer_Tick);
            GhostTimer.Tick += new EventHandler(GhostTimer_Tick);
            Invalidator.Tick += new EventHandler(Invali_Tick);
            Invalidator.Enabled = true;
            Invalidator.Interval=50;
            GhostTimer.Enabled = true;
            GhostTimer.Interval = 100;
            l.h.ChangeFaceCollection(l.HeroDown);
            FX = 0;
            FY = 0;
            TX = FX + diametr;
            TY = FY + diametr;
        }

        Timer Invalidator = new Timer();
        Land l = new Land();      

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            l.DrawSprites(e.Graphics, FX, FY, TX, TY);
        }

        List<Point> p;

        bool moving = false;
        bool changedDirection = false;
        int x3, y3;

        int FX, FY, TX, TY, diametr=10;

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (!moving))
            {
                moving = true;
                p = l.FindPath(l.h.x, l.h.y, l.FromXY(e.X, e.Y).x + FX, l.FromXY(e.X, e.Y).y + FY);
                MoveIt();
            }
            else
            {
                changedDirection = true;
                x3 = e.X;
                y3 = e.Y;
            }
        }

        int x2,  x1,  y1,  y2;
        Timer MoveTimer = new Timer();
        Timer GhostTimer = new Timer();
        
        void UpdateThePickups()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Pickups:");
            foreach (Pickup pick in l.h.PickupList)
            {
                listBox1.Items.Add(pick.PickupType + ' ' + pick.PickupQuantity.ToString());
            }
            listBox1.Items.Add("Health:" + l.h.Health);
        }
        
        public void MoveIt()
        {
            if (p.Count > 0)
            {
                Point cur = p[0];
                x1 = l.h.x;
                x2 = cur.x;
                y1 = l.h.y;
                y2 = cur.y;
                if ((x2 < x1) && (y2 == y1)) { l.h.direction = 'L'; l.h.ChangeFaceCollection(l.HeroLeft); }
                else if ((x2 > x1) && (y2 == y1)) { l.h.direction = 'R'; l.h.ChangeFaceCollection(l.HeroRight); }
                else if ((x2 == x1) && (y2 < y1)) { l.h.direction = 'U'; l.h.ChangeFaceCollection(l.HeroUp); }
                else if ((x2 == x1) && (y2 > y1)) { l.h.direction = 'D'; l.h.ChangeFaceCollection(l.HeroDown); }
                l.h.actualX = l.toxy(x1, y1).x;
                l.h.actualY = l.toxy(x1, y1).y;
                MoveTimer.Enabled = true;
                Invalidator.Enabled = false;
                p.Remove(p[0]);
            }
            else
                moving = false;
            if ((l.h.x - FX) <= 2)
            {
                FX = l.h.x - diametr / 2; TX = FX + diametr;
            }
            else if ((TX - l.h.x) <= 2)
            {
                TX = l.h.x + diametr / 2; FX = TX - diametr;
            }
            if (FX < 0) 
            { 
                FX = 0; 
                TX = FX + diametr; 
            }
            if (TX > l.width) 
            { 
                TX = l.width; 
                FX = TX - diametr; 
            } 

            if ((l.h.y - FY) <= 2) 
            { 
                FY = l.h.y - diametr / 2; 
                TY = FY + diametr; 
            }
            else if ((TY - l.h.y) <= 2) { 
                TY = l.h.y + diametr / 2; 
                FY = TY-diametr;
            }
            if (FY < 0) 
            { 
                FY = 0; 
                TY = FY + diametr; 
            }
            if (TY > l.height) 
            { 
                TY = l.height; 
                FY = TY - diametr; 
            }            
            l.PickTheThingUp(
                delegate
                {
                    //pictureBox1.Invalidate();
                    UpdateThePickups();
                }
            );                     
        }

        public void CheckTheGhosts()
        {
            foreach (Ghost gh in l.GhostList)
            {
                if (gh.wheretoX < 0 || (gh.actualX == gh.wheretoX * l.PixWidth && gh.actualY == gh.wheretoY * l.PixWidth))
                {
                    int dist = l.Distance(gh.x, gh.y, l.h.x, l.h.y);
                    if (dist <= gh.seeing_distance)
                    {
                        if (dist <= gh.hitdistance)
                        {
                            if (gh.faceCollection[0] != gh.attackCollection[0])
                            {
                                gh.ChangeFaceCollection(gh.attackCollection);
                                l.h.Health -= gh.hitpoints;
                            }
                            label1.Text = "Health:" + l.h.Health;
                            gh.wheretoX = gh.x;
                            gh.wheretoY = gh.y;

                        }
                        else
                        {
                            List<Point> gp = l.FindPath(gh.x, gh.y, l.h.x, l.h.y);
                            if (gp.Count == 0)
                            {
                                gh.wheretoX = -1;
                                gh.wheretoY = -1;
                                return;
                            }
                            gh.wheretoX = gp[0].x;
                            gh.wheretoY = gp[0].y;
                            if ((gh.wheretoX < gh.x)) { gh.direction = 'L'; if (gh.faceCollection[0] != gh.leftfaceCollection[0]) gh.ChangeFaceCollection(gh.leftfaceCollection); }
                            else if ((gh.wheretoX > gh.x)) { gh.direction = 'R'; if (gh.faceCollection[0] != gh.rightfaceCollection[0]) gh.ChangeFaceCollection(gh.rightfaceCollection); }
                            else if ((gh.wheretoY < gh.y)) { gh.direction = 'U'; }
                            else if ((gh.wheretoY > gh.y)) { gh.direction = 'D'; }
                        }
                    }
                    else
                    {
                        gh.wheretoX = -1;
                        gh.wheretoY = -1;
                    }
                }
            }
        }

        public void GhostTimer_Tick(object sender, EventArgs e)
        {
            foreach (Ghost gh in l.GhostList)
            {
                foreach (Ghost gh2 in l.GhostList)
                    if ((gh.wheretoX == gh2.wheretoX) && (gh.wheretoY == gh2.wheretoY) && (gh != gh2)) gh.wheretoX = -1;
                if (gh.wheretoX >= 0)
                {
                    if ((gh.actualX != (gh.wheretoX * l.PixWidth)) || (gh.actualY != (gh.wheretoY * l.PixWidth)))
                    {
                        switch (gh.direction)
                        {
                            case 'L':
                                gh.actualX -= gh.speed;
                                break;
                            case 'R':
                                gh.actualX += gh.speed;
                                break;
                            case 'U':
                                gh.actualY -= gh.speed;
                                break;
                            case 'D':
                                gh.actualY += gh.speed;
                                break;
                        }
                    }
                    if ((gh.actualX == (gh.wheretoX * l.PixWidth)) && (gh.actualY == (gh.wheretoY * l.PixWidth)))
                    {
                        gh.x = gh.wheretoX;
                        gh.y = gh.wheretoY;
                        gh.wheretoX = -1;
                    }
                }
                gh.ChangeFace();
            }
            CheckTheGhosts();
        }

        public void Invali_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        public void MoveTimer_Tick(object sender, EventArgs e)
        {
            if ((l.h.actualX != (x2 * l.PixWidth)) || (l.h.actualY != (y2 * l.PixWidth)))
            {
                switch (l.h.direction)
                {
                    case 'L':
                        l.h.actualX -= 5;
                        break;
                    case 'R':
                        l.h.actualX += 5;
                        break;
                    case 'U':
                        l.h.actualY -= 5;
                        break;
                    case 'D':
                        l.h.actualY += 5;
                        break;
                }
                l.h.ChangeFace();
                pictureBox1.Invalidate();
            }
            if (l.h.actualX == x2 * l.PixWidth && l.h.actualY == y2 * l.PixWidth)            
            {
                MoveTimer.Enabled = false;
                Invalidator.Enabled = true;
                l.h.x = x2;
                l.h.y = y2;
                if (changedDirection)
                {
                    p = l.FindPath(l.h.x, l.h.y, l.FromXY(x3, y3).x + FX, l.FromXY(x3, y3).y + FY);
                    changedDirection = false;
                }
                MoveIt();
             }
        }
    }
}

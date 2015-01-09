using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Strategy
{
    public class Ghost
    {
        public int x, y;
        public int actualX, actualY;
        public Bitmap face;
        public Bitmap[] leftfaceCollection;
        public Bitmap[] rightfaceCollection;
        public Bitmap[] attackCollection;
        public Bitmap[] faceCollection;
        public char direction = 'S';
        public int wheretoX, wheretoY;
        public int hitpoints;
        public int hitdistance;
        public int speed;
        public int seeing_distance;
        int frames;


        public Ghost(int X, int Y, string type)
        {
            x = X;
            y = Y;
            if (type == "Ghost")
            {
                seeing_distance = 6;
                hitpoints = 15;
                hitdistance = 1;
                speed = 5;
                faceCollection = new Bitmap[4];
                leftfaceCollection = new Bitmap[4] 
                {
                    new Bitmap(Image.FromFile("Ghost2.png")).Clone(new Rectangle(0, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("Ghost2.png")).Clone(new Rectangle(44, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Ghost2.png")).Clone(new Rectangle(88, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Ghost2.png")).Clone(new Rectangle(132, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                rightfaceCollection = new Bitmap[4] 
                {
                    new Bitmap(Image.FromFile("Ghost1.png")).Clone(new Rectangle(0, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("Ghost1.png")).Clone(new Rectangle(44, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Ghost1.png")).Clone(new Rectangle(88, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Ghost1.png")).Clone(new Rectangle(132, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                attackCollection = new Bitmap[4] 
                {
                    new Bitmap(Image.FromFile("GhostAttack1.png")).Clone(new Rectangle(0, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("GhostAttack1.png")).Clone(new Rectangle(44, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("GhostAttack2.png")).Clone(new Rectangle(0, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("GhostAttack2.png")).Clone(new Rectangle(44, 0, 44, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                foreach (Bitmap b in leftfaceCollection)
                    b.MakeTransparent(Color.White);
                foreach (Bitmap b in rightfaceCollection)
                    b.MakeTransparent(Color.White);
                foreach (Bitmap b in attackCollection)
                    b.MakeTransparent(Color.White);
            }
            else if (type == "Drunk")
            {
                seeing_distance = 10;
                hitpoints = 5;
                hitdistance = 2;
                speed = 5;
                faceCollection = new Bitmap[6];
                leftfaceCollection = new Bitmap[6] 
                {
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(0, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(32, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(64, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(96, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(128, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk2.png")).Clone(new Rectangle(160, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                rightfaceCollection = new Bitmap[6] 
                {
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(0, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(32, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(64, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(96, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(128, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("Drunk1.png")).Clone(new Rectangle(160, 0, 32, 44), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                attackCollection = new Bitmap[4] 
                {
                    new Bitmap(Image.FromFile("DrunkAttack.png")).Clone(new Rectangle(0, 0, 44, 46), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("DrunkAttack.png")).Clone(new Rectangle(44, 0, 44, 46), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("DrunkAttack.png")).Clone(new Rectangle(88, 0, 44, 46), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                    new Bitmap(Image.FromFile("DrunkAttack.png")).Clone(new Rectangle(132, 0, 44, 46), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                foreach (Bitmap b in leftfaceCollection)
                    b.MakeTransparent(Color.Black);
                foreach (Bitmap b in rightfaceCollection)
                    b.MakeTransparent(Color.Black);
                foreach (Bitmap b in attackCollection)
                    b.MakeTransparent(Color.Black);

            }
            else if (type == "Cyclop")
            {
                seeing_distance = 50;
                hitpoints = 50;
                hitdistance = 1;
                speed = 1;
                faceCollection = new Bitmap[12];
                leftfaceCollection = new Bitmap[2] 
                {
                    new Bitmap(Image.FromFile("CyclopLeft.png")).Clone(new Rectangle(0, 0, 60, 80), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopLeft.png")).Clone(new Rectangle(60, 0, 60, 80), System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                };

                rightfaceCollection = new Bitmap[2] 
                {
                    new Bitmap(Image.FromFile("CyclopRight.png")).Clone(new Rectangle(0, 0, 60, 80), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopRight.png")).Clone(new Rectangle(60, 0, 60, 80), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),
                };

                attackCollection = new Bitmap[12] 
                {
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(0, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(70, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(140, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(210, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(280, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(350, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(420, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(490, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(560, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(630, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(700, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                    new Bitmap(Image.FromFile("CyclopAttack.png")).Clone(new Rectangle(770, 0, 70, 92), System.Drawing.Imaging.PixelFormat.Format32bppPArgb),                 
                };

                foreach (Bitmap b in leftfaceCollection)
                    b.MakeTransparent(Color.Black);
                foreach (Bitmap b in rightfaceCollection)
                    b.MakeTransparent(Color.Black);
                foreach (Bitmap b in attackCollection)
                    b.MakeTransparent(Color.Black);

            }

            ChangeFaceCollection(leftfaceCollection);
            wheretoX = -1;
            face = faceCollection[0];
        }

        public void ChangeFaceCollection(Bitmap[] fc)
        {
            for (int i = 0; i < fc.Count(); i++)
            {
                faceCollection[i] = fc[i];
            }
            face = faceCollection[0];
            frames = fc.Count();
        }

        public void ChangeFace()
        {
            for (int i = 0; i < frames - 1 ; i++)
            {
                if (face == faceCollection[i])
                {
                    face = faceCollection[i+1];
                    return;
                }
            }
            face = faceCollection[0];
        }
        
    }
}

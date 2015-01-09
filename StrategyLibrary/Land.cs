using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Strategy
{
    public class Land
    {
        public Land()
        {
            LoadGraphics();
        }

        public void LoadGraphics()
        {
            //Hero
            HeroLeft = new Bitmap[3];
            HeroRight = new Bitmap[3];
            HeroUp = new Bitmap[3];
            HeroDown = new Bitmap[3];
            HeroLeft[0] = new Bitmap(Image.FromFile("HeroLeft.png")).Clone(new Rectangle(0, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroLeft[1] = new Bitmap(Image.FromFile("HeroLeft.png")).Clone(new Rectangle(32, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroLeft[2] = new Bitmap(Image.FromFile("HeroLeft.png")).Clone(new Rectangle(64, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroRight[0] = new Bitmap(Image.FromFile("HeroRight.png")).Clone(new Rectangle(0, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroRight[1] = new Bitmap(Image.FromFile("HeroRight.png")).Clone(new Rectangle(32, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroRight[2] = new Bitmap(Image.FromFile("HeroRight.png")).Clone(new Rectangle(64, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroUp[0] = new Bitmap(Image.FromFile("HeroUp.png")).Clone(new Rectangle(0, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroUp[1] = new Bitmap(Image.FromFile("HeroUp.png")).Clone(new Rectangle(32, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroUp[2] = new Bitmap(Image.FromFile("HeroUp.png")).Clone(new Rectangle(64, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroDown[0] = new Bitmap(Image.FromFile("HeroDown.png")).Clone(new Rectangle(0, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroDown[1] = new Bitmap(Image.FromFile("HeroDown.png")).Clone(new Rectangle(32, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            HeroDown[2] = new Bitmap(Image.FromFile("HeroDown.png")).Clone(new Rectangle(64, 0, 32, 32), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            foreach (Bitmap b in HeroLeft)
                b.MakeTransparent(Color.White);
            foreach (Bitmap b in HeroRight)
                b.MakeTransparent(Color.White);
            foreach (Bitmap b in HeroUp)
                b.MakeTransparent(Color.White);
            foreach (Bitmap b in HeroDown)
                b.MakeTransparent(Color.White);
            //Hero

            //Environment

            grassSprite = new Bitmap(Image.FromFile("Grass.png"));            
            treeSprite = new Bitmap(Image.FromFile("Tree.png"));
            tree2Sprite = new Bitmap(Image.FromFile("Tree2.png"));
            waterSprite = new Bitmap(Image.FromFile("Water.png"));
            stoneSprite = new Bitmap(Image.FromFile("Stone.png"));
            bushSprite = new Bitmap(Image.FromFile("Bush.png"));

            grassSprite.MakeTransparent(Color.White);
            treeSprite.MakeTransparent(Color.White);
            tree2Sprite.MakeTransparent(Color.White);
            waterSprite.MakeTransparent(Color.White);
            stoneSprite.MakeTransparent(Color.White);
            bushSprite.MakeTransparent(Color.White);
        }

        public int width; 
        public int height;
        public MapPixel[,] FullMap;
        public int PixWidth = 50;
        public int PixHeight = 50;
        public Hero h;
        public List<Ghost> GhostList = new List<Ghost>();

        public static Bitmap grassSprite;
        public static Bitmap treeSprite;
        public static Bitmap tree2Sprite;
        public static Bitmap waterSprite;
        public static Bitmap stoneSprite;
        public static Bitmap bushSprite;

        public Bitmap[] HeroLeft;
        public Bitmap[] HeroRight;
        public Bitmap[] HeroUp;
        public Bitmap[] HeroDown;

        public void BuildAMap()
        {
            FullMap = new MapPixel[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    FullMap[j, i] = new MapPixel();
                    if ((i == 0) || (i == height - 1)) FullMap[j, i].ChangeContent(Content.Bounds);
                    if ((j == width - 1) || (j == 0)) FullMap[j, i].ChangeContent(Content.Bounds);
                }
            }
            h = new Hero(1,1);
            h.actualX = h.x * PixWidth;
            h.actualY = h.y * PixHeight;
            h.face = HeroLeft[1];
        }

        public void SaveAMap(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(width.ToString() + ' ' + height.ToString());
            for (int i = 1; i < height-1; i++)
            {
                for (int j = 1; j < width-1; j++)
                {
                    if (FullMap[j, i].Content == Content.Tree)
                        sw.WriteLine("Obstacle Tree "+j.ToString()+' '+ i.ToString());
                    if (FullMap[j, i].pickup != null)
                        sw.WriteLine("Pickup " + FullMap[j, i].pickup.PickupType + ' ' + FullMap[j, i].pickup.PickupQuantity.ToString() + ' ' + j.ToString() + ' ' + i.ToString());
                }
            }
            sw.WriteLine("Hero " + h.x.ToString() + ' ' + h.y.ToString());
            sw.Close();
        }

        public void LoadAMap(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string s;
            string[] tokens;
            s = sr.ReadLine();
            tokens = s.Split(' ');
            width = Convert.ToInt32(tokens[0]);
            height = Convert.ToInt32(tokens[1]);
            FullMap = new MapPixel[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    FullMap[j, i] = new MapPixel();
                    if ((i == 0) || (i == height - 1)) FullMap[j, i].ChangeContent(Content.Bounds);
                    if ((j == width - 1) || (j == 0)) FullMap[j, i].ChangeContent(Content.Bounds);
                }
            }
            while ((s = sr.ReadLine()) != null)
            {
                tokens = s.Split(' ');
                if (tokens[0] == "Obstacle")
                    FullMap[Convert.ToInt32(tokens[2]), Convert.ToInt32(tokens[3])].ChangeContent(
                        (Content)Enum.Parse(typeof(Content), tokens[1])
                    );
                else if (tokens[0] == "Hero")
                {
                    h = new Hero(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]));                   
                }
                else if (tokens[0] == "Pickup")
                {
                    FullMap[Convert.ToInt32(tokens[3]), Convert.ToInt32(tokens[4])].pickup = new Pickup(tokens[1], Convert.ToInt32(tokens[2]));                   
                }
                else if (tokens[0] == "Ghost")
                {
                    GhostList.Add(new Ghost(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]), tokens[3]));
                }
            }
            h.actualX = h.x * PixWidth;
            h.actualY = h.y * PixHeight;
            //h.face = HeroLeft[1];
            foreach (Ghost g in GhostList)
            {
                g.actualX = g.x * PixWidth;
                g.actualY = g.y * PixHeight;
            }
        }

        public Point toxy(int x, int y)
        {
            return (new Point(x * PixWidth, y * PixHeight));
        }

        public int tox(int x)
        {
            return (x * PixWidth);
        }

        public int toy(int y)
        {
            return (y * PixHeight);
        }

        public Point FromXY(int x, int y)
        {
            return (new Point(x / PixWidth, y / PixHeight));
        }

        public int Distance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Round(Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
        }

        public void DrawSprites(Graphics g, int FromX, int FromY, int ToX, int ToY)
        {
            for (int i = FromY; i < ToY; i++)
            {
                for (int j = FromX; j < ToX; j++)
                {
                    g.DrawImage(grassSprite, tox(j-FromX), toy(i-FromY), PixWidth, PixHeight);
                }
            }
            for (int i = FromY; i < ToY; i++)
            {
                for (int j = FromX; j < ToX; j++)
                {
                    if (FullMap[j, i].free != true)
                    {
                        g.DrawImage(FullMap[j, i].PixelFace(), tox(j - FromX), toy(i - FromY), FullMap[j, i].PixelFace().Width, FullMap[j, i].PixelFace().Height);
                    }
                    if (FullMap[j, i].pickup != null)
                    {
                        g.DrawImage(FullMap[j, i].pickup.PickupImage, tox(j - FromX), toy(i - FromY), PixHeight, PixHeight);
                    }
                }
            }
            g.DrawImage(h.face, (h.actualX + (PixWidth - h.face.Width) / 2) - toxy(FromX, FromY).x, (h.actualY + (PixHeight - h.face.Height) / 2) - toxy(FromX, FromY).y, h.face.Width, h.face.Height);
            foreach (Ghost gh in GhostList)
            {
                g.DrawImage(gh.face, (gh.actualX + (PixWidth - gh.face.Width) / 2) - toxy(FromX, FromY).x, (gh.actualY + (PixHeight - gh.face.Height) / 2) - toxy(FromX, FromY).y, gh.face.Width, gh.face.Height);
            }
        }

        public void PickTheThingUp(Action rerender)
        {
            if (FullMap[h.x, h.y].pickup != null)
            {
                Pickup pick = h.PickupList.Find
                    (
                        delegate(Pickup bk)
                        {
                            return bk.PickupType == FullMap[h.x, h.y].pickup.PickupType;
                        }
                    );
                if (pick != null)
                {
                    pick.PickupQuantity += FullMap[h.x, h.y].pickup.PickupQuantity;
                }
                else
                {
                    h.PickupList.Add(FullMap[h.x, h.y].pickup);
                }
               // label1.Text = "You've picked up " + FullMap[h.x, h.y].pickup.PickupQuantity.ToString() + " pieces of " + l.FullMap[l.h.x, l.h.y].pickup.PickupType;
                FullMap[h.x, h.y].pickup = null;
                rerender();
            }
        }

        public List<Point> FindPath(int x1, int y1, int x2, int y2)
        {
            Point[,] FromWhere = new Point[width, height];
            Queue<Point> Pile = new Queue<Point>();
            List<Point> Path = new List<Point>();

            if (!FullMap[x2, y2].free)
            {
                Path.Add(new Point(x1, y1));
                return Path;
            }

            FromWhere[x1, y1] = new Point(-1, -1);

            Pile.Enqueue(new Point(x1, y1));
            Point cur = new Point(0, 0);

            while (Pile.Count > 0)
            {
                cur = Pile.Dequeue();
                if (FullMap[cur.x - 1, cur.y].free && FromWhere[cur.x - 1, cur.y] == null)
                {
                        FromWhere[cur.x - 1, cur.y] = new Point(cur.x, cur.y);
                        Pile.Enqueue(new Point(cur.x - 1, cur.y));                
                }
                if (FullMap[cur.x + 1, cur.y].free && FromWhere[cur.x + 1, cur.y] == null)
                {
                        FromWhere[cur.x + 1, cur.y] = new Point(cur.x, cur.y);
                        Pile.Enqueue(new Point(cur.x + 1, cur.y));                
                }
                if (FullMap[cur.x, cur.y - 1].free && FromWhere[cur.x, cur.y - 1] == null)
                {
                        FromWhere[cur.x, cur.y - 1] = new Point(cur.x, cur.y);
                        Pile.Enqueue(new Point(cur.x, cur.y - 1));
                }
                if (FullMap[cur.x, cur.y + 1].free && FromWhere[cur.x, cur.y + 1] == null)
                {
                        FromWhere[cur.x, cur.y + 1] = new Point(cur.x, cur.y);
                        Pile.Enqueue(new Point(cur.x, cur.y + 1));
                }

                if (FromWhere[x2,y2] != null)
                {
                    cur.x = x2;
                    cur.y = y2;
                    Path.Clear();                    
                    Path.Add(new Point(x2, y2));
                    while ((FromWhere[cur.x, cur.y].x != -1))
                    {
                        Point cur2 = new Point(0, 0);
                        Path.Add(new Point(FromWhere[cur.x, cur.y].x, FromWhere[cur.x, cur.y].y));
                        cur2.x = FromWhere[cur.x, cur.y].x;
                        cur2.y = FromWhere[cur.x, cur.y].y;
                        cur.x = cur2.x;
                        cur.y = cur2.y;
                    }
                    Path.Reverse();
                    Path.Remove(Path[0]);
                    return Path;
                }
            }
            return Path;
        }
    }
}

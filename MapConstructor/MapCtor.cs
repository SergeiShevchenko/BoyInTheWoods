using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Strategy
{
    public partial class MapCtor : Form
    {
        public MapCtor()
        {
            InitializeComponent();
        }

        Land l = new Land();

        private void MapCtor_Load(object sender, EventArgs e)
        {            
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
        }       


        private void button1_Click(object sender, EventArgs e)
        {
            l.LoadAMap(textBox1.Text);
            textBox2.Text = l.width.ToString();
            textBox3.Text = l.height.ToString();
            pictureBox1.Width = l.width * l.PixWidth;
            pictureBox1.Height = l.height * l.PixHeight;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (l.FullMap!=null)
                l.DrawSprites(e.Graphics, 0, 0, l.width, l.height);
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            l.height=Convert.ToInt32(textBox3.Text);
            l.width=Convert.ToInt32(textBox2.Text);
            l.BuildAMap();
            pictureBox1.Width = l.width * l.PixWidth;
            pictureBox1.Height = l.height * l.PixHeight;
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            l.SaveAMap(textBox1.Text);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (comboBox1.Text == "Tree")
            {
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].ChangeContent(Content.Tree);
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].pickup = null;
            }
            else if (comboBox1.Text == "Free")
            {
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].ChangeContent(Content.Free);
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].pickup = null;
            }
            else if (comboBox1.Text == "Gold")
            {
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].ChangeContent(Content.Free);
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].pickup = new Pickup("Gold", 100);
            }
            else if (comboBox1.Text == "Hero")
            {
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].ChangeContent(Content.Free);
                l.FullMap[l.FromXY(e.X, e.Y).x, l.FromXY(e.X, e.Y).y].pickup = null;
                l.h.x = l.FromXY(e.X, e.Y).x;
                l.h.y = l.FromXY(e.X, e.Y).y;
            }
            pictureBox1.Invalidate();
        }
    }
}

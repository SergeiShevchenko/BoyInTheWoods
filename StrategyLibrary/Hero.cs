using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Strategy
{
    public class Hero
    {
        public int x, y;
        public int actualX, actualY;
        public Bitmap face;
        public Bitmap[] faceCollection=new Bitmap[3];
        public char direction='S';
        public List<Pickup> PickupList= new List<Pickup>();

        public int Health = 100;

        public Hero(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public void ChangeFaceCollection(Bitmap[] fc)
        {
            faceCollection[0] = fc[0];
            faceCollection[1] = fc[1];
            faceCollection[2] = fc[2];
            face = faceCollection[0];
        }

        public void ChangeFace()
        {
            if (face == faceCollection[0]) face = faceCollection[1];
            else if (face == faceCollection[1]) face = faceCollection[2];
            else if (face == faceCollection[2]) face = faceCollection[0];
        }
    }
}

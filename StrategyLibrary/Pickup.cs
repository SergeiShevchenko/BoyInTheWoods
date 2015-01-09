using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Strategy
{
    public class Pickup
    {
        public string PickupType;
        public int PickupQuantity;
        public Bitmap PickupImage;
        public Pickup(string PT, int PQ)
        {
            PickupType = PT;
            PickupQuantity = PQ;
            if ((PickupType == "Gold") || (PickupType == "Silver")) PickupImage=new Bitmap(Image.FromFile("Gold.png"));
            else if (PickupType == "Sword") PickupImage= new Bitmap(Image.FromFile("Sword.png"));
            else if (PickupType == "Potion") PickupImage = new Bitmap(Image.FromFile("Potion.png"));
            PickupImage.MakeTransparent(Color.White);
        }


    }
}

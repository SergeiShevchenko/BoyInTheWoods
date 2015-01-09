using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Strategy
{
    public enum Content {
        Free,
        Tree,
        Tree2,
        Bush,
        Stone,
        Water,
        Bounds
    };


    public class MapPixel
    {
        public Content Content;
        public bool free { get { return Content == Content.Free; } }
        public Pickup pickup;
        public Bitmap PixelFace()
        {
            switch (Content)
            {
                case Content.Tree:
                    return Land.treeSprite;
                case Content.Tree2:
                    return Land.tree2Sprite;
                case Content.Bush:
                    return Land.bushSprite;
                case Content.Stone:
                    return Land.stoneSprite;
                case Content.Water:
                    return Land.waterSprite;
                case Content.Free:
                    return Land.grassSprite;
                case Content.Bounds:
                    return Land.treeSprite;
            }
            return Land.grassSprite;
        }

        public MapPixel()
        {
            ChangeContent(Content.Free);
        }
        public MapPixel(Content C)
        {
            ChangeContent(C);
        }
        public void ChangeContent(Content c)
        {
            Content=c;
        }
    }
}

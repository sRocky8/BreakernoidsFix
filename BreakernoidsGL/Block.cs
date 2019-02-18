using System;
using Microsoft.Xna.Framework;

namespace BreakernoidsGL
{
    public enum BlockColor
    {
        Red = 0,
        Yellow,
        Blue,
        Green,
        Purple,
        GreyHi,
        Grey
    }
    public class Block : GameObject
    {

        public BlockColor thisBlockColor;

        public Block(BlockColor blockColor, Game myGame) :
            base(myGame)
        {
            thisBlockColor = blockColor;
            int colorToNumber = (int)blockColor;

            switch (colorToNumber)
            {
                case 0:
                    textureName = "block_red";
                    break;
                case 1:
                    textureName = "block_yellow";
                    break;
                case 2:
                    textureName = "block_blue";
                    break;
                case 3:
                    textureName = "block_green";
                    break;
                case 4:
                    textureName = "block_purple";
                    break;
                case 5:
                    textureName = "block_grey_hi";
                    break;
                case 6:
                    textureName = "block_grey";
                    break;
            }
        }

        public bool OnHit()
        {
            if(thisBlockColor != BlockColor.GreyHi)
            {
                return true;
            }
            else{
                thisBlockColor = BlockColor.Grey;
                textureName = "block_grey";
                return false;
            }
        }
    }
}
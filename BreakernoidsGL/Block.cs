using System;
using Microsoft.Xna.Framework;

namespace BreakernoidsGL
{
    public class Block : GameObject
    {
        public Block(Game myGame) :
            base(myGame)
        {
            textureName = "block_red";
        }

//        public override void Update(float deltaTime)
//        {
//            base.Update(deltaTime);
//        }
    }
}
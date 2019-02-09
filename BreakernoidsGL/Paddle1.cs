using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace BreakernoidsGL
{
    public class Paddle : GameObject
    {
        public Paddle(Game myGame) :
            base(myGame)
        {
            textureName = "paddle";
        }
    }
}
using System;
using Microsoft.Xna.Framework;

namespace BreakernoidsGL{
    public class Ball : GameObject
    {
        private readonly int speed = 400;
        public Vector2 direction = new Vector2(0.707f, -0.707f);
        public bool caught;

        public Ball(Game myGame):
            base(myGame)
        {
            textureName = "ball";
        }

        public override void Update(float deltaTime)
        {
            if (caught == false)
            {
                position += direction * speed * deltaTime;
            }
            base.Update(deltaTime);

        }
    }
}
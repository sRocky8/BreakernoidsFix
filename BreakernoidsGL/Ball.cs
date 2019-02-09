using System;
using Microsoft.Xna.Framework;

namespace BreakernoidsGL{
    public class Ball : GameObject
    {
        private readonly int speed = 350;
        public Vector2 direction = new Vector2(0.707f, -0.707f);

        public Ball(Game myGame):
            base(myGame)
        {
            textureName = "ball";
        }

        public override void Update(float deltaTime)
        {
//            position.X = MathHelper.Clamp(position.X, 32 + (texture.Width / 2), 992 - (texture.Width / 2));
            position += direction * speed * deltaTime;

            base.Update(deltaTime);
        }
    }
}
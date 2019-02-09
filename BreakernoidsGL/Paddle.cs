using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace BreakernoidsGL
{
    public class Paddle : GameObject
    {
        private readonly int speed = 500;

        public Paddle(Game myGame):
            base(myGame)
        {
            textureName = "paddle";
        }

        public override void Update(float deltaTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Left))
            {
                position.X -= speed * deltaTime;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                position.X += speed * deltaTime;
            }
            position.X = MathHelper.Clamp(position.X, 32 + (texture.Width /2), 992 - (texture.Width / 2));
            base.Update(deltaTime);
        }
    }
}

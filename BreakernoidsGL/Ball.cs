using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BreakernoidsGL{
    public class Ball : GameObject
    {
        private readonly int speed = 400;
        public Vector2 direction = new Vector2(0.707f, -0.707f);
        public bool caught;
        public int collisionFrames;

        public Ball(Game myGame):
            base(myGame)
        {
            textureName = "ball";
        }

        public override void Update(float deltaTime)
        {
            if (collisionFrames > 0 && caught == false)
            {
                collisionFrames -= 1;
                Console.WriteLine("Frames left: " + collisionFrames + "\n");
            }
            KeyboardState keyState = Keyboard.GetState();
            if (caught == false)
            {
                position += direction * speed * deltaTime;
            }
            else if (caught == true)
            {
                
                if (keyState.IsKeyDown(Keys.Space))
                {
                    caught = false;
                    direction = new Vector2(0.0f, 1.0f);
                }
            }
            
            base.Update(deltaTime);

        }
    }
}
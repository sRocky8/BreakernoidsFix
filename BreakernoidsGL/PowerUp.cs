using System;
using Microsoft.Xna.Framework;

namespace BreakernoidsGL
{
    public enum PowerUpTypes
    {
        powerup_c = 0,
        powerup_b,
        powerup_p
    }
    public class PowerUp : GameObject
    {
        private readonly int speed = 300;

        public Vector2 direction = new Vector2(0.0f, 1.0f);
        public PowerUpTypes thisPowerUp;
        public bool remove;

        public PowerUp(PowerUpTypes powerUp, Game myGame):
            base(myGame)
        {
            thisPowerUp = powerUp;
            int powerUpToNumber = (int)powerUp;

            switch (powerUpToNumber)
            {
                case 0:
                    textureName = "powerup_c";
                    break;
                case 1:
                    textureName = "powerup_b";
                    break;
                case 2:
                    textureName = "powerup_p";
                    break;
            }
        }
        public override void Update(float deltaTime)
        {
            position += speed * direction * deltaTime;

            base.Update(deltaTime);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakernoidsGL
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bgTexture;
        Paddle paddle;
        Ball ball;
        List<Block> blocks = new List<Block>();
        Block deleteBlock;
        int collisionFrames;
        bool blockCollision = false;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("bg");
            
            paddle = new Paddle(this);
            paddle.LoadContent();
            paddle.position = new Vector2(512, 740);

            ball = new Ball(this);
            ball.LoadContent();
            ball.position = new Vector2(512, paddle.position.Y - ball.Height - paddle.Height);

            int[,] blockLayout = new int[,]
            {
                {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
                {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
            };

            for (int i = 0; i < blockLayout.GetLength(0); i++)
            {
                for (int j = 0; j < blockLayout.GetLength(1); j++) {
                    Block tempBlock = new Block((BlockColor)blockLayout[i, j], this);
                    tempBlock.LoadContent();
                    tempBlock.position = new Vector2(64 + (j * 64), 100 + (i * 32));
                    blocks.Add(tempBlock);
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            paddle.Update(deltaTime);
            ball.Update(deltaTime);
            CheckCollisions();
            LoseLife();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);


            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Draw all sprites here
            spriteBatch.Draw(bgTexture, new Vector2(0, 0), Color.White);
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            foreach (Block b in blocks)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void CheckCollisions()
        {
            float radius = ball.Width / 2;
            foreach (Block b in blocks)
            {
                //left
                if (ball.position.X < b.position.X + radius - (b.Width / 20)
                    && (ball.position.X > (b.position.X - radius - (b.Width / 2))
                    && (ball.position.Y < (b.position.Y + radius + b.Height / 3))
                    && (ball.position.Y > (b.position.Y - radius - b.Height / 3))))
                {
                    ball.direction.X = ball.direction.X * -1;
                    blockCollision = true;
                    deleteBlock = b;
                }
                //right
                else if (ball.position.X > (b.position.X - radius + b.Width / 20)
                    && (ball.position.X < (b.position.X + radius + b.Width / 2)
                    && (ball.position.Y < (b.position.Y + radius + b.Height / 3))
                    && (ball.position.Y > (b.position.Y - radius - b.Height / 3))))
                {
                    ball.direction.X = ball.direction.X * -1;
                    blockCollision = true;
                    deleteBlock = b;
                }
                //up and down
                else if ((ball.position.X > (b.position.X - radius - b.Width / 2))
                   && (ball.position.X < (b.position.X + radius + b.Width / 2))
                   && (ball.position.Y < (b.position.Y + radius + b.Height / 2))
                   && (ball.position.Y > (b.position.Y - radius - b.Height / 2)))
                {
                    ball.direction.Y = ball.direction.Y * -1;
                    blockCollision = true;
                    deleteBlock = b;
                }
            }



            if (deleteBlock != null && blockCollision == true)
            {
                blocks.Remove(deleteBlock);
                blockCollision = false;
            }

            if (collisionFrames == 0)
            {
                if (ball.position.X > (paddle.position.X - radius + (paddle.Width / 6)) 
                    && (ball.position.X < (paddle.position.X + radius + (paddle.Width / 2)) 
                    && (ball.position.Y < paddle.position.Y)
                    && (ball.position.Y > (paddle.position.Y - radius - paddle.Height / 2))))
                {
                    ball.direction = Vector2.Reflect(ball.direction, new Vector2(0.196f, -0.981f));
                    collisionFrames = 20;
                }
                else if (ball.position.X < paddle.position.X + radius - (paddle.Width / 6)
                    && (ball.position.X > (paddle.position.X - radius - (paddle.Width / 2))
                    && (ball.position.Y < paddle.position.Y)
                    && (ball.position.Y > (paddle.position.Y - radius - paddle.Height / 2))))
                {
                    ball.direction = Vector2.Reflect(ball.direction, new Vector2(-0.196f, -0.981f));
                    collisionFrames = 20;
                }
                else if (ball.position.X > (paddle.position.X - radius - (paddle.Width / 6))
                    && (ball.position.X < paddle.position.X + radius + (paddle.Width / 6))
                    && (ball.position.Y < paddle.position.Y)
                    && (ball.position.Y > (paddle.position.Y - radius - paddle.Height / 2)))
                {
                    ball.direction = Vector2.Reflect(ball.direction, new Vector2(0, -1));
                    collisionFrames = 20;
                }
            }

            while (collisionFrames > 0)
            {
                --collisionFrames;
            }

            if (Math.Abs(ball.position.X - 32) < radius)
            {
                ball.direction.X = ball.direction.X * -1;
            }
            else if (Math.Abs(ball.position.X - 992) < radius)
            {
                //right wall collision
                ball.direction.X = ball.direction.X * -1;
            }
            else if (Math.Abs(ball.position.Y - 32) < radius)
            {
                //collision with top
                ball.direction.Y = ball.direction.Y * -1;
            }
        }
        
        protected void LoseLife()
        {
            float radius = ball.Width / 2;
            if (ball.position.Y > 768 + radius)
            {
                paddle.position = new Vector2(512, 740);

                ball.position = new Vector2(512, paddle.position.Y - ball.Height - paddle.Height);
                ball.direction = new Vector2(0.707f, -0.707f);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameRunner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character myChar;
        Ground myGround;
        Bird myBird;
        Saw mySaw1, mySaw2, mySaw3;
        SpriteFont myFont;
        float gameVelocity;
        bool isGameOver;
        float preScore;
        float curScore;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            gameVelocity = 0.5f;//0-5->0.7
            myGround = new Ground(0, 500, gameVelocity);
            myChar = new Character(200, 500 - myGround.Height - 100, 80);
            myBird = new Bird(1200, 250, gameVelocity * 1.5f);//pos:350 or 250
            mySaw1 = new Saw(1200, 400 - myGround.Height, gameVelocity);
            mySaw2 = new Saw(1800, 400 - myGround.Height, gameVelocity);
            mySaw3 = new Saw(1900, 400 - myGround.Height, gameVelocity);
            isGameOver = false;
            preScore = -1;
            curScore = 0;
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
            myGround.LoadContent(this, new string[] { "ground" });
            myChar.LoadContent(this, new string[] { "ninja/Jump__000","ninja/Jump__001","ninja/Jump__002","ninja/Jump__003","ninja/Jump__004","ninja/Jump__005","ninja/Jump__006","ninja/Jump__007","ninja/Jump__008","ninja/Jump__009",
                    "ninja/Run__000","ninja/Run__001","ninja/Run__002","ninja/Run__003","ninja/Run__004","ninja/Run__005","ninja/Run__006","ninja/Run__007","ninja/Run__008","ninja/Run__009",
            "ninja/Slide__000",});
            myBird.LoadContent(this, new string[] { "bird/1", "bird/2", "bird/3" });
            mySaw1.LoadContent(this, new string[] { "Saw" });
            mySaw2.LoadContent(this, new string[] { "Saw" });
            mySaw3.LoadContent(this, new string[] { "Saw" });
            myFont = Content.Load<SpriteFont>("myFont");

            myChar.Collide += (object sender, EventArgs e) =>
            {
                isGameOver = true;
                gameVelocity = 0;
                myGround.Velocity = gameVelocity;
                myBird.V0 = gameVelocity * 1.5f;
                mySaw1.V0 = gameVelocity;
                mySaw2.V0 = gameVelocity;
                mySaw3.V0 = gameVelocity;
                if (preScore == -1)
                {
                    preScore = curScore / 100;

                }
                else
                {
                    preScore = preScore < (int)curScore / 100 ? (int)curScore / 100 : preScore;
                }

            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            myGround.UnLoad();
            myChar.UnLoad();
            myBird.UnLoad();
            mySaw1.UnLoad();
            mySaw2.UnLoad();
            mySaw3.UnLoad();
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




            if (!isGameOver)
            {
                myChar.AddBirdCollisionListener(myBird);
                myChar.AddSawCollisionListener(mySaw1);
                myChar.AddSawCollisionListener(mySaw2);
                myChar.AddSawCollisionListener(mySaw3);

                if (mySaw1.PosX + 100 < 0)
                {
                    mySaw1.PosX = 1200;
                }
                if (mySaw3.PosX + 100 < 0)
                {
                    mySaw2.PosX = 1200;
                    mySaw3.PosX = 1300;
                }
                if (myBird.PosX + 2800 < 0)
                {
                    Random rnd = new Random();

                    if (rnd.Next(1, 2) == 1)
                    {
                        myBird.PosY = 250;
                    }
                    else
                    {
                        myBird.PosY = 350;
                    }
                    myBird.PosX = 1200;
                }
                curScore += (float)gameTime.ElapsedGameTime.TotalMilliseconds * (gameVelocity - 0.5f + 1);
                if (curScore / 100 > 100 && curScore / 100 < 200)
                {
                    gameVelocity = 0.6f;
                    myGround.Velocity = gameVelocity;
                    myBird.V0 = gameVelocity * 1.5f;
                    mySaw1.V0 = gameVelocity;
                    mySaw2.V0 = gameVelocity;
                    mySaw3.V0 = gameVelocity;

                }
                if (curScore / 100 >= 300)
                {
                    gameVelocity = 0.7f;
                    myGround.Velocity = gameVelocity;
                    myBird.V0 = gameVelocity * 1.5f;
                    mySaw1.V0 = gameVelocity;
                    mySaw2.V0 = gameVelocity;
                    mySaw3.V0 = gameVelocity;
                }

                myGround.Update(gameTime);
                myChar.Update(gameTime);
                myBird.Update(gameTime);
                mySaw1.Update(gameTime);
                mySaw2.Update(gameTime);
                mySaw3.Update(gameTime);

            }

            MouseState ms = Mouse.GetState();
            if ((ms.LeftButton == ButtonState.Pressed || ms.RightButton == ButtonState.Pressed) && isGameOver)
            {
                gameVelocity = 0.5f;//0-5->0.7
                myChar.PosY = 0;
                myGround.Velocity = gameVelocity;
                myGround.PosX = 0;
                myGround.PosY = 500;

                myBird.V0 = gameVelocity * 1.5f;
                myBird.PosX = 1200;
                myBird.PosY = 250;

                mySaw1.V0 = gameVelocity;
                mySaw1.PosX = 1200;

                mySaw2.V0 = gameVelocity;
                mySaw2.PosX = 1800;

                mySaw3.V0 = gameVelocity;
                mySaw3.PosX = 1900;

                curScore = 0;
                isGameOver = false;



            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront);
            myGround.Draw(gameTime, spriteBatch);
            myChar.Draw(gameTime, spriteBatch);
            myBird.Draw(gameTime, spriteBatch);
            mySaw1.Draw(gameTime, spriteBatch);
            mySaw2.Draw(gameTime, spriteBatch);
            mySaw3.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(myFont, "Score: " + (int)curScore / 100, new Vector2(1000, 20), Color.Black);
            spriteBatch.DrawString(myFont, "!!!Guideline:\nPress button down to slide.Press button up to jump.\nAvoid saw and bird.If you lose, click right or left mouse to restart game. ", new Vector2(30, 550), Color.Black);
            if (preScore != -1)
            {
                spriteBatch.DrawString(myFont, "HI: " + (int)preScore, new Vector2(800, 20), Color.Black);
            }
            if (isGameOver)
            {
                spriteBatch.DrawString(myFont, "You Lose! Click Left or Right Mouse to restart game!", new Vector2(200, 200), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

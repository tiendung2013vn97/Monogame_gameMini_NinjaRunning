using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunner
{
    class Character : Sprite2D
    {
        public int State { get; set; }
        public float V0 { get; set; }
        bool isJumping;
        bool isSliding;
        float timeCount;
        public delegate void CollideHandler(object sender, EventArgs e);
        public event CollideHandler Collide;
        public Character(float x, float y, float v0)
            : base(x, y)
        {
            isJumping = false;
            isSliding = false;
            V0 = v0;
            timeCount = 0;
        }
        public void AddSawCollisionListener(Saw other)
        {
            bool isCollided = false;
            if(State==2){
                Rectangle r1 = new Rectangle((int)PosX,(int) PosY+20, 100, 80);
                Rectangle r2 = new Rectangle((int)other.PosX, (int)other.PosY, 100, 100);
                isCollided=r1.Intersects(r2);
            }
            else
            {
                Rectangle r1 = new Rectangle((int)PosX, (int)PosY, 100, 100);
                Rectangle r2 = new Rectangle((int)other.PosX, (int)other.PosY, 100, 100);
                isCollided = r1.Intersects(r2);
            }

            if (isCollided && Collide != null)
            {
                this.Collide(this, new EventArgs());
            }

        }
        public void AddBirdCollisionListener(Bird other)
        {
            bool isCollided = false;
            if (State == 2)
            {
                Rectangle r1 = new Rectangle((int)PosX, (int)PosY+20, 100, 80);
                Rectangle r2 = new Rectangle((int)other.PosX, (int)other.PosY, 70, 70);
                isCollided = r1.Intersects(r2);
            }
            else
            {
                Rectangle r1 = new Rectangle((int)PosX, (int)PosY, 100, 100);
                Rectangle r2 = new Rectangle((int)other.PosX, (int)other.PosY, 70, 70);
                isCollided = r1.Intersects(r2);
            }

            if (isCollided && Collide != null)
            {
                this.Collide(this, new EventArgs());
            }

        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();


            //process running
            if (isJumping == false&&isSliding==false)
            {
                State = 0;//0 mean running
                imgIndex = 10 + (imgIndex + 1) % 10;
            }

            //if character isn't jumpping or sliding, the character will jump
            if ( key.IsKeyDown(Keys.Up) && !isJumping && isSliding == false)
            {
                isJumping = true;
                timeCount = 0;
                
                
            }

            //process increase imgIndex to update image of jumping
            if (isJumping == true)
            {
                State = 1;//0 mean jumpping
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds*20;
                timeCount += t;
                PosY = 400 - (V0*1.1f * timeCount + 0.5f * -9.8f * timeCount * timeCount);
                if (PosY > 400)
                {
                    PosY = 400;
                    isJumping = false;
                }
                float timeFly=2*V0/9.8f;
                if (!(imgIndex >= 0 && imgIndex <= 9))
                {
                    if (timeCount < timeFly / 10)
                    {
                        imgIndex = 0;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        if (timeCount >= timeFly / 10 * i && timeCount < timeFly / 10 * (i + 1))
                        {
                            imgIndex = i;
                        }
                    }
                    if (timeCount == timeFly)
                    {
                        imgIndex = 10;
                        isJumping = false;

                    }

                }

            }

            //if character isn't jumpping or sliding, the character will sliding
            if (key.IsKeyDown(Keys.Down) && !isJumping && !isSliding)
            {
                isSliding = true;
                timeCount = 0;
            }

            //process increase imgIndex to update image of jumping
            if (isSliding == true)
            {
                State = 2;//2 mean sliding
                imgIndex = 20;
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeCount += t;
                if (timeCount >= 0.4)
                {
                    imgIndex = 10;
                    isSliding = false;
                }
            }


        }//end Update

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (imgIndex != 20)
            {
                spriteBatch.Draw(img[imgIndex], new Rectangle((int)PosX, (int)PosY, 100, 100), Color.White);
            }
            else
            {
                spriteBatch.Draw(img[imgIndex], new Rectangle((int)PosX, (int)PosY+20, 100, 80), Color.White);
            }
            
        }//end Draw

    }
}

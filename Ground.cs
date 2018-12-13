using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunner
{
    class Ground:Sprite2D
    {
        public float Velocity { get; set; }
        public Ground(float x, float y, float v)
            : base(x, y)
        {
            Velocity = v;
        }
        public override void Update(GameTime gameTime)
        {
            PosX -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;

        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (PosX + Width < 0)
            {
                PosX = 0;
            }
            spriteBatch.Draw(img[imgIndex], new Vector2(PosX, PosY), Color.White);
            if (-PosX +1200>= Width && PosX+Width>=0)
            {
                float distance=(float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;
                spriteBatch.Draw(img[imgIndex], new Vector2(PosX+Width, PosY), Color.White);
            }
            
        }


    }
}

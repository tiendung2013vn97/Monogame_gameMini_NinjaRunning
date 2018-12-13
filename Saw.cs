using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunner
{
    class Saw:Sprite2D
    {
        public float V0 { get; set; }
        public Saw(float x,float y,float v):base(x,y)
        {
            V0 = v;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            PosX -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * V0;
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img[imgIndex], new Rectangle((int)PosX, (int)PosY, 100, 100), Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunner
{
    class Sprite2D
    {
        public List<Texture2D> img{get;set;}
        public int Width { get; set; }
        public int Height { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }

        public int imgIndex { get; set; }

        public Sprite2D(float x, float y)
        {
            imgIndex = 0;
            PosX = x;
            PosY = y;
           
        }
        public void LoadContent(Game game,string[] imgPath)
        {
            img = new List<Texture2D>();
            for (int i = 0; i < imgPath.Length; i++)
            {
                var tex = game.Content.Load<Texture2D>(imgPath[i]);
                this.img.Add(tex);
            }
            if (imgPath.Length > 0)
            {
                Width = img[0].Width;
                Height = img[0].Height;
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            int size=img.Count;
            imgIndex = (imgIndex + 1) % size;
        }
        public void UnLoad()
        {
            img.Clear();
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img[imgIndex], new Vector2(PosX, PosY), Color.White);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    public class Animation
    {
        public Rectangle sourceRect, desRect;
        public Vector2 spriteSize, position;
        public int frames;
        public float delay = 15f;
        public Texture2D tex;
        public Animation(Texture2D _tex, Vector2 _spritesize, Vector2 position, int _frameNo)
        {
            tex = _tex;
            frames = _frameNo;
            spriteSize = _spritesize;
            sourceRect = new Rectangle(0,0, (int)spriteSize.X, (int)spriteSize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
        public void Update(GameTime _gt)
        {
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(tex, desRect, sourceRect, Color.White);
        }
    }
}

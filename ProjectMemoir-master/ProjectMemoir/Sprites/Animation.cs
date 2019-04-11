using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    public class Animation
    {
        public Rectangle sourceRect, desRect;
        public Vector2 spriteSize, sourcesize ,position;
        public int frames;
        public float delay = 15f;
        public Texture2D tex;
        public Color col;
        public Animation(Texture2D _tex, Vector2 _spritesize, Vector2 _sourceSize,Vector2 _position, int _frameNo, Color _col)
        {
            tex = _tex;
            frames = _frameNo;
            spriteSize = _spritesize;
            position = _position;
            col = _col;
            sourcesize = _sourceSize;
            //rememeber the change source rects xy to 0 0
            sourceRect = new Rectangle(32,0, (int)sourcesize.X, (int)sourcesize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
        public void Update(GameTime _gt)
        {
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(tex, desRect, sourceRect, col);
        }
    }
}

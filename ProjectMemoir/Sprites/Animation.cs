using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    public class Animation
    {
        public Rectangle sourceRect, desRect;
        public Vector2 spriteSize, sourcesize ,position, sourcePos;
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
            sourcePos = new Vector2(0);
            //rememeber the change source rects xy to 0 0
            sourceRect = new Rectangle((int)sourcePos.X,(int)sourcePos.Y, (int)sourcesize.X, (int)sourcesize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
        public void Update(GameTime _gt)
        {
            sourceRect = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, (int)sourcesize.X, (int)sourcesize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(tex, desRect, sourceRect, col);
        }
    }
}

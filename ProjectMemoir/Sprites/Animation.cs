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
        public int frames, currentframe, mirrorval = 1;
        public float delay = 0f,maxDelay = 1f;
        public Texture2D tex;
        public Color col;
        public SpriteEffects mirrored;
        public bool needsChange = true;// if the source pos x needs to snap back to the original position
        public Animation(Texture2D _tex, Vector2 _spritesize, Vector2 _sourceSize,Vector2 _position, int _frameNo, Color _col)
        {
            tex = _tex;
            frames = _frameNo;
            spriteSize = _spritesize;
            position = _position;
            col = _col;
            sourcesize = _sourceSize;
            sourcePos = new Vector2(0);
            mirrored = SpriteEffects.None;
            //rememeber the change source rects xy to 0 0
            sourceRect = new Rectangle((int)sourcePos.X,(int)sourcePos.Y, (int)sourcesize.X, (int)sourcesize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X , (int)spriteSize.Y);
        }
        public void Update(GameTime _gt)
        {
            if (frames > 0)
            {
                Animate();
            } else if(needsChange)
            {
                if (sourcePos.X >= sourcesize.X * frames) { currentframe = 0; }
                sourcePos.X = sourcesize.X * currentframe;
            }
            sourceRect = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, (int)sourcesize.X, (int)sourcesize.Y);
            desRect = new Rectangle((int)position.X, (int)position.Y, (int)spriteSize.X, (int)spriteSize.Y);
        }
        private void Animate()
        {
            
            if (delay >= maxDelay)
            {
                if (currentframe < frames)
                {
                    currentframe++;
                }
                else
                {
                    currentframe = 0;
                }
                delay = 0f;
            }
            else { delay += 0.5f; }
            sourcePos.X = sourcesize.X * currentframe;
        }
        public bool isFinished()
        {
            return currentframe == frames;
        }
        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(tex, desRect, sourceRect, col, 0.0f, new Vector2(0, 0), mirrored, 0.0f);
        }
    }
}

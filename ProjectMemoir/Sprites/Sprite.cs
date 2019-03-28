using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Sprites
{
    public class Sprite
    {
        public Animation anim;
        public Vector2 velocity;
        public Sprite(ContentManager _con, Vector2 _pos)
        {
            
        }

        public virtual void Update(GameTime _gt, List<Sprite> _sl)
        {
            anim.position += velocity;
            anim.Update(_gt);
        }


        public virtual void Draw(SpriteBatch _sb)
        {
            anim.Draw(_sb);
        }
        # region collision
        //checking if you touching the corresponding side of sprite "_S"
        public bool checkLeftCol(Sprite _s)
        {
            return anim.desRect.Left < _s.anim.desRect.Left &&
                anim.desRect.Right + velocity.X > _s.anim.desRect.Left &&
                anim.desRect.Top < _s.anim.desRect.Bottom &&
                anim.desRect.Bottom > _s.anim.desRect.Top
                ;
        }
        public bool checkRightCol(Sprite _s)
        {
            return anim.desRect.Right > _s.anim.desRect.Right &&
                anim.desRect.Left + velocity.X < _s.anim.desRect.Right &&
                anim.desRect.Top < _s.anim.desRect.Bottom &&
                anim.desRect.Bottom > _s.anim.desRect.Top
                ;
        }
        public bool checkTopCol(Sprite _s)
        {
            return anim.desRect.Right > _s.anim.desRect.Left &&
                anim.desRect.Left < _s.anim.desRect.Right &&
                anim.desRect.Top < _s.anim.desRect.Top &&
                anim.desRect.Bottom + velocity.Y > _s.anim.desRect.Top
                ;
        }
        public bool checkBottomCol(Sprite _s)
        {
            return anim.desRect.Right > _s.anim.desRect.Left &&
                anim.desRect.Left < _s.anim.desRect.Right &&
                anim.desRect.Top + velocity.Y < _s.anim.desRect.Bottom &&
                anim.desRect.Bottom > _s.anim.desRect.Bottom
                ;
        }
        #endregion
    }
}

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
        public Sprite(ContentManager _con, Vector2 _pos)
        {
            
        }

        public virtual void Update(GameTime _gt, List<Sprite> _sl)
        {
            anim.Update(_gt);
        }


        public virtual void Draw(SpriteBatch _sb)
        {
            anim.Draw(_sb);
        }
    }
}

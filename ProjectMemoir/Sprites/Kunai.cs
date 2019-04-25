using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites;
namespace ProjectMemoir.Sprites
{
    public class Kunai:PhysObject
    {


        int facing;
        public Kunai(ContentManager _con, Vector2 _pos, int _side):base(_con, _pos)
        {
            facing = _side;
            velocity = new Vector2(20 * _side, 0);
            anim = new Animation(_con.Load<Texture2D>("playersprites/kunai"), new Vector2(29,9), new Vector2(29,9), _pos, 0, Color.White);
            grav = 0;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
           
            //flip the sprite based on facing value
            if(facing == 1)
            {
                anim.mirrored = SpriteEffects.None;
            } else
            {
                anim.mirrored = SpriteEffects.FlipHorizontally;
            }

            foreach (Sprite _s in _sl)
            {
                if (_s.GetType() != typeof(Solid)) { continue; }
                if (checkLeftCol(_s) || checkRightCol(_s) || checkTopCol(_s) || checkBottomCol(_s))
                    isVisible = false;
            }
            base.Update(_gt, _sl);
        }

    }
}

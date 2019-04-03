using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites;


namespace ProjectMemoir.Sprites.Enemies
{
    public class SentryProjectile:PhysObject
    {
        public SentryProjectile(ContentManager _con, Vector2 _pos, Vector2 _vel) :base(_con, _pos)
        {
            grav = 0;
            velocity = _vel;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(10), _pos, 0, Color.PaleVioletRed);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            foreach(Player _s in _sl)
            {
                if(_s.GetType() != typeof(Player))
                {
                    if (anim.desRect.Intersects(_s.anim.desRect))
                    {
                        _s.hp -= 5;
                        isVisible = false;
                    }
                }
            }
            base.Update(_gt, _sl);
        }
    }
}

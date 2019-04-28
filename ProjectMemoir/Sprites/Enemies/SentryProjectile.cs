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
        Player target;
        public SentryProjectile(ContentManager _con, Vector2 _pos, Vector2 _vel, Player _target) :base(_con, _pos)
        {
            grav = 0;
            velocity = _vel;
            target = _target;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/sentry_projectile"), new Vector2(15), new Vector2(10), _pos, 5, Color.White);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {

            if (anim.desRect.Intersects(target.anim.desRect) && !target.invincible)
            {
                target.getHurt(Math.Sign(target.anim.position.X - anim.position.X) * 4, -4);
                isVisible = false;
            }

            //destroy if you touch a solid
            foreach(Sprite _s in _sl)
            {
                if(_s.GetType() != typeof(Solid)) { continue; }
                if (checkLeftCol(_s) || checkRightCol(_s) || checkTopCol(_s) || checkBottomCol(_s))
                    isVisible = false;
            }
            base.Update(_gt, _sl);
        }
    }
}

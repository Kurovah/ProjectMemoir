using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    class DummyEn:Sprite
    {
        bool isAlert;
        Sprite target;
        float grav = 0.5f;
        public DummyEn(ContentManager _con, Vector2 _pos, Sprite _target):base(_con, _pos)
        {
            target = _target;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), _pos, 0);
            velocity = new Vector2(0);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            foreach (Sprite _s in _sl)
            {
                if (_s == this || _s.GetType() == typeof(Player)) { continue; }
                //apply gravity if not touching ground
                if (!checkTopCol(_s))
                {
                    if (velocity.Y < 12f)
                    {
                        velocity.Y += grav;
                    }
                }
                else
                {
                    if(velocity.Y < 0)
                    velocity.Y = 0;
                    anim.position.Y = _s.anim.desRect.Top - anim.spriteSize.Y-10;
                }
            }
            //chase if player gets close enough
            if (distanceToTarget() < 100f) { isAlert = true; } else { isAlert = false; }

            switch (isAlert)
            {
                case true:
                    velocity.X = Math.Sign(target.anim.position.X - anim.position.X)*2;
                    break;
                case false:
                    velocity.X = 0;
                    break;
            }
            base.Update(_gt, _sl);
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
    }
}

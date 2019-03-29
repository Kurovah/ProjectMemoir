using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    class DummyEn:PhysObject
    {
        bool isAlert;
        Sprite target;
        public DummyEn(ContentManager _con, Vector2 _pos, Sprite _target):base(_con, _pos)
        {
            target = _target;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos, 0, Color.Aqua);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
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
            if (!IsGrounded(_sl))
            {
                Applygravity();
            }
            base.Update(_gt, _sl);
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
    }
}

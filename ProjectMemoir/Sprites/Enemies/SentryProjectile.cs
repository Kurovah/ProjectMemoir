﻿using System;
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
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(10), _pos, 0, Color.PaleVioletRed);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {

            if (anim.desRect.Intersects(target.anim.desRect))
            {
                target.hp -= 5;
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
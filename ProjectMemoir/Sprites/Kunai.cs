using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites.Enemies;
using ProjectMemoir.Scenes;
namespace ProjectMemoir.Sprites
{
    public class Kunai:PhysObject
    {


        int facing;
        private bool checked_list;
        List<Prowler> hitList;
        public Kunai(ContentManager _con, Vector2 _pos, int _side, Scene _parentScene) : base(_con, _pos,_parentScene)
        {
            facing = _side;
            velocity = new Vector2(20 * _side, 0);
            anim = new Animation(_con.Load<Texture2D>("playersprites/kunai"), new Vector2(29, 9), new Vector2(29, 9), _pos, 0, Color.White);
            grav = 0;
            hitList = new List<Prowler>();
            checked_list = false;
            
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (!checked_list) { 
                foreach (Sprite _s in _sl)
                {
                    if (_s.GetType() == typeof(Prowler))
                    {
                        hitList.Add((Prowler)_s);
                    }
                }
                checked_list = true;
            }
            //flip the sprite based on facing value
            if (facing == 1)
            {
                anim.mirrored = SpriteEffects.None;
            } else
            {
                anim.mirrored = SpriteEffects.FlipHorizontally;
            }

            foreach (Sprite _s in _sl)
            {
                if (_s.GetType() == typeof(Solid))
                {
                    if (checkLeftCol(_s) || checkRightCol(_s) || checkTopCol(_s) || checkBottomCol(_s))
                        isVisible = false;
                }
            }
            foreach (Prowler _p in hitList)
            {
                if (anim.desRect.Intersects(_p.anim.desRect))
                {
                    _p.stuntime = 50;
                    _p.currentstate = Prowler.States.stunned;
                }
            }
            base.Update(_gt, _sl);
        }

    }
}

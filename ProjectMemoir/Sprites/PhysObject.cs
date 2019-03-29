using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ProjectMemoir.Sprites
{
    public class PhysObject:Sprite
    {
        float grav = 0.25f;
        public PhysObject(ContentManager _con, Vector2 _pos) : base(_con, _pos)
        {
            velocity = new Vector2(0);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            Collision(_sl);
            anim.position += velocity;
            base.Update(_gt, _sl);
        }
        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
        }
        public  void Applygravity()
        {

           if (velocity.Y < 12f) { velocity.Y += grav; }

        }
        public bool IsGrounded(List<Sprite> _sl)
        {
            foreach(Sprite _s in _sl)
            {
                if(_s.GetType() != typeof(Solid)) { continue; }
                if (checkGroundCol(_s)) { return true; }
                
            }
            return false;
        }
        public void Collision(List<Sprite> _sl)
        {
            foreach (Sprite _s in _sl)
            {
                //dont' collide with non solid sprites
                if (_s.GetType() != typeof(Solid)) { continue; }
                //lateral
                if (velocity.X > 0 && checkLeftCol(_s))
                {
                    velocity.X = 0;
                    anim.position.X = _s.anim.desRect.Left - anim.desRect.Width;
                }

                if (velocity.X < 0 && checkRightCol(_s))
                {
                    velocity.X = 0;
                    anim.position.X = _s.anim.desRect.Right;
                }

                //vertical
                if (velocity.Y > 0 && checkTopCol(_s))
                {
                    velocity.Y = 0;
                    anim.position.Y = _s.anim.desRect.Top - anim.desRect.Height;
                }

                if (velocity.Y < 0 && checkBottomCol(_s))
                {
                    velocity.Y = 0;
                    anim.position.Y = _s.anim.desRect.Bottom;
                }
            }
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

        //need this to check for ground
        public bool checkGroundCol(Sprite _s)
        {
            return anim.desRect.Right > _s.anim.desRect.Left &&
                anim.desRect.Left < _s.anim.desRect.Right &&
                anim.desRect.Top < _s.anim.desRect.Top &&
                anim.desRect.Bottom + 1f > _s.anim.desRect.Top
                ;
        }
        #endregion
    }
}

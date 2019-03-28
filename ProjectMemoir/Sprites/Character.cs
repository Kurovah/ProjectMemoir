using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ProjectMemoir.Sprites
{
    public class Player:Sprite
    {
        private Texture2D _texture;
        public Vector2 velocity;
        KeyboardState currentKS;
        float spd = 5f, grav = 0.5f;
        bool hasJumped;


        public Player(ContentManager _con, Vector2 _pos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), _pos, 0);
            hasJumped = true;
            velocity = new Vector2(0);
        }

        public override void  Update(GameTime _gt, List<Sprite> _sl)
        {
            currentKS = Keyboard.GetState();
            Move(_sl);
            Collision(_sl);
            anim.position += velocity;
            base.Update(_gt, _sl);
        }
        public void Move(List<Sprite> _sl)
        {
            //lateral movement
            if (currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D))//left move
            {
                velocity.X = -spd;
            }
            else if(currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A))//right move
            {
                velocity.X = spd;
            } else
            {
                velocity.X = 0;
            }

            //vertical movement (gravity an jumping)
            foreach(Sprite _s in _sl)
            {
                if(_s == this) { continue; }
                if (!checkTopCol(_s) & velocity.Y < 10f)
                {
                    velocity.Y += grav;
                }
            }

        }
        public void Collision(List<Sprite> _sl)
        {
            foreach(Sprite _s in _sl){
                if(_s == this) { continue; }
                //lateral
                if(velocity.X > 0 && checkLeftCol(_s))
                {
                    velocity.X = 0;
                    anim.desRect.X = _s.anim.desRect.Left - anim.desRect.Width;
                }

                if (velocity.X < 0 && checkRightCol(_s))
                {
                    velocity.X = 0;
                    anim.desRect.X = _s.anim.desRect.Right;
                }

                //vertical
                if (velocity.Y > 0 && checkLeftCol(_s))
                {
                    velocity.X = 0;
                    anim.desRect.X = _s.anim.desRect.Left - anim.desRect.Width;
                }

                if (velocity.X < 0 && checkRightCol(_s))
                {
                    velocity.X = 0;
                    anim.desRect.X = _s.anim.desRect.Right;
                }
            }
        }

# region collision
        //checking if you touching the corresponding side of sprite "_S"
        public bool checkLeftCol(Sprite _s)
        {
            return anim.desRect.Left < _s.anim.desRect.Left &&
                anim.desRect.Right+velocity.X > _s.anim.desRect.Left &&
                anim.desRect.Top < _s.anim.desRect.Top &&
                anim.desRect.Bottom > _s.anim.desRect.Bottom
                ;
        }
        public bool checkRightCol(Sprite _s)
        {
            return anim.desRect.Right > _s.anim.desRect.Right &&
                anim.desRect.Left + velocity.X > _s.anim.desRect.Right &&
                anim.desRect.Top < _s.anim.desRect.Top &&
                anim.desRect.Bottom > _s.anim.desRect.Bottom
                ;
        }
        public bool checkTopCol(Sprite _s)
        {
            return anim.desRect.Left > _s.anim.desRect.Left &&
                anim.desRect.Right < _s.anim.desRect.Right &&
                anim.desRect.Top < _s.anim.desRect.Top &&
                anim.desRect.Bottom +velocity.Y > _s.anim.desRect.Top
                ;
        }
        public bool checkBottomCol(Sprite _s)
        {
            return anim.desRect.Left < _s.anim.desRect.Left &&
                anim.desRect.Right > _s.anim.desRect.Right &&
                anim.desRect.Top+velocity.Y < _s.anim.desRect.Bottom &&
                anim.desRect.Bottom > _s.anim.desRect.Bottom
                ;
        }
#endregion
    }
}

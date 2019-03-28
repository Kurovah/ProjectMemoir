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
        KeyboardState currentKS;
        float spd = 5f, grav = 0.25f;


        public Player(ContentManager _con, Vector2 _pos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), _pos, 0);
            velocity = new Vector2(0);
        }

        public override void  Update(GameTime _gt, List<Sprite> _sl)
        {
            currentKS = Keyboard.GetState();
            Move(_sl);
            Collision(_sl);
            base.Update(_gt, _sl);
        }
        public void Move(List<Sprite> _sl)
        {
            //lateral movement
            if (currentKS.IsKeyDown(Keys.A))//left move
            {
                velocity.X = -spd;
            }
            if(currentKS.IsKeyDown(Keys.D))//right move
            {
                velocity.X = spd;
            }
            if(!currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D) && currentKS.IsKeyDown(Keys.A))
            {
                velocity.X = 0;
            }

            foreach (Sprite _s in _sl)
            {
                if (_s == this || _s.GetType() == typeof(DummyEn)) { continue; }
                //apply gravity if not touching ground
                if (!checkTopCol(_s))
                {
                    if (velocity.Y < 12f)
                    {
                        velocity.Y += grav;
                    }
                } else
                {
                    //can jump if grounded
                    if (currentKS.IsKeyDown(Keys.J))
                    {
                        velocity.Y = -12;
                    }
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


    }
}

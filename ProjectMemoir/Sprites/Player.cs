using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ProjectMemoir.Sprites
{
    public class Player:PhysObject
    {
        KeyboardState currentKS;
        float spd = 5f;
        public int hp = 100, maxHp = 100;
        SpriteFont txt;
        bool g;
        public Player(ContentManager _con, Vector2 _pos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(40), new Vector2(32), _pos, 0, Color.Red);
            txt = _con.Load<SpriteFont>("Font");
        }

        public override void  Update(GameTime _gt, List<Sprite> _sl)
        {
            g = IsGrounded(_sl); 
            currentKS = Keyboard.GetState();
            Move(_sl);
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

            if (IsGrounded(_sl)) {
                if (currentKS.IsKeyDown(Keys.J)) {
                    velocity.Y += -8f;
                }
            }
            else
            {
                Applygravity();
            }
        }
        public override void Draw(SpriteBatch _sb)
        {
            //text for debugging
            _sb.DrawString(txt, "XPos:" + anim.position.X, anim.position - new Vector2(0, 55), Color.Black);
            _sb.DrawString(txt, "YPos:" + anim.position.Y, anim.position - new Vector2(0, 45), Color.Black);
            _sb.DrawString(txt, "Xvel:" + velocity.X, anim.position - new Vector2(0, 35), Color.Black);
            _sb.DrawString(txt, "Yvel:" + velocity.Y, anim.position - new Vector2(0, 25), Color.Black);
            _sb.DrawString(txt, "Grounded:" + g, anim.position - new Vector2(0, 15), Color.Black);
            base.Draw(_sb);
        }
    }
}

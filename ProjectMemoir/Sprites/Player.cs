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

        public Player(ContentManager _con, Vector2 _pos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos, 0, Color.Red);
        }

        public override void  Update(GameTime _gt, List<Sprite> _sl)
        {
            currentKS = Keyboard.GetState();
            DebugHealthChange();
            Move(_sl);
            base.Update(_gt, _sl);
        }
        public void DebugHealthChange()
        {
            if (currentKS.IsKeyDown(Keys.I))
            {
                hp -= 1;
            }
            if (currentKS.IsKeyDown(Keys.O))
            {
                hp += 1;
            }
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

            if (IsGrounded(_sl) && currentKS.IsKeyDown(Keys.J)) { velocity.Y = -12f; }
        }
        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
        }
    }
}

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
        enum playerStates
        {
            normal,
            upspecial,
            downspecial,
            sidespecial,
            nuetralspecial,
            hurt
        };

        KeyboardState currentKS;
        float spd = 5f, delay = 0;
        public int hp = 100, maxHp = 100;
        int jumpCount = 0, triggerCount = 0;
        SpriteFont txt;
        bool g, trigger = false;
        playerStates currentState = playerStates.normal;

        public Player(ContentManager _con, Vector2 _pos):base(_con, _pos)
        {
            currentState = playerStates.normal;
            anim = new Animation(_con.Load<Texture2D>("playersprites/player_idle"), new Vector2(55), new Vector2(55), _pos, 4, Color.White);
            anim.maxDelay = 3f;
            txt = _con.Load<SpriteFont>("Font");
        }

        public override void  Update(GameTime _gt, List<Sprite> _sl)
        {
            g = IsGrounded(_sl); 
            currentKS = Keyboard.GetState();

            //state machine
            switch (currentState)
            {
                case playerStates.normal:
                    Move(_sl);

                    if (jumpCount >= 1)
                        jumpCount = 0;
                    if (triggerCount > 0)
                        triggerCount -= 1;

                    if (currentKS.IsKeyDown(Keys.W) && currentKS.IsKeyDown(Keys.K) && triggerCount == 0) //For double jump
                    {
                        currentState = playerStates.upspecial;
                        triggerCount = 64;
                    }
                    break;

                case playerStates.upspecial:
                    
                    while (jumpCount < 1)
                    {                      
                        velocity.Y += -7f;                           
                        jumpCount++;                      
                    }

                    currentState = playerStates.normal;
                    break;
            }

            base.Update(_gt, _sl);
        }

        public void Move(List<Sprite> _sl)
        {

            if (IsGrounded(_sl)) {
                if (currentKS.IsKeyDown(Keys.J)) {
                    velocity.Y += -8f;
                    anim.currentframe = 0;
                }
                #region movement
                //lateral movement
                if (currentKS.IsKeyDown(Keys.A))//left move
                {
                    //changing animation
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    anim.maxDelay = 2f;
                    if (velocity.X >= 0) { anim.currentframe = 0; }

                    velocity.X = -spd;
                    anim.mirrored = SpriteEffects.FlipHorizontally;
                }
                if (currentKS.IsKeyDown(Keys.D))//right move
                {
                    //changing animation
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    anim.maxDelay = 2f;
                    if (velocity.X <= 0) { anim.currentframe = 0; }

                    velocity.X = spd;
                    anim.mirrored = SpriteEffects.None;
                }
                if (!currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D) && currentKS.IsKeyDown(Keys.A))
                {
                    anim.tex = con.Load<Texture2D>("playersprites/player_idle");
                    anim.frames = 4;
                    anim.maxDelay = 3f;
                    if (velocity.X != 0) { anim.currentframe = 0; }
                    velocity.X = 0;
                }
                #endregion
            }
            else
            {
                anim.currentframe = 0;
                anim.tex = con.Load<Texture2D>("playersprites/player_air");
                anim.frames = 0;
                #region movement in air
                //lateral movement
                if (currentKS.IsKeyDown(Keys.A))//left move
                {
                    velocity.X = -spd * 0.75f;
                    anim.mirrored = SpriteEffects.FlipHorizontally;
                }
                if (currentKS.IsKeyDown(Keys.D))//right move
                {
                    velocity.X = spd * 0.75f;
                    anim.mirrored = SpriteEffects.None;
                }
                if (!currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D) && currentKS.IsKeyDown(Keys.A))
                {
                    velocity.X = 0;
                }
                #endregion
                Applygravity();
            }
        }
        public override void Draw(SpriteBatch _sb)
        {
            //text for debugging
            _sb.DrawString(txt, "AnimFrame:" + anim.currentframe, anim.position - new Vector2(0, 65), Color.Black);
            _sb.DrawString(txt, "XPos:" + anim.position.X, anim.position - new Vector2(0, 55), Color.Black);
            _sb.DrawString(txt, "YPos:" + anim.position.Y, anim.position - new Vector2(0, 45), Color.Black);
            _sb.DrawString(txt, "Xvel:" + velocity.X, anim.position - new Vector2(0, 35), Color.Black);
            _sb.DrawString(txt, "Yvel:" + velocity.Y, anim.position - new Vector2(0, 25), Color.Black);
            _sb.DrawString(txt, "Grounded:" + g, anim.position - new Vector2(0, 15), Color.Black);
            base.Draw(_sb);
        }
    }
}

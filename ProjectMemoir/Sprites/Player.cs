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
            neutralspecial,
            hurt
        };

        KeyboardState currentKS;
        float spd = 5f, delay = 0;
        public int hp = 100, maxHp = 100;
        int jumpCount = 0, triggerCount = 0;
        SpriteFont txt;
        bool g, trigger= false;
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

                    #region using abilities
                    if (currentKS.IsKeyDown(Keys.W) && currentKS.IsKeyDown(Keys.K) && triggerCount == 0) //For double jump
                    {
                        currentState = playerStates.upspecial;
                        triggerCount = 64;
                    }

                    if (currentKS.IsKeyDown(Keys.S) && currentKS.IsKeyDown(Keys.K) && triggerCount == 0) //For dive
                    {
                        currentState = playerStates.downspecial;
                        triggerCount = 64;
                    }

                    if ((currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D)) && currentKS.IsKeyDown(Keys.K) && triggerCount == 0) //For roll
                    {
                        currentState = playerStates.sidespecial;
                        triggerCount = 64;
                    }

                    if (currentKS.IsKeyDown(Keys.K) && triggerCount == 0) //For projectile
                    {
                        currentState = playerStates.neutralspecial;
                        triggerCount = 64;
                    }
                    #endregion;
                    break;

                case playerStates.upspecial:
                    
                    if(jumpCount < 1)
                    {
                        anim.currentframe = 0;
                        velocity.Y = -10f;                           
                        jumpCount++;                      
                    }

                    currentState = playerStates.normal;
                    break;

                case playerStates.downspecial:

                    currentState = playerStates.normal;
                    break;

                case playerStates.neutralspecial:

                    currentState = playerStates.normal;
                    break;

                case playerStates.sidespecial:

                    currentState = playerStates.normal;
                    break;
            }
            animControl(_sl);
            base.Update(_gt, _sl);
        }

        //animation control
        private void animControl(List<Sprite> _sl)
        {
            switch (currentState)
            {
                case playerStates.normal:
                    if (IsGrounded(_sl))
                    {
                        if (currentKS.IsKeyDown(Keys.A) && currentKS.IsKeyDown(Keys.D) ||
                            !currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D))
                        {
                            if(velocity.X != 0) { anim.currentframe = 0; }
                            anim.tex = con.Load<Texture2D>("playersprites/player_idle");
                            anim.frames = 4;
                            anim.maxDelay = 2f;
                        } else
                        {
                            if (currentKS.IsKeyDown(Keys.A))
                            {
                                if (velocity.X >= 0) { anim.currentframe = 0; }
                                anim.tex = con.Load<Texture2D>("playersprites/player_run");
                                anim.frames = 8;
                                anim.mirrored = SpriteEffects.FlipHorizontally;
                                anim.maxDelay = 2f;
                            }
                            if (currentKS.IsKeyDown(Keys.D))
                            {
                                if (velocity.X <= 0) { anim.currentframe = 0; }
                                anim.tex = con.Load<Texture2D>("playersprites/player_run");
                                anim.frames = 8;
                                anim.mirrored = SpriteEffects.None;
                                anim.maxDelay = 2f;
                            }
                        }
                    } else
                    {
                        anim.tex = con.Load<Texture2D>("playersprites/player_air");
                        anim.frames = 0;
                        anim.currentframe = 0;
                    }
                    break;

                case playerStates.upspecial:
                    
                    break;

                case playerStates.downspecial:

                    break;

                case playerStates.neutralspecial:

                    break;

                case playerStates.sidespecial:

                    break;
            }
        }


        public void Move(List<Sprite> _sl)
        {
            //lateral movement
            if (!currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A) ||
                currentKS.IsKeyDown(Keys.D) && currentKS.IsKeyDown(Keys.A))
            {
                velocity.X = 0;
            } else
            {
                if (currentKS.IsKeyDown(Keys.A))//left move
                {
                    velocity.X = -spd;
                }
                if (currentKS.IsKeyDown(Keys.D))//right move
                {
                    velocity.X = spd;
                }
            }


            if (IsGrounded(_sl)) {
                //jumping
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
            _sb.DrawString(txt, "anim rect data SourceRectX:"+anim.sourceRect.X+" SourceRect Y:" + anim.sourceRect.Y, anim.position - new Vector2(0, 45), Color.Black);
            _sb.DrawString(txt, "AnimData Frame:" + anim.currentframe+ " currentTex:" + anim.tex, anim.position - new Vector2(0, 35), Color.Black);
            _sb.DrawString(txt, "XPos:" + anim.position.X + " YPos:" + anim.position.Y + " Yvel:" + velocity.Y+ " Xvel:" + velocity.X, anim.position - new Vector2(0, 25), Color.Black);
            _sb.DrawString(txt, "Grounded:" + g, anim.position - new Vector2(0, 15), Color.Black);
            base.Draw(_sb);
        }
    }
}

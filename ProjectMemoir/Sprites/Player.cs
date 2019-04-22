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
        float spd = 5f;
        public int hp = 100, maxHp = 100, facing = 1;
        SpriteFont txt;
        bool g, UScanuse = true, SScanuse = true, DScanuse = true, NScanuse = true;
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
                    playerNormalState(_sl);
                    break;

                case playerStates.upspecial:
                    
                    playerUpSpecial();
                    break;

                case playerStates.downspecial:

                    currentState = playerStates.normal;
                    break;

                case playerStates.neutralspecial:

                    currentState = playerStates.normal;
                    break;

                case playerStates.sidespecial:
                    playerSideSpecial(_sl);
                    break;
            }

            //flip the sprite based on facing value
            if(facing == 1)
            {
                anim.mirrored = SpriteEffects.None;
            } else
            {
                anim.mirrored = SpriteEffects.FlipHorizontally;
            }
            base.Update(_gt, _sl);
        }

        private void playerSideSpecial(List<Sprite> _sl)
        {
            SScanuse = false;
            if (IsGrounded(_sl))
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_dash_ground");
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_dash_air");
            }
            
            anim.frames = 12;

            if(anim.currentframe < 5 || anim.currentframe > 10)
            {
                velocity = new Vector2(0);
            } else
            {
                velocity.X = 10 * facing;
            }

            if (anim.isFinished())
            {
                currentState = playerStates.normal;
            }
        }
        private void playerUpSpecial()
        {
            UScanuse = false;
            anim.tex = con.Load<Texture2D>("playersprites/player_up_special");
            anim.sourcesize = anim.spriteSize = new Vector2(70);
            anim.frames = 5;

            //aplying upward velocity
            if(anim.currentframe == 0)
            {
                anim.spriteOrigin = new Vector2(0,15);
            }else if (anim.currentframe == 2) {
                    velocity.Y = -10f;
            }

            if((anim.currentframe == anim.frames))
            {
                currentState = playerStates.normal;
                anim.spriteOrigin = new Vector2(0, 0);
            }
        }

        public void playerNormalState(List<Sprite> _sl)
        {
            //making sure the sprite is the right size
            if (anim.sourcesize != new Vector2(55))
            {
                anim.sourcesize = anim.spriteSize = new Vector2(55);
            }
            #region using abilities
            

            //checking if the player wants to do a special move
            if (currentKS.IsKeyDown(Keys.K)) //For projectile
            {
                anim.currentframe = 0;
                if (currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D)) //For roll
                {
                    if (currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D)) { facing = -1; } else if (currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A)) { facing = 1; }
                    currentState = playerStates.sidespecial;
                }
                else if (currentKS.IsKeyDown(Keys.W) && UScanuse) //For double jump
                {
                    currentState = playerStates.upspecial;
                }
                else if (currentKS.IsKeyDown(Keys.S)) //For dive
                {
                    currentState = playerStates.downspecial;
                } else
                {
                    currentState = playerStates.neutralspecial;
                    
                }
            }
            #endregion;
            #region lateral movement
            if (!currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A) ||
                currentKS.IsKeyDown(Keys.D) && currentKS.IsKeyDown(Keys.A))
            {
                velocity.X = 0;
                if (velocity.X != 0) { anim.currentframe = 0; }
                anim.tex = con.Load<Texture2D>("playersprites/player_idle");
                anim.frames = 4;
                anim.maxDelay = 2f;
            } else {
                if (currentKS.IsKeyDown(Keys.A))//left move
                {
                    velocity.X = -spd;
                    if (velocity.X >= 0) { anim.currentframe = 0; }
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    facing = -1;
                    anim.maxDelay = 2f;
                }
                if (currentKS.IsKeyDown(Keys.D))//right move
                {
                    velocity.X = spd;
                    if (velocity.X <= 0) { anim.currentframe = 0; }
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    facing = 1;
                    anim.maxDelay = 2f;
                }
            }
            #endregion
            #region allowing for jump and checking if the player is airborne
            if (IsGrounded(_sl)) {
                //jumping
                if(UScanuse == false) { UScanuse = true; }
                if (currentKS.IsKeyDown(Keys.J)) {
                    velocity.Y += -9f;
                }
                
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_air");
                anim.frames = 0;
                anim.currentframe = 0;
                if (currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D)) {facing = -1;} else if (currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A)) { facing = 1;}
                Applygravity();
            }
            #endregion
            
           
        }
        public override void Draw(SpriteBatch _sb)
        {
            /*/text for debugging
            _sb.DrawString(txt, "anim rect data SourceRectX:"+anim.sourceRect.X+" SourceRect Y:" + anim.sourceRect.Y, anim.position - new Vector2(0, 45), Color.Black);
            _sb.DrawString(txt, "AnimData Frame:" + anim.currentframe+ " currentTex:" + anim.tex, anim.position - new Vector2(0, 35), Color.Black);
            _sb.DrawString(txt, "XPos:" + anim.position.X + " YPos:" + anim.position.Y + " Yvel:" + velocity.Y+ " Xvel:" + velocity.X + " stateCurrent:"+currentState, anim.position - new Vector2(0, 25), Color.Black);
            _sb.DrawString(txt, "Grounded:" + g, anim.position - new Vector2(0, 15), Color.Black);
            //*/
            base.Draw(_sb);
        }
    }
}

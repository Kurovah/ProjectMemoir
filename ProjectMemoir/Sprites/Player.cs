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
        public int hp = 100, maxHp = 100;
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
                    
                    playerUpSpecial(_sl);
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
            base.Update(_gt, _sl);
        }


        private void playerUpSpecial(List<Sprite> _sl)
        {
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
                    currentState = playerStates.sidespecial;
                }
                else if (currentKS.IsKeyDown(Keys.W) && UScanuse) //For double jump
                {
                    UScanuse = false;
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
                    anim.mirrored = SpriteEffects.FlipHorizontally;
                    anim.maxDelay = 2f;
                }
                if (currentKS.IsKeyDown(Keys.D))//right move
                {
                    velocity.X = spd;
                    if (velocity.X <= 0) { anim.currentframe = 0; }
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    anim.mirrored = SpriteEffects.None;
                    anim.maxDelay = 2f;
                }
            }
            #endregion
            #region allowing for jump and checking if the player is airborne
            if (IsGrounded(_sl)) {
                //jumping
                if(UScanuse == false) { UScanuse = true; }
                if (currentKS.IsKeyDown(Keys.J)) {
                    velocity.Y += -8f;
                }
                
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_air");
                anim.frames = 0;
                anim.currentframe = 0;
                if (currentKS.IsKeyDown(Keys.A)) {anim.mirrored = SpriteEffects.FlipHorizontally;} else {anim.mirrored = SpriteEffects.None;}
                Applygravity();
            }
            #endregion
            
           
        }
        public override void Draw(SpriteBatch _sb)
        {
            //text for debugging
            _sb.DrawString(txt, "anim rect data SourceRectX:"+anim.sourceRect.X+" SourceRect Y:" + anim.sourceRect.Y, anim.position - new Vector2(0, 45), Color.Black);
            _sb.DrawString(txt, "AnimData Frame:" + anim.currentframe+ " currentTex:" + anim.tex, anim.position - new Vector2(0, 35), Color.Black);
            _sb.DrawString(txt, "XPos:" + anim.position.X + " YPos:" + anim.position.Y + " Yvel:" + velocity.Y+ " Xvel:" + velocity.X + " stateCurrent:"+currentState, anim.position - new Vector2(0, 25), Color.Black);
            _sb.DrawString(txt, "Grounded:" + g, anim.position - new Vector2(0, 15), Color.Black);
            base.Draw(_sb);
        }
    }
}

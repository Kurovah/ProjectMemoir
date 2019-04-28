﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ProjectMemoir.Components;
using Microsoft.Xna.Framework.Audio;

namespace ProjectMemoir.Sprites
{
    public class Player:PhysObject
    {
        public enum playerStates
        {
            normal,
            upspecial,
            downspecial,
            sidespecial,
            neutralspecial,
            hurt
        };
        List<Kunai> kl;
        KeyboardState currentKS;
        float spd = 5f;
        public int hp = 100, maxHp = 100, facing = 1, type = 0;
        public float itimer, stuntimer;
        public bool invincible;
        SpriteFont txt;
        bool g, UScanuse = true, startDive = false, SScanuse = false;
        public playerStates currentState = playerStates.normal;
        public PlayerStats ps;
        SoundEffect jumpEffect;

        public Player(ContentManager _con, Vector2 _pos, PlayerStats _ps):base(_con, _pos)
        {
            currentState = playerStates.normal;
            ps = _ps;
            anim = new Animation(_con.Load<Texture2D>("playersprites/player_idle"), new Vector2(55), new Vector2(55),_pos*32, 4, Color.White);
            anim.maxDelay = 3f;
            jumpEffect = _con.Load<SoundEffect>("sounds/Jump");
            txt = _con.Load<SpriteFont>("Font");
            kl = new List<Kunai>();
            itimer = -1f;
            stuntimer = -1f;
            invincible = false;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            g = IsGrounded(_sl); 
            currentKS = Keyboard.GetState();
            #region state machine
            switch (currentState)
            {
                case playerStates.normal:
                    playerNormalState(_sl);
                    break;

                case playerStates.upspecial:
                    playerUpSpecial();
                    break;

                case playerStates.downspecial:
                    playerDownSpecial(_sl);
                    break;

                case playerStates.neutralspecial:

                    playerNeutralSpecial(_sl);
                    break;

                case playerStates.sidespecial:
                    playerSideSpecial(_sl);
                    break;
                case playerStates.hurt:
                    playerHurtState(_sl);
                    break;
            }
            #endregion
            
            //flip the sprite based on facing value
            if (facing == 1)
            {
                anim.mirrored = SpriteEffects.None;
            } else
            {
                anim.mirrored = SpriteEffects.FlipHorizontally;
            }

            #region updating the list of kunai
            foreach (Kunai _sp in kl)
            {
                _sp.Update(_gt, _sl);
            }
            for (int i = 0; i < kl.Count; i++)
            {
                if (!kl[i].isVisible)
                {
                    kl.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            base.Update(_gt, _sl);
        }
        private void playerDownSpecial(List<Sprite> _sl)
        {
            
            if (!startDive)
            {
                
                if (IsGrounded(_sl))
                {
                    anim.tex = con.Load<Texture2D>("playersprites/player_ground_smash");
                    anim.frames = 4;
                    startDive = true;
                    type = 0;
                }
                else
                {
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_start");
                    anim.frames = 3;
                    type = 1;
                    startDive = true;
                }
            }
            //different phases of the smash
            switch (type)
            {
                case 0://grounded
                    if (anim.isFinished())
                    {
                        currentState = playerStates.normal;
                        startDive = false;
                        
                    }
                    break;
                case 1://air start
                    velocity = new Vector2(0,-6);
                    if (anim.isFinished())
                    {
                        anim.currentframe = 0;
                        type = 2;
                    }
                    break;
                case 2://loop air
                    velocity = new Vector2(2*facing, 10);
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_loop");
                    anim.frames = 0;
                    //smashing breakable blocks
                    foreach (Sprite _s in _sl)
                    {
                        if(checkTopCol(_s) && _s.GetType() == typeof(BreakableBlock))
                        {
                            _s.isVisible = false;
                        }
                    }

                    if (IsGrounded(_sl))
                    {
                        type = 3;
                        anim.currentframe = 0;
                    }
                    break;
                case 3:
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_end");
                    anim.frames = 2;
                    if (anim.isFinished())
                    {
                        startDive = false;
                        currentState = playerStates.normal;
                        
                    }
                    break;
            }
            
        }
        private void playerNeutralSpecial(List<Sprite> _sl)
        {
            velocity = Vector2.Zero;
            if (IsGrounded(_sl))
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_kunai_toss_ground");
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_kunai_air");
            }

            anim.frames = 4;

            if (anim.currentframe == 2)
            {
                kl.Add(new Kunai(con, new Vector2(anim.position.X+27+27*facing, anim.position.Y+27), facing));
            }

            if (anim.isFinished())
            {
                currentState = playerStates.normal;
            }
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
                invincible = false;
            } else
            {
                velocity.X = 10 * facing;
                invincible = true;
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
            if (itimer > 0)
            {
                itimer -= 0.1f;
            }
            else
            {
                invincible = false;
            }
            //making sure the sprite is the right size
            if (anim.sourcesize != new Vector2(55))
            {
                anim.sourcesize = anim.spriteSize = new Vector2(55);
            }
            #region using abilities
            

            //checking if the player wants to do a special move
            if (currentKS.IsKeyDown(Keys.K)) //For projectile
            {
                
                if ((currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D)) && ps.abilities["Side"]) //For roll
                {
                    if (currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D)) { facing = -1; } else if (currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A)) { facing = 1; }
                    currentState = playerStates.sidespecial;
                }
                else if (currentKS.IsKeyDown(Keys.W) && UScanuse && ps.abilities["Up"]) //For double jump
                {
                    currentState = playerStates.upspecial;
                }
                else if (currentKS.IsKeyDown(Keys.S) && ps.abilities["Down"]) //For dive
                {
                    currentState = playerStates.downspecial;
                } else if(ps.abilities["Neutral"])
                {
                    currentState = playerStates.neutralspecial;
                    anim.currentframe = 0;
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
                    jumpEffect.Play();
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
        public void playerHurtState(List<Sprite> _sl)
        {
            if (stuntimer < 0)
            {
                stuntimer = 40;
            }
            Applygravity();
            if (stuntimer > 0 || !IsGrounded(_sl))
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_hurt");
                anim.currentframe = 0;
                anim.frames = 0;
                invincible = true;
                stuntimer -= 0.1f;
            } else
            {
                stuntimer = -1;
                itimer = 50;
                currentState = playerStates.normal;
            }
        }
        public override void Draw(SpriteBatch _sb)
        {
            _sb.DrawString(txt,"state:" + currentState+
                " itimer:"+itimer+
                " hurt:"+invincible
                , new Vector2(32), Color.White);
            foreach (Kunai _k in kl)
            {
                _k.Draw(_sb);
            }
            base.Draw(_sb);
        }
    }
}

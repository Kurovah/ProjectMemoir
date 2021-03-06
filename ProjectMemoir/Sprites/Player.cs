﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ProjectMemoir.Components;
using Microsoft.Xna.Framework.Audio;
using ProjectMemoir.Scenes;

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
        public KeyboardState currentKS;
        float spd = 5f;
        public int hp = 100, maxHp = 100, facing = 1, type = 0, hurtcount = 1, sidecount = 1, walkTime = -1, upcount = 1;
        public float itimer, stuntimer;
        public bool invincible;
        SpriteFont txt;
        bool grounded,lastgrounded, UScanuse = true, startDive = false, SScanuse = false;
        public playerStates currentState = playerStates.normal;
        public PlayerStats ps;
        InputManager input;
        public Player(ContentManager _con, Vector2 _pos, Scene _parentScene):base(_con, _pos, _parentScene)
        {
            txt = _con.Load<SpriteFont>("font");
            currentState = playerStates.normal;
            ps = _parentScene.game.ps;
            anim = new Animation(_con.Load<Texture2D>("playersprites/player_idle"), new Vector2(55), new Vector2(55),_pos*32 + new Vector2(0,9), 4, Color.White);
            anim.maxDelay = 3f;
            kl = new List<Kunai>();
            itimer = -1f;
            stuntimer = -1f;
            invincible = false;
            grounded = lastgrounded = true;
            input = _parentScene.game.input;
            
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            grounded = IsGrounded(_sl);
            if (invincible) { anim.alpha = 0.75f; } else { anim.alpha = 1; }
            #region state machine
            switch (currentState)
            {
                case playerStates.normal:
                    playerNormalState();
                    break;

                case playerStates.upspecial:
                    playerUpSpecial();
                    break;

                case playerStates.downspecial:
                    playerDownSpecial(_sl);
                    break;

                case playerStates.neutralspecial:

                    playerNeutralSpecial();
                    break;

                case playerStates.sidespecial:
                    playerSideSpecial();
                    break;
                case playerStates.hurt:
                    playerHurtState();
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
                    parentScene.soundManager.kunaiClink.Play();
                    kl.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            lastgrounded = grounded;
            base.Update(_gt, _sl);
        }
        private void playerDownSpecial(List<Sprite> _sl)
        {
            
            if (!startDive)
            {
                
                if (grounded)
                {
                    anim.tex = con.Load<Texture2D>("playersprites/player_ground_smash");
                    anim.frames = 4;
                    startDive = true;
                    type = 0;
                    anim.delay = 0;
                }
                else
                {
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_start");
                    anim.frames = 3;
                    type = 1;
                    startDive = true;
                    anim.delay = 0;
                }
            }
            //different phases of the smash
            switch (type)
            {
                case 0://grounded
                    if (anim.currentframe == 4 && this.anim.delay == 0f)
                    {
                        parentScene.soundManager.playerCrush.Play();
                        parentScene.vfxQ.Add(new VFX(this.con, new Vector2(anim.position.X+55-59, anim.position.Y - 39 + 55), this.parentScene, "Vfx/vfx_groundsmash", new Vector2(59, 39), 4));
                        foreach (Sprite _s in _sl)
                        {
                            if (checkGroundCol(_s) && _s.GetType() == typeof(BreakableBlock))
                            {
                                _s.isVisible = false;
                            }
                        }
                    }
                    if (anim.isFinished())
                    {
                        currentState = playerStates.normal;
                        startDive = false;
                        anim.delay = 0;

                    }
                    break;
                case 1://air start
                    velocity = new Vector2(0,-6);
                    if (anim.isFinished())
                    {
                        anim.currentframe = 0;
                        anim.delay = 0;
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
                            parentScene.soundManager.BrickCrush.Play();
                            _s.isVisible = false;
                        }
                    }

                    if (grounded)
                    {
                        parentScene.soundManager.playerCrush.Play();
                        type = 3;
                        anim.currentframe = 0;
                    }
                    break;
                case 3:
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_end");
                    anim.frames = 2;
                    if(anim.currentframe == 0 && this.anim.delay == 0f)
                    {
                        parentScene.soundManager.playerCrush.Play();
                        parentScene.vfxQ.Add(new VFX(this.con, new Vector2(anim.position.X, anim.position.Y - 39 + 55),this.parentScene, "Vfx/vfx_groundsmash", new Vector2(59,39),4));
                    }

                    if (anim.isFinished())
                    {
                        startDive = false;
                        currentState = playerStates.normal;
                        anim.delay = 0;
                    }
                    break;
            }
            
        }
        private void playerNeutralSpecial()
        {
            velocity = Vector2.Zero;
            if (grounded)
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_kunai_toss_ground");
                anim.frames = 4;
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_kunai_air");
                anim.frames = 4;
            }

            

            if (anim.currentframe == 2  && anim.delay == 0)
            {
                parentScene.soundManager.kunaiToss.Play();
                kl.Add(new Kunai(con, new Vector2(anim.position.X+27+27*facing, anim.position.Y+27), facing, this.parentScene));
            }

            if (anim.isFinished())
            {
                currentState = playerStates.normal;
            }
        }
        private void playerSideSpecial()
        {
            SScanuse = false;

            if (grounded)
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_dash_ground");
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_dash_air");
            }
            
            anim.frames = 12;

            if(anim.currentframe < 4 || anim.currentframe > 10)
            {
                velocity = new Vector2(0);
                invincible = false;
                if (sidecount == 1)
                {
                    parentScene.soundManager.playerAquaDash.Play();
                    sidecount -= 1;
                }
            }
            else
            {
                velocity.X = 10 * facing;
                invincible = true;
            }

            if (anim.isFinished())
            {
                currentState = playerStates.normal;
                sidecount = 1;
            }
        }
        private void playerUpSpecial()
        {
            UScanuse = false;
            anim.tex = con.Load<Texture2D>("playersprites/player_up_special");
            anim.sourcesize = anim.spriteSize = new Vector2(70);
            anim.frames = 5;

            if (upcount == 1)
            {
                //parentScene.soundManager.playerJump.Play();
                parentScene.soundManager.playerFlashFlip.Play();
                upcount -= 1;
            }
            //aplying upward velocity
            if(anim.currentframe == 0)
            {
                anim.spriteOrigin = new Vector2(15);
            }
            else if (anim.currentframe == 2) {
                    velocity.Y = -8f;
                    anim.spriteOrigin = new Vector2(0, 0);
            }

            if((anim.currentframe == anim.frames))
            {
                currentState = playerStates.normal;
                upcount = 1;               
            }
        }
        public void playerNormalState()
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
            if (input.ActionInput) //For projectile
            {
                
                if ((input.LeftInputH || input.RightInputH) && ps.abilities["Side"]) //For roll
                {
                    if (input.LeftInput && !input.RightInput) { facing = -1; } else if (input.RightInput && !input.LeftInput) { facing = 1; }
                    currentState = playerStates.sidespecial;
                    anim.delay = 0;
                    anim.currentframe = 0;
                }
                else if (input.UpInputH && UScanuse && ps.abilities["Up"]) //For double jump
                {
                    if (grounded != true)
                    {
                        currentState = playerStates.upspecial;
                        anim.delay = 0;
                        anim.currentframe = 0;
                    }
                }
                else if (input.DownInputH && ps.abilities["Down"]) //For dive
                {
                    currentState = playerStates.downspecial;
                    anim.delay = 0;
                    anim.currentframe = 0;
                    
                } else if(ps.abilities["Neutral"] )
                {
                    currentState = playerStates.neutralspecial;
                    anim.delay = 0;
                    anim.currentframe = 0;
                }
            }
            #endregion;
            #region lateral movement
            if (!input.RightInputH && !input.LeftInputH ||
                input.RightInputH && input.LeftInputH)
            {
                velocity.X = 0;
                if (velocity.X != 0) { anim.currentframe = 0; anim.delay = 0; }
                anim.tex = con.Load<Texture2D>("playersprites/player_idle");
                anim.frames = 4;
                anim.maxDelay = 2f;
            } else {
                if (input.LeftInputH)//left move
                {
                    walkTime += 1;
                    velocity.X = -spd;
                    if (velocity.X >= 0) { anim.currentframe = 0; anim.delay = 0; }
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    facing = -1;
                    anim.maxDelay = 2f;
                    if (grounded)
                    {
                        if ((walkTime % 19f) == 0)
                        {
                            walkTime = 0;
                            parentScene.soundManager.playerRun.Play();
                        }
                    }
                }
                if (input.RightInputH)//right move
                {
                    walkTime += 1;
                    velocity.X = spd;
                    if (velocity.X <= 0) { anim.currentframe = 0; anim.delay = 0; }
                    anim.tex = con.Load<Texture2D>("playersprites/player_run");
                    anim.frames = 8;
                    facing = 1;
                    anim.maxDelay = 2f;
                    if (grounded)
                    {
                        if ((walkTime % 19f) == 0)
                        {
                            walkTime = 0;
                            parentScene.soundManager.playerRun.Play();
                        }
                    }
                }
            }
            #endregion
            #region allowing for jump and checking if the player is airborne
            if (grounded) {
                //jumping
                if (!lastgrounded)
                {
                    parentScene.vfxQ.Add(new VFX(this.con, new Vector2(anim.position.X + 7, anim.position.Y + anim.spriteSize.Y - 16), this.parentScene, "Vfx/vfx_landingdust", new Vector2(41, 17), 4));
                }
                if(UScanuse == false) { UScanuse = true; }
                if (input.JumpInput) {
                    velocity.Y += -9f;
                    parentScene.soundManager.playerJump.Play();
                }
                
            }
            else
            {
                anim.tex = con.Load<Texture2D>("playersprites/player_air");
                anim.frames = 0;
                anim.currentframe = 0;
                if (input.LeftInput && !input.RightInput) {facing = -1;} else if (input.RightInput && !input.LeftInput) { facing = 1;}
                Applygravity();
            }
            #endregion
            
           
        }
        public void playerHurtState()
        {
            Applygravity();
            if (hurtcount > 0)
            {
                hurtcount -= 1;
                parentScene.soundManager.playerGetHurt.Play();
            }
            if (stuntimer > 0)
            {
                
                anim.tex = con.Load<Texture2D>("playersprites/player_hurt");
                anim.currentframe = 0;
                anim.frames = 0;
                invincible = true;
                stuntimer -= 0.1f;
                //the stun ends prematurely when you land
                if (grounded && velocity.Y > 0)
                {
                    stuntimer = 0;
                }
            }
            else
            {
                stuntimer = -1;
                itimer = 10;
                hurtcount = 1;
                currentState = playerStates.normal;
            }
        }
        public void getHurt(float _xvel, float _yvel)
        {
            if (anim.sourcesize != new Vector2(55))
            {
                anim.sourcesize = anim.spriteSize = new Vector2(55);
            }
            anim.spriteOrigin = new Vector2(0, 0);
            ps.hp -= 1;
            if (ps.hp <= 0) {
                parentScene.game.nextScene = new GameOver(parentScene.game, con);
            }
            else
            {
                currentState = Player.playerStates.hurt;
                velocity = new Vector2(_xvel, _yvel);
                stuntimer = 10;
            }
        }
        public override void Draw(SpriteBatch _sb)
        {
            //_sb.DrawString(txt, "anim.CurrentFrame:" + anim.currentframe + " anim.delay:" + anim.delay, new Vector2(20,40), Color.White);
            //_sb.DrawString(txt, "size:"+parentScene.vfxList.Count, new Vector2(20,60), Color.White);
            foreach (Kunai _k in kl)
            {
                _k.Draw(_sb);
            }
            base.Draw(_sb);
        }
    }
}

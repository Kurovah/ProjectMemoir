using Microsoft.Xna.Framework;
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
        KeyboardState currentKS;
        float spd = 5f;
        public int hp = 100, maxHp = 100, facing = 1, type = 0;
        public float itimer, stuntimer;
        public bool invincible;
        SpriteFont txt;
        bool kunaiCreated,effectCreated,grounded, UScanuse = true, startDive = false, SScanuse = false;
        public playerStates currentState = playerStates.normal;
        public PlayerStats ps;
        
        public Player(ContentManager _con, Vector2 _pos, Scene _parentScene):base(_con, _pos, _parentScene)
        {
            currentState = playerStates.normal;
            ps = _parentScene.game.ps;
            anim = new Animation(_con.Load<Texture2D>("playersprites/player_idle"), new Vector2(55), new Vector2(55),_pos*32, 4, Color.White);
            anim.maxDelay = 3f;
            kl = new List<Kunai>();
            itimer = -1f;
            stuntimer = -1f;
            invincible = false;
            grounded = true;
            kunaiCreated = false;
            effectCreated = false;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            grounded = IsGrounded(_sl);
            if (invincible) { anim.alpha = 0.75f; } else { anim.alpha = 1; }
            currentKS = Keyboard.GetState();
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
                
                if (grounded)
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
                    if (anim.currentframe == 4 && !effectCreated)
                    {
                        effectCreated = true;
                        parentScene.vfxQ.Add(new VFX(this.con, new Vector2(anim.position.X, anim.position.Y - 39 + 55), this.parentScene, "Vfx/vfx_groundsmash", new Vector2(59, 39), 4));
                    }
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

                    if (grounded)
                    {
                        type = 3;
                        anim.currentframe = 0;
                    }
                    break;
                case 3:
                    anim.tex = con.Load<Texture2D>("playersprites/player_air_smash_end");
                    anim.frames = 2;
                    if(anim.currentframe == 0 && !effectCreated)
                    {
                        effectCreated = true;
                        parentScene.vfxQ.Add(new VFX(this.con, new Vector2(anim.position.X, anim.position.Y - 39 + 55),this.parentScene, "Vfx/vfx_groundsmash", new Vector2(59,39),4));
                    }

                    if (anim.isFinished())
                    {
                        startDive = false;
                        currentState = playerStates.normal;
                        
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

            

            if (anim.currentframe == 2  && !kunaiCreated)
            {
                kunaiCreated = true;
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
            if (currentKS.IsKeyDown(Keys.K)) //For projectile
            {
                
                if ((currentKS.IsKeyDown(Keys.A) || currentKS.IsKeyDown(Keys.D)) && ps.abilities["Side"]) //For roll
                {
                    if (currentKS.IsKeyDown(Keys.A) && !currentKS.IsKeyDown(Keys.D)) { facing = -1; } else if (currentKS.IsKeyDown(Keys.D) && !currentKS.IsKeyDown(Keys.A)) { facing = 1; }
                    currentState = playerStates.sidespecial;
                    anim.currentframe = 0;
                }
                else if (currentKS.IsKeyDown(Keys.W) && UScanuse && ps.abilities["Up"]) //For double jump
                {
                    currentState = playerStates.upspecial;
                    anim.currentframe = 0;
                }
                else if (currentKS.IsKeyDown(Keys.S) && ps.abilities["Down"]) //For dive
                {
                    effectCreated = false;
                    currentState = playerStates.downspecial;
                    anim.currentframe = 0;
                } else if(ps.abilities["Neutral"] )
                {
                    currentState = playerStates.neutralspecial;
                    kunaiCreated = false;
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
            if (grounded) {
                //jumping
                if(UScanuse == false) { UScanuse = true; }
                if (currentKS.IsKeyDown(Keys.J)) {
                    velocity.Y += -9f;
                    parentScene.soundManager.playerJump.Play();
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
        public void playerHurtState()
        {
            Applygravity();
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
            } else
            {
                stuntimer = -1;
                itimer = 10;
                currentState = playerStates.normal;
            }
        }
        public void getHurt(float _xvel, float _yvel)
        {
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
            foreach (Kunai _k in kl)
            {
                _k.Draw(_sb);
            }
            base.Draw(_sb);
        }
    }
}

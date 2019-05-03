using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites.Enemies
{
    

    public class Charger:PhysObject
    {
        enum States
        {
            idle,
            chargeup,
            charging
        };
        private Player target;
        private int facing;
        private States currentState;
        private Animation sight;
        private float turntime;
        public Charger(ContentManager _con, Vector2 _pos, Gamescene _parentScene) :base(_con, _pos, _parentScene)
        {
            target = _parentScene.player;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/charger_idle"), new Vector2(80), new Vector2(64), _pos, 0, Color.White);
            sight = new Animation(_con.Load<Texture2D>("enemySprites/enemy_sightrect"), new Vector2(160,64), new Vector2(96,64), _pos, 0, Color.White);
            sight.alpha = 1f;
            currentState = States.idle;
            facing = -1;
            turntime = 1;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            switch (currentState)
            {
                case States.idle:
                    if(turntime > 0)
                    {
                        turntime -= 0.01f;
                    } else
                    {
                        turntime = 1;
                        facing = -facing;
                    }
                    anim.tex = con.Load<Texture2D>("enemySprites/charger_idle");
                    anim.frames = 0;
                    sight.position.Y = anim.position.Y;
                    sight.mirrored = anim.mirrored;
                    if (facing == -1)
                    {
                        sight.position.X = anim.position.X - 160;
                    }
                    else
                    {
                        sight.position.X = anim.position.X + 80;
                    }
                    
                    sight.Update(_gt);
                    //only attack player if they are not invincible
                    if (canSeePlayer() && !target.invincible) {
                        facing = Math.Sign(target.anim.position.X - anim.position.X);
                        anim.currentframe = 0;
                        currentState = States.chargeup;
                        sight.alpha = 0;
                    }
                    break;

                case States.chargeup:
                    anim.tex = con.Load<Texture2D>("enemySprites/charger_charge_up");
                    anim.frames = 6;
                    if (anim.isFinished())
                    {
                        anim.currentframe = 0;
                        currentState = States.charging;
                        
                    }
                    break;

                case States.charging:
                    anim.tex = con.Load<Texture2D>("enemySprites/charger_attack");
                    anim.frames = 0;
                    velocity.X = facing*20f;

                    
                    foreach(Sprite _s in _sl)
                    {
                        if(_s.GetType() != typeof(Solid)) { continue; }
                        //crash into wall
                        if(checkLeftCol(_s)) {
                            currentState = States.idle;
                            velocity.X = 0;
                            anim.position.X = _s.anim.desRect.Left - anim.spriteSize.X;
                            sight.alpha = 1f;

                        }
                        else if (checkRightCol(_s))
                        {
                            currentState = States.idle;
                            velocity.X = 0;
                            anim.position.X = _s.anim.desRect.Right;
                            sight.alpha = 1f;
                        }
                    }

                       
                    //crash into player
                    if ((checkRightCol(target) || checkLeftCol(target) || checkTopCol(target) || checkBottomCol(target)) && !target.invincible) {
                        velocity.X = 0;
                        currentState = States.idle;
                        target.getHurt(Math.Sign(target.anim.position.X - anim.position.X)*4, -8);
                        sight.alpha = 1f;
                    }
                    break;
            }
            if(facing == -1) { anim.mirrored = SpriteEffects.None; } else { anim.mirrored = SpriteEffects.FlipHorizontally; }
            Applygravity();
            base.Update(_gt, _sl);
        }
        private bool canSeePlayer()
        {
            return sight.desRect.Intersects(target.anim.desRect);
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
        public override void Draw(SpriteBatch _sb)
        {
            sight.Draw(_sb);
            base.Draw(_sb);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        public Charger(ContentManager _con, Vector2 _pos, Player _target) :base(_con, _pos)
        {
            target = _target;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/charger_idle"), new Vector2(80), new Vector2(64), _pos, 0, Color.White);
            currentState = States.idle;
            facing = -1;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            switch (currentState)
            {
                case States.idle:
                    anim.tex = con.Load<Texture2D>("enemySprites/charger_idle");
                    anim.frames = 0;

                    //only attack player if they are not invincible
                    if (distanceToTarget() < 250f && canSeePlayer() && !target.invincible) {
                        facing = Math.Sign(target.anim.position.X - anim.position.X);
                        anim.currentframe = 0;
                        currentState = States.chargeup;
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
                        }
                        else if (checkRightCol(_s))
                        {
                            currentState = States.idle;
                            velocity.X = 0;
                            anim.position.X = _s.anim.desRect.Right;
                        }
                    }

                       
                    //crash into player
                    if ((checkRightCol(target) || checkLeftCol(target) || checkTopCol(target) || checkBottomCol(target)) && !target.invincible) {
                        velocity.X = 0;
                        currentState = States.idle;
                        target.getHurt(Math.Sign(target.anim.position.X - anim.position.X)*4, -8);
                    }
                    break;
            }
            if(facing == -1) { anim.mirrored = SpriteEffects.None; } else { anim.mirrored = SpriteEffects.FlipHorizontally; }
            Applygravity();
            base.Update(_gt, _sl);
        }
        private bool canSeePlayer()
        {
            float playerPoint = target.anim.position.Y + target.anim.spriteSize.Y / 2,
                anchor1 = anim.position.Y,
                anchor2 = anim.position.Y + anim.spriteSize.Y;
            return playerPoint < anchor2 && playerPoint > anchor1;
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
    }
}

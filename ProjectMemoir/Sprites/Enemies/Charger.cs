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
        private int charge,facing;
        private States currentState;
        public Charger(ContentManager _con, Vector2 _pos, Player _target) :base(_con, _pos)
        {
            target = _target;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(50), new Vector2(32), _pos, 0, Color.Maroon);
            charge = 500;
            currentState = States.idle;
            facing = 1;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            switch (currentState)
            {
                case States.idle:

                    if (distanceToTarget() < 400f && Math.Abs(target.anim.position.Y - anim.position.Y) < 20f) {
                        facing = Math.Sign(target.anim.position.X - anim.position.X); currentState = States.chargeup;
                    }
                    break;
                case States.chargeup:
                    if(charge > 0)
                    {
                        charge -= 15;
                    } else
                    {
                        currentState = States.charging;
                    }
                    break;
                case States.charging:
                    velocity.X = facing*50f;

                    
                    foreach(Sprite _s in _sl)
                    {
                        if(_s.GetType() != typeof(Solid)) { continue; }
                        //crash into wall
                        if(checkLeftCol(_s)) {
                            charge = 500;
                            currentState = States.idle;
                            velocity.X = 0;
                            anim.position.X = _s.anim.desRect.Left - anim.spriteSize.X;
                        }
                        else if (checkRightCol(_s))
                        {
                            charge = 500;
                            currentState = States.idle;
                            velocity.X = 0;
                            anim.position.X = _s.anim.desRect.Right;
                        }
                    }

                       
                    //crash into player
                    if (checkRightCol(target) || checkLeftCol(target) || checkTopCol(target) || checkBottomCol(target)) {
                        charge = 500;
                        target.velocity.Y = -20;
                        target.hp = -20;

                    }
                    break;
            }
            Applygravity();
            base.Update(_gt, _sl);
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
    }
}

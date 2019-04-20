using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites.Enemies
{
    public class Prowler:PhysObject
    {
        enum States
        {
            wander,
            follow
        };

        private Player target;
        private States currentstate;
        private int jumpcount,facing;
        private Vector2 pos;
        private float distance = 500, oldDistance, targetDistance;
        private bool right;


        public Prowler(ContentManager _con, Vector2 _pos, Player _target) : base(_con, _pos)
        {
            target = _target;
            pos = _pos;
            oldDistance = distance;
            right = true;
            facing = 1;
            jumpcount = 0;
            currentstate = States.wander;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/prowler_walk"), new Vector2(55), new Vector2(55), _pos, 6, Color.White);
            anim.maxDelay = 2f;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            switch (currentstate)
            {
                case States.wander:
                    {
                        anim.col = Color.Aqua;
                        pos += velocity;
                        if (distance <= 0)
                        {
                            right = true;
                            velocity.X = 1f;
                        }
                        else if (distance >= oldDistance)
                        {
                            right = false;
                            velocity.X = -1f;
                        }

                        if (right == true)
                        {
                            distance += 10;
                        }
                        else
                            distance -= 10;


                        if (distanceToTarget() < 400f && Math.Abs(target.anim.position.Y - anim.position.Y) < 40f)
                        {
                            facing = Math.Sign(target.anim.position.X - anim.position.X); currentstate = States.follow;
                        }
                        break;
                    }

                case States.follow:
                    targetDistance = target.anim.position.X - anim.position.X;
                    anim.col = Color.Red;
                    if (targetDistance < facing)
                        velocity.X = -2f;
                    else if (targetDistance >= facing)
                        velocity.X = 2f;
                    else if (targetDistance == 0)
                        velocity.X = 0f;

                    if(Math.Abs(target.anim.position.Y - anim.position.Y) > 30 && IsGrounded(_sl))
                    {
                        velocity.Y = -6f;
                    }


                    if (distanceToTarget() >= 400f || Math.Abs(target.anim.position.Y - anim.position.Y) > 70f)
                    {
                        velocity.X = 0f; currentstate = States.wander;
                    }
                    break;
            }

            if(velocity.X > 0)
            {
                anim.mirrored = SpriteEffects.FlipHorizontally;
            } else if (velocity.X < 0)
            {
                anim.mirrored = SpriteEffects.None;
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

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Scenes;
namespace ProjectMemoir.Sprites.Enemies
{
    public class Prowler:PhysObject
    {
        public enum States
        {
            wander,
            follow,
            stunned
        };

        private Player target;
        public States currentstate;
        public int facing,stuntime;
        private Vector2 pos;
        private float distance = 500, oldDistance, targetDistance;
        private bool right;
        private Animation stunFx;


        public Prowler(ContentManager _con, Vector2 _pos, Player _target, Scene _parentScene) : base(_con, _pos, _parentScene)
        {
            target = _target;
            pos = _pos;
            oldDistance = distance;
            right = true;
            facing = 1;
            currentstate = States.wander;
            stunFx = new Animation(_con.Load<Texture2D>("Vfx/vfx_stun"), new Vector2(27,15), new Vector2(55,30), _pos, 3, Color.White);
            stunFx.alpha = 0;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/prowler_walk"), new Vector2(55), new Vector2(55), _pos, 6, Color.White);
            anim.maxDelay = 2f;
            stuntime = 0;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            stunFx.position = anim.position + new Vector2(27/2,-10);
            stunFx.Update(_gt);
            switch (currentstate)
            {
                case States.wander:
                    {
                        anim.col = Color.White;
                        anim.maxDelay = 2f;
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


                        if (distanceToTarget() < 200f && 
                            canSeePlayer() && 
                            !target.invincible && 
                            Math.Sign(target.anim.position.X - anim.position.X) == facing )
                        {
                            facing = Math.Sign(target.anim.position.X - anim.position.X); currentstate = States.follow;
                        }
                        break;
                    }

                case States.follow:
                    {
                        anim.maxDelay = 1f;
                        targetDistance = target.anim.position.X - anim.position.X;
                        anim.col = Color.Red;
                        if (targetDistance < facing)
                            velocity.X = -2f;
                        else if (targetDistance >= facing)
                            velocity.X = 2f;
                        else if (targetDistance == 0)
                            velocity.X = 0f;

                        if (anim.desRect.Intersects(target.anim.desRect) && !target.invincible)
                        {
                            target.getHurt(Math.Sign(target.anim.position.X - anim.position.X) * 4, -8);
                        }

                        if (distanceToTarget() >= 400f || Math.Abs(target.anim.position.Y - anim.position.Y) > 70f || target.invincible)
                        {
                            velocity.X = 0f; currentstate = States.wander;
                        }

                        break;
                    }
                case States.stunned:
                    velocity = Vector2.Zero;
                    Applygravity();
                    stunFx.alpha = 1;
                    anim.frames = 0;
                    anim.currentframe = 0;
                    anim.col = Color.White;
                    if(stuntime > 0)
                    {
                        stuntime--;
                    } else
                    {
                        stunFx.alpha = 0;
                        currentstate = States.wander;
                        anim.frames = 6;
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
        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
            stunFx.Draw(_sb);
        }
    }
}

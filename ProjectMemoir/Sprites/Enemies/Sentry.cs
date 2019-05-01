using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Scenes;
namespace ProjectMemoir.Sprites.Enemies
{
    class Sentry:PhysObject
    {
        enum states
        {
            idle,
            alert,
            stunned
        };

        bool shot = false;
        states currentState;
        Player target;
        List<SentryProjectile> spl;
        Gamescene parentScene;
        public Sentry(ContentManager _con, Vector2 _pos,Gamescene _parentScene) : base(_con, _pos, _parentScene)
        {
            grav = 0;
            currentState = states.idle;
            target = _parentScene.player;
            con = _con;
            anim = new Animation(_con.Load<Texture2D>("enemySprites/sentry_idle"), new Vector2(71,45), new Vector2(71,45), _pos, 5, Color.White);
            anim.maxDelay = 1.5f;
            spl = new List < SentryProjectile >();
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            switch (currentState)
            {
                case states.idle:
                    anim.tex = con.Load<Texture2D>("enemySprites/sentry_idle");
                    anim.frames = 5;
                    if (distanceToTarget() < 200f && !target.invincible) { currentState = states.alert; anim.currentframe = 0; }
                    break;
                case states.alert:
                    
                    if (distanceToTarget() >= 200f || target.invincible) { currentState = states.idle; anim.currentframe = 0; }
                    anim.tex = con.Load<Texture2D>("enemySprites/sentry_shoot");
                    anim.frames = 5;

                    if (anim.currentframe == 4 && !shot)
                    {
                        shot = true;
                        spl.Add(new SentryProjectile(con, anim.position + new Vector2(20,17), new Vector2(target.anim.position.X - anim.position.X, target.anim.position.Y - anim.position.Y) * 0.01f, this.parentScene));
                    }
                    if(anim.currentframe == 0) { shot = false; }
                    break;
            }
           
            foreach(SentryProjectile _sp in spl)
            {
                _sp.Update(_gt, _sl);
            }
            for (int i = 0; i < spl.Count; i++)
            {
                if (!spl[i].isVisible)
                {
                    spl.RemoveAt(i);
                    i--;
                }
            }
            base.Update(_gt, _sl);
        }
        public override void Draw(SpriteBatch _sb)
        {
            foreach (SentryProjectile _sp in spl)
            {
                _sp.Draw(_sb);
            }
            base.Draw(_sb);
        }
        public float distanceToTarget()
        {
            return Math.Abs(target.anim.position.X - anim.position.X);
        }
    }
}

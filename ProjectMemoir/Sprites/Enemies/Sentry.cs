using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites.Enemies
{
    class Sentry:PhysObject
    {
        bool isAlert;
        Player target;
        float delay = 500;
        ContentManager con;
        List<SentryProjectile> spl;
        public Sentry(ContentManager _con, Vector2 _pos, Player _target) : base(_con, _pos)
        {
            grav = 0;
            isAlert = false;
            target = _target;
            con = _con;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos, 0, Color.Aqua);
            spl = new List < SentryProjectile >();
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (distanceToTarget() < 200f) { isAlert = true; } else { isAlert = false; }
            switch (isAlert)
            {
                case true:
                    if (delay > 0)
                    {
                        delay -= 10f;
                    } else
                    {
                        anim.col = Color.Red;
                        spl.Add(new SentryProjectile(con, anim.position, new Vector2(target.anim.position.X - anim.position.X, target.anim.position.Y - anim.position.Y) * 0.01f, target));
                        delay = 500;
                    }
                    break;
                case false:
                    delay = 500;
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

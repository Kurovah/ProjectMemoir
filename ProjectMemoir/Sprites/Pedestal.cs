using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Components;

namespace ProjectMemoir.Sprites
{
    public class Pedestal:Sprite
    {
        String type;
        PlayerStats ps;
        Player player;
        Animation obj;
        public Pedestal(ContentManager _con, Vector2 _pos, PlayerStats _ps, String _type, Player _target) : base(_con, _pos)
        {
            type = _type;
            player = _target;
            ps = _ps;
            obj = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos, 0, Color.White);
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32,40), new Vector2(32), _pos+new Vector2(0,24), 0, Color.White);
            
            anim.sourcePos = new Vector2(32);
            obj.sourcePos = new Vector2(32);

            if (ps.abilities[type])
            {
                obj.alpha = 0;
            }
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (player.anim.desRect.Intersects(anim.desRect))
            {
                if (!ps.abilities[type])
                {
                    ps.abilities[type] = true;
                    obj.alpha = 0;
                }
            }

            
            obj.Update(_gt);
            base.Update(_gt, _sl);
        }

        public override void Draw(SpriteBatch _sb)
        {
            obj.Draw(_sb);
            base.Draw(_sb);
        }
    }
}

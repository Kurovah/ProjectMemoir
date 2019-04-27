using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Components;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites
{
    public class GriefTree : Sprite
    {
        String type;
        PlayerStats ps;
        Player player;
        float initx;
        Gamescene gs;
        public GriefTree(ContentManager _con, Vector2 _pos, PlayerStats _ps, String _type, Player _target, Gamescene _gs) : base(_con, _pos)
        {
            type = _type;
            player = _target;
            ps = _ps;
            initx = _pos.Y;
            anim = new Animation(_con.Load<Texture2D>("griefTree"), new Vector2(160), new Vector2(160), _pos, 0, Color.White);
            anim.needsChange = false;

            gs = _gs;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            
            

            if (player.anim.desRect.Intersects(this.anim.desRect))
            {
                if (!ps.treesPurified[type])
                {
                    ps.treesPurified[type] = true;
                    gs.pu.active = true;
                    gs.pu.text = "A portion of your grief has been cleansed, your heart grows lighter";
                }
            }

            if (ps.treesPurified[type])
            {  
                anim.sourcePos.Y = 161;
                anim.position.Y = initx +1;
            }
            else
            {
                anim.sourcePos.Y = 0;
            }

            base.Update(_gt, _sl);
        }

        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
        }
    }
}

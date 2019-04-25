using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites;
using ProjectMemoir.Components;
namespace ProjectMemoir.Sprites
{
    public class Seal:Solid
    {


        PlayerStats ps;
        String type;
        public Seal(ContentManager _con, Vector2 _pos, Vector2 _spriteSize, String _type, PlayerStats _ps):base(_con, _pos,_spriteSize)
        {
            ps = _ps;
            type = _type;
            anim = new Animation(_con.Load<Texture2D>("seal"), new Vector2(32,64), new Vector2(32,64), _pos, 0, Color.White);
            anim.needsChange = false;
            canCollide = true;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {

            switch (type)
            {
                case "Red":
                    anim.sourcePos.X = 0;
                    break;
                case "Blue":
                    anim.sourcePos.X = 64;
                    break;
                case "Green":
                    anim.sourcePos.X = 32;
                    break;
            }
            if (ps.abilities[type])
            {
                anim.sourcePos.Y = 64;
                canCollide = false;
            } else
            {
                anim.sourcePos.Y = 0;
                canCollide = true;
            }

            base.Update(_gt, _sl);
        }

    }
}

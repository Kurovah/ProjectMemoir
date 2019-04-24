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
        Vector2 position;
        PlayerStats ps;
        Animation obj;
        public Pedestal(ContentManager _con, Vector2 _pos, PlayerStats _ps, String _type) : base(_con, _pos)
        {
            type = _type;
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos * 32, 0, Color.White);
            obj = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32), new Vector2(32), _pos * 32, 0, Color.White);
        }
    }
}

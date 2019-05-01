using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites
{
    public class Solid:Sprite
    {
        public Solid(ContentManager _con,Vector2 _pos ,Vector2 _spriteSize, Scene _parentScene):base(_con, _pos, _parentScene)
        {
            canCollide = true;
            anim = new Animation(_con.Load<Texture2D>("forP"), _spriteSize, new Vector2(32), _pos, 0, Color.Black*0f);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir.Sprites
{
    public class Solid:Sprite
    {
        public Solid(ContentManager _con,Vector2 _pos ,Vector2 _spriteSize):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), _spriteSize, new Vector2(32), _pos, 0, Color.Black);
        }
    }
}
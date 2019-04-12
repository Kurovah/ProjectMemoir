using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Components
{

    public class Autotiler{
        int tilesize = 32;
        Vector2 roomSize;
        Rectangle checkRect;
        List<Animation> tiles;
        public bool active = true;
        Texture2D tex;
        Vector2 tSize;
        public Autotiler(ContentManager _con, String _tileset,Vector2 _roomsize)
        {
            roomSize = _roomsize;
            tiles = new List<Animation>();
            checkRect = new Rectangle(0, 0, tilesize, tilesize);
            tex = _con.Load<Texture2D>(_tileset);
            tSize = new Vector2(tilesize);
        }

        public void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (active)
            {
                for (int i = 0; i <= roomSize.Y; i += tilesize)
                {
                    for (int j = 0; j <= roomSize.X; j += tilesize)
                    {
                        checkRect.X = j;
                        checkRect.Y = i;
                        //check to see if a solid is being touched
                        foreach (Sprite _s in _sl)
                        {
                            if (_s.GetType() != typeof(Solid)) { continue; }
                            if (checkRect.Intersects(_s.anim.desRect))
                            {
                                tiles.Add(new Animation(tex, tSize, tSize, new Vector2(j, i), 0, Color.White));
                            }
                        }
                    }
                }
            }
            //when finished show that
            active = false;
        }
        public void  Draw(SpriteBatch _sb)
        {
            foreach(Animation _a in tiles)
            {
                _a.Draw(_sb);
            }
        }
    }
}


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
        Vector2 tSize,texCoord;
        SpriteFont txt;//for testing
        public Autotiler(ContentManager _con, String _tileset,Vector2 _roomsize)
        {
            roomSize = _roomsize;
            tiles = new List<Animation>();
            checkRect = new Rectangle(0, 0, tilesize, tilesize);
            tex = _con.Load<Texture2D>(_tileset);
            tSize = new Vector2(tilesize);
            txt = _con.Load<SpriteFont>("Font");
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
                            int bw = 0;
                            if (_s.GetType() != typeof(Solid)) { continue; }
                            if (checkRect.Intersects(_s.anim.desRect) )
                            {
                                //check again for bw calc to get the new tile
                                #region checksides for bitwise collisions
                                foreach (Sprite _s2 in _sl)
                                {
                                    if (_s2.GetType() != typeof(Solid)) { continue; }
                                    #region side checks
                                    //left side
                                    Rectangle Checkrect2 = new Rectangle(checkRect.X - 32, checkRect.Y, tilesize, tilesize);
                                    if (Checkrect2.Intersects(_s2.anim.desRect))
                                    {
                                        bw += 2;
                                    }
                                    //right side
                                    Checkrect2 = new Rectangle(checkRect.X + 32, checkRect.Y, tilesize, tilesize);
                                    if (Checkrect2.Intersects(_s2.anim.desRect))
                                    {
                                        bw += 4;
                                    }
                                    //up side
                                    Checkrect2 = new Rectangle(checkRect.X, checkRect.Y - 32, tilesize, tilesize);
                                    if (Checkrect2.Intersects(_s2.anim.desRect))
                                    {
                                        bw += 1;
                                    }
                                    //down side
                                    Checkrect2 = new Rectangle(checkRect.X, checkRect.Y + 32, tilesize, tilesize);
                                    if (Checkrect2.Intersects(_s2.anim.desRect))
                                    {
                                        bw += 8;
                                    }
                                    #endregion
                                }
                                #endregion
                                #region create tex coord depending on bw calculation
                                switch (bw)
                                {
                                    case 0:
                                        texCoord = new Vector2(96);
                                        break;
                                    case 1:
                                        texCoord = new Vector2(96,64);
                                        break;
                                    case 2:
                                        texCoord = new Vector2(64,96);
                                        break;
                                    case 3:
                                        texCoord = new Vector2(64);
                                        break;
                                    case 4:
                                        texCoord = new Vector2(0,96);
                                        break;
                                    case 5:
                                        texCoord = new Vector2(0,64);
                                        break;
                                    case 6:
                                        texCoord = new Vector2(32,96);
                                        break;
                                    case 7:
                                        texCoord = new Vector2(32,64);
                                        break;
                                    case 8:
                                        texCoord = new Vector2(96,0);
                                        break;
                                    case 9:
                                        texCoord = new Vector2(96,32);
                                        break;
                                    case 10:
                                        texCoord = new Vector2(64,0);
                                        break;
                                    case 11:
                                        texCoord = new Vector2(64,32);
                                        break;
                                    case 12:
                                        texCoord = new Vector2(0);
                                        break;
                                    case 13:
                                        texCoord = new Vector2(0,32);
                                        break;
                                    case 14:
                                        texCoord = new Vector2(32,0);
                                        break;
                                    case 15:
                                        texCoord = new Vector2(32);
                                        break;
                                }
                                #endregion
                                Animation a = new Animation(tex, tSize, tSize, new Vector2(j, i), 0, Color.White);
                                a.t = bw;
                                a.sourcePos = texCoord;
                                a.needsChange = false;
                                tiles.Add(a);
                            }
                        }
                    }
                }
            }
            //update the tiles so that they are the correct sprites
            foreach (Animation _a in tiles)
            {
                _a.Update(_gt);
            }
            //when finished show that
            active = false;
        }
        public void  Draw(SpriteBatch _sb)
        {
            foreach(Animation _a in tiles)
            {
                _a.Draw(_sb);
                _sb.DrawString(txt, "" + _a.t, _a.position, Color.White);
            }
        }
    }
}


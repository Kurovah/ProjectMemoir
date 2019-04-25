using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Components
{
    public class HUD
    {
        PlayerStats target;
        ContentManager con;
        Texture2D tex;
        float scale;
        public HUD(PlayerStats _playerchr, ContentManager _con)
        {
            target = _playerchr;
            con = _con;
            tex = _con.Load<Texture2D>("HUD");
            scale = 2f;
        }


        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(tex, 
                        new Rectangle(5, 5, (int)(96*scale),(int)(32 *scale)), 
                        new Rectangle(0, 0, 96, 32),
                        Color.White);

           //drawing the hearts
            for(int i=0; i <  target.hp; i++)
            {
                _sb.Draw(tex,
                    new Rectangle(5 + (int)(32*i*scale), 5, (int)(32 * scale), (int)(32 * scale)), 
                    new Rectangle(97, 0, 32, 32), 
                    Color.White);
            }
        }
    }
}

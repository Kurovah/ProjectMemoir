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
        Player target;
        Animation frontBar, backBar;
        float width = 100;
        public HUD(Player _playerchr, ContentManager _con)
        {
            target = _playerchr;
            frontBar = new Animation(_con.Load<Texture2D>("forP"), new Vector2(width, 20), new Vector2(32), new Vector2(5), 0, Color.Red);
            backBar = new Animation(_con.Load<Texture2D>("forP"), new Vector2(width, 20), new Vector2(32), new Vector2(5), 0, Color.Black);
        }

        public void Update(GameTime _gt)
        {
            frontBar.spriteSize.X = width * target.hp / target.maxHp;
            frontBar.Update(_gt);
        }

        public void Draw(SpriteBatch _sb)
        {
            backBar.Draw(_sb);
            frontBar.Draw(_sb);
        }
    }
}

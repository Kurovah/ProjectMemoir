using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Components
{
    public abstract class Menu
    {
        protected int pos;
        protected List<string> options;
        protected SpriteFont txt;
        protected KeyboardState currentK, lastK;
        protected Vector2 startPos;
        Animation pointer;
        public Menu(ContentManager _con, List<string> _options, Vector2 _startpos)
        {
            //set the position of the menu to zero (top option)
            pos = 0;
            options = _options;
            startPos = _startpos;
            pointer = new Animation(_con.Load<Texture2D>("forP"), new Vector2(10), new Vector2(32), _startpos, 0, Color.Red);
        }
        public void Update(GameTime _gt)
        {
            currentK = Keyboard.GetState();
            //going up
            if(currentK.IsKeyDown(Keys.S) && !lastK.IsKeyDown(Keys.S))
            {
                if(pos < options.Count) { pos++; } else { pos = 0; }
            }
            //going up
            if (currentK.IsKeyDown(Keys.S) && !lastK.IsKeyDown(Keys.S))
            {
                if (pos >= 0) { pos--; } else { pos = options.Count; }
            }
            lastK = currentK;
            //select option
            if (currentK.IsKeyDown(Keys.J) )
            {
                Selectoption(pos);
            }
            pointer.position.Y = startPos.Y + pos * 32;
            pointer.Update(_gt);
        }

        public abstract void Selectoption(int OP);

        public virtual void Draw(SpriteBatch _sb)
        {
            int P = 0;
            foreach (String _s in options)
            {
                _sb.DrawString(txt, _s, startPos + new Vector2(0, P*32), Color.White);
                P++;
            }
            pointer.Draw(_sb);
        }
    }
}

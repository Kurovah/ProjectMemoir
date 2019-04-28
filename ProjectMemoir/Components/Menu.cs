using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Sprites;
using ProjectMemoir.Scenes;
using ProjectMemoir.Components;

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
        protected Game1 g;
        protected Scene scene;
        public bool active;
        public Menu(ContentManager _con, List<string> _options, Vector2 _startpos, Scene _scene)
        {
            //set the position of the menu to zero (top option)
            pos = 0;
            options = _options;
            startPos = _startpos;
            txt = _con.Load<SpriteFont>("Font");
            scene = _scene;
            pointer = new Animation(_con.Load<Texture2D>("forP"), new Vector2(20), new Vector2(32), _startpos, 0, Color.Red);
            active = true;
        }
        public virtual void Update(GameTime _gt)
        {
            if (active)
            {
                currentK = Keyboard.GetState();
                //going up
                if (currentK.IsKeyDown(Keys.S) && !lastK.IsKeyDown(Keys.S))
                {
                    if (pos < options.Count - 1) { pos++; } else { pos = 0; }
                }
                //going up
                if (currentK.IsKeyDown(Keys.W) && !lastK.IsKeyDown(Keys.W))
                {
                    if (pos > 0) { pos--; } else { pos = options.Count - 1; }
                }

                //select option
                if (currentK.IsKeyDown(Keys.J))
                {
                    Selectoption(pos);
                }

                pointer.position.Y = startPos.Y + pos * 32;
                pointer.Update(_gt);
                lastK = currentK;
            }
        }

        public abstract void Selectoption(int OP);

        public virtual void Draw(SpriteBatch _sb)
        {
            
            pointer.Draw(_sb);
        }
    }
}

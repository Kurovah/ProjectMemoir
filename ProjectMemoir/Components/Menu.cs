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
        protected Vector2 startPos, offset;
        public Animation pointer;
        protected Game1 g;
        protected Scene scene;
        public bool active;
        public InputManager input;
        public Menu(ContentManager _con, List<string> _options, Vector2 _startpos, Scene _scene)
        {
            //set the position of the menu to zero (top option)
            pos = 0;
            options = _options;
            startPos = _startpos;
            txt = _con.Load<SpriteFont>("Font");
            scene = _scene;
            pointer = new Animation(_con.Load<Texture2D>("menu_pointer"), new Vector2(150,20), new Vector2(100,20), _startpos, 0, Color.White);
            active = true;
            input = _scene.game.input;
        }
        public virtual void Update(GameTime _gt)
        {
            if (active)
            {
                //going up
                if (input.DownInput)
                {
                    if (pos < options.Count - 1) { pos++; scene.soundManager.mainMenuSelect.Play(); } else { pos = 0; scene.soundManager.mainMenuSelect.Play(); }
                }
                //going up
                if (input.UpInput)
                {
                    if (pos > 0) { pos--; scene.soundManager.mainMenuSelect.Play(); } else { pos = options.Count - 1; scene.soundManager.mainMenuSelect.Play(); }
                }

                //select option
                if (input.JumpInput)
                {
                    Selectoption(pos);
                }
                pointer.Update(_gt);
            }
        }

        public abstract void Selectoption(int OP);

        public virtual void Draw(SpriteBatch _sb)
        {
            
            pointer.Draw(_sb);
        }
    }
}

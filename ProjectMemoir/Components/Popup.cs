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
    public class PopUp
    {
        protected int pos;
        protected List<string> options;
        protected SpriteFont txt;
        protected KeyboardState currentK, lastK;
        protected Vector2 startPos;
        Animation backboard;
        protected Game1 g;
        protected Scene scene;
        public bool active;
        public String text;
        public PopUp(ContentManager _con, Scene _scene, String _text)
        {
            //set the position of the menu to zero (top option)
            pos = 0;
            txt = _con.Load<SpriteFont>("Font");
            text = _text;
            backboard = new Animation(_con.Load<Texture2D>("forP"),new Vector2(1088,50),new Vector2(31),new Vector2(96,720/2 - 25),0,Color.White);
            scene = _scene;
            active = false;
        }
        public virtual void Update(GameTime _gt)
        {
            currentK = Keyboard.GetState();
            scene.pause = active;
            if (active)
            {
                if (currentK.IsKeyDown(Keys.J) && !lastK.IsKeyDown(Keys.J)) { active = false; }
            }
            lastK = currentK;
        }

        public virtual void Draw(SpriteBatch _sb)
        {
            if (active) {
                backboard.Draw(_sb);
                _sb.DrawString(txt, text, new Vector2( (1280 / 2) - txt.MeasureString(text).X/2, (720 / 2 - 25)+10), Color.White);
            }
            
        }
    }
}

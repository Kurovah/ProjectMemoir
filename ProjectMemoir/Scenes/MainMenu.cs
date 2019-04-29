using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Sprites;
using ProjectMemoir.Sprites.Enemies;
using ProjectMemoir.Components;

namespace ProjectMemoir.Scenes
{
    public class MainMenu:Scene
    {

        public MainMenuMenu gOMenu;
        public MainMenu(Game1 _game, ContentManager _con):base(_game, _con)
        {
            background = _con.Load<Texture2D>("backgrounds/title_screen");
            gOMenu = new MainMenuMenu(_con,new List<string> (){"Start","Quit"}, new Vector2(5), this);
        }

        public override void Load()
        {
        }
        public override void Update(GameTime _gt)
        {

            gOMenu.Update(_gt);
        }
        public override void Draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin();
            _sb.Draw(background, Vector2.Zero, Color.White);
            gOMenu.Draw(_sb);
            _sb.End();
        }


    }
}

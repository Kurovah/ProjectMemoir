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
    class Endscene:Scene
    {
        public Endscene(Game1 _game, ContentManager _con) : base(_game, _con)
        {
            background = _con.Load<Texture2D>("backgrounds/endofGame_bk");
        }

        public override void Load()
        {
        }
        public override void Update(GameTime _gt)
        {
           
        }
        public override void Draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin();
            _sb.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            _sb.End();
        }
    }
}

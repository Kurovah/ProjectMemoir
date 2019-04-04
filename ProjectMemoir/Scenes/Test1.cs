using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ProjectMemoir.Sprites;
using ProjectMemoir.Sprites.Enemies;
using ProjectMemoir.Components;

namespace ProjectMemoir.Scenes
{
    public class Test1:Scene
    {
        private Player player;
        private Charger sen;
        private Cam cam;
        private HUD hud;
        

        public Test1(Game1 _game, ContentManager _con):base(_game, _con)
        {

        }

        public override void Load()
        {
            spriteList = new List<Sprite>();
            spriteList.Add(player = new Player(this.con, new Vector2(40)));
            //spriteList.Add(den = new DummyEn(this.con, new Vector2(400, 200), player));
            spriteList.Add(sen = new Charger(this.con, new Vector2(400, 400), player));
            //solids to collide with
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(3, 720)));
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(1280, 3)));
            spriteList.Add(new Solid(this.con, new Vector2(0, 720), new Vector2(1280, 3)));
            spriteList.Add(new Solid(this.con, new Vector2(1280, 0), new Vector2(3, 720)));
            cam = new Cam(player, new Vector2(0, 620), new Vector2(0, 360));
            hud = new HUD(player, this.con);
        }

        public override void Update(GameTime _gt)
        {
            
            foreach (Sprite _s in spriteList)
            {
                _s.Update(_gt, spriteList);
            }
            checkToRemoveSprite();
            cam.Update(_gt);
            hud.Update(_gt);
        }

        public override void Draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin(transformMatrix: cam.trans);
            foreach (Sprite _s in spriteList)
            {
                _s.Draw(_sb);
            }
            _sb.End();

            //so the HUD isn't moved by the trans matrix
            _sb.Begin();
                hud.Draw(_sb);
            _sb.End();
        }
    }
}

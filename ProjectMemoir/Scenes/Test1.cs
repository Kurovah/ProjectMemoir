﻿using System;
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
    public class Test1:Scene
    {
        private Player player;
       // private Charger sen;
       // private Sentry en;
        private Prowler pro;
        private Cam cam;
        private HUD hud;
        private Vector2 newPos;
        private bool pause;
        private KeyboardState currentK, lastK;

        public Test1(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con)
        {
            newPos = _playerpos;
            pause = false;
        }

        public override void Load()
        {
            spriteList = new List<Sprite>();
            spriteList.Add(player = new Player(this.con, newPos));
            //spriteList.Add(den = new DummyEn(this.con, new Vector2(400, 200), player));
            //spriteList.Add(sen = new Charger(this.con, new Vector2(400, 400), player));
            //spriteList.Add(en = new Sentry(this.con, new Vector2(400, 500), player));
            spriteList.Add(pro = new Prowler(this.con, new Vector2(600, 600), player));
            //solids to collide with
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(32, 720)));
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(1280, 32)));
            spriteList.Add(new Solid(this.con, new Vector2(0, 720), new Vector2(1280, 32)));
            spriteList.Add(new Solid(this.con, new Vector2(1280, 0), new Vector2(32, 720)));
            spriteList.Add(new SceneChanger(this.con, new Vector2(1000,630),player,this.game, "s", new Vector2(32,630)));
            cam = new Cam(player, new Vector2(0, 620), new Vector2(0, 360));
            hud = new HUD(player, this.con);
        }

        public override void Update(GameTime _gt)
        {
            currentK = Keyboard.GetState();
            if (currentK.IsKeyDown(Keys.P) && !lastK.IsKeyDown(Keys.P)) { pause = !pause;}//if P is "pressed" pause the game 
            if (!pause)
            {
                foreach (Sprite _s in spriteList)
                {
                    _s.Update(_gt, spriteList);
                }
                checkToRemoveSprite();
                cam.Update(_gt);
                hud.Update(_gt);
            }

            lastK = currentK;
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
                if (pause) {
                //the black background
                _sb.Draw(con.Load<Texture2D>("forP"), new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 32, 32), Color.Black*0.75f); }
            _sb.End();

        }
    }
}

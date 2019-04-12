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
    public class Test1:Scene
    {
        private Player player;
       // private Charger sen;
       // private Sentry en;
        private Prowler pro;
        private Cam cam;
        private HUD hud;
        private Vector2 newPos;
        private KeyboardState currentK, lastK;
        PauseMenu pmenu;
        Autotiler at;

        public Test1(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con)
        {
            newPos = _playerpos;
            pause = false;
            pmenu = new PauseMenu(_con, new List<string> { "This", "is", "a", "Menu" }, new Vector2(0, 0), _game, this);
            roomSize = new Vector2(0);
        }

        public override void Load()
        {
            spriteList = new List<Sprite>();
            spriteList.Add(player = new Player(this.con, newPos));
            //spriteList.Add(pro = new Prowler(this.con, new Vector2(600, 600), player));
            //solids to collide with
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(32, 768)));
            spriteList.Add(new Solid(this.con, new Vector2(0), new Vector2(1312, 32)));
            spriteList.Add(new Solid(this.con, new Vector2(0, 768), new Vector2(1312, 128)));
            spriteList.Add(new Solid(this.con, new Vector2(1280, 0), new Vector2(32, 768)));
            spriteList.Add(new SceneChanger(this.con, new Vector2(1000,630),player,this.game, "s", new Vector2(32,630)));
            
            hud = new HUD(player, this.con);

            //checking the size of the room
            foreach(Sprite _s in spriteList)
            {
                if(roomSize.X < _s.anim.position.X + _s.anim.spriteSize.X) { roomSize.X = _s.anim.position.X + _s.anim.spriteSize.X-16; }
                if (roomSize.Y < _s.anim.position.Y + _s.anim.spriteSize.Y) { roomSize.Y = _s.anim.position.Y + _s.anim.spriteSize.Y-16; }
            }
            //put anything that's dependant on the roomsize here
            cam = new Cam(player, new Vector2(game.GraphicsDevice.Viewport.Width/2-16,roomSize.X- game.GraphicsDevice.Viewport.Width / 2), 
                                  new Vector2(game.GraphicsDevice.Viewport.Height/2-16, roomSize.Y - game.GraphicsDevice.Viewport.Height / 2));
            at = new Autotiler(con, "VillageTiles", roomSize);
            
        }

        public override void Update(GameTime _gt)
        {
            //nothing moves while the level is being tiled
            while (at.active)
            {
                at.Update(_gt, spriteList);
            }
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
            } else
            {
                pmenu.Update(_gt);
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
            at.Draw(_sb);
            _sb.End();

            //so the HUD isn't moved by the trans matrix
            _sb.Begin();
                hud.Draw(_sb);
                if (pause) {
                //the black background
                _sb.Draw(con.Load<Texture2D>("forP"), new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 32, 32), Color.Black*0.75f);
                //draw Pause menu
                pmenu.Draw(_sb);
                }
            _sb.End();

        }
    }
}

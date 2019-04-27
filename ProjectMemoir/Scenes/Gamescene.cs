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
    public class Gamescene : Scene
    {
        //init shared variables
        protected Player player;
        protected HUD hud;
        protected Vector2 newPos;
        protected KeyboardState currentK, lastK;
        protected PauseMenu pmenu;
        protected Autotiler at;
        protected PlayerStats ps;
        
        Game1 g;
        public Gamescene(Game1 _game, ContentManager _con, Vector2 _playerpos) :base(_game, _con){
            newPos = _playerpos;
            pause = false;
            pmenu = new PauseMenu(_con, new List<string> { "This", "is", "a", "Menu" }, new Vector2(0, 0), _game, this);
            roomSize = new Vector2(0);
            g = _game;
        }

        public override void Load()
        {
            if (id != null)
            {
                g.ps.mapPeices[id] = true;
            }

           
            hud = new HUD(this.game.ps, this.con);

            //checking the size of the room
            foreach (Sprite _s in spriteList)
            {
                if (roomSize.X < _s.anim.position.X + _s.anim.spriteSize.X) { roomSize.X = _s.anim.position.X + _s.anim.spriteSize.X; }
                if (roomSize.Y < _s.anim.position.Y + _s.anim.spriteSize.Y) { roomSize.Y = _s.anim.position.Y + _s.anim.spriteSize.Y; }
            }
            
            //put anything that's dependant on the roomsize here
            
            at = new Autotiler(con, "tilesets/VillageTiles", roomSize);
            spriteList.Add(player = new Player(this.con, newPos, this.game.ps));
            cam = new Cam(player, roomSize, new Vector2(1280, 720));
        }

        #region functions to create new objects in the game world
        protected void newSolid(int _x, int _y, int _width, int _height)
        {
            spriteList.Add(new Solid(this.con, new Vector2(_x*32, _y*32), new Vector2(_width * 32, _height * 32)));
        }
        protected void newSentry(int _x, int _y)
        {
            spriteList.Add(new Sentry(this.con, new Vector2(_x*32, _y*32), player));
        }
        protected void newCharger(int _x, int _y)
        {
            spriteList.Add(new Charger(this.con, new Vector2(_x * 32, _y * 32), player));
        }
        protected void newProwler(int _x, int _y)
        {
            spriteList.Add(new Prowler(this.con, new Vector2(_x * 32, _y * 32), player));
        }
        protected void newSceneChanger(int _x, int _y, int _width, int _height,string _scene, Vector2 _newPos)
        {
            spriteList.Add(new SceneChanger(this.con, new Vector2(_x*32, _y*32), new Vector2(_width * 32, _height * 32), player, this.game, _scene,_newPos));
        }
        protected void newPedestal(int _x, int _y, String _type)
        {
            spriteList.Add(new Pedestal(this.con, new Vector2(_x * 32, _y * 32), game.ps, _type, player));
        }
        protected void newSeal(int _x, int _y, String _type)
        {
            spriteList.Add(new Seal(this.con, new Vector2(_x * 32, _y * 32),new Vector2(32,64), _type, game.ps));
        }

        protected void newBreakableBlock(int _x, int _y)
        {
            spriteList.Add(new BreakableBlock(this.con, new Vector2(_x * 32, _y * 32), new Vector2(32)));
        }
        #endregion

        public override void Update(GameTime _gt)
        {
            //nothing moves while the level is being tiled
            if (at.active)
            {
                at.Update(_gt, spriteList);
            }
            else
            {
                pmenu.Update(_gt);
                if (!pause)
                {
                    foreach (Sprite _s in spriteList)
                    {
                        _s.Update(_gt, spriteList);
                    }
                    checkToRemoveSprite();
                    cam.Update(_gt);
                }
            }
        }
        public override void Draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.trans);
            foreach (Sprite _s in spriteList)
            {
                _s.Draw(_sb);
            }
            at.Draw(_sb);
            _sb.End();

            //so the HUD isn't moved by the trans matrix
            _sb.Begin();
            hud.Draw(_sb);
            if (pause)
            {
                //the black background
                _sb.Draw(con.Load<Texture2D>("forP"), new Rectangle(0, 0, 1280, 720), new Rectangle(0, 0, 32, 32), Color.Black * 0.75f);
                //draw Pause menu
                pmenu.Draw(_sb);
            }
            _sb.End();

        }
    }
}

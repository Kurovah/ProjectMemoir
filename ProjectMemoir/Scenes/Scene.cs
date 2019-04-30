using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ProjectMemoir.Sprites;
using ProjectMemoir.Components;

namespace ProjectMemoir.Scenes
{
    public abstract class Scene
    {
        public Game1 game;
        protected ContentManager con;
        public Cam cam;
        public List<Sprite> spriteList,spriteDraw;
        public bool pause;
        public Vector2 roomSize;
        public String id;
        public Texture2D background;
        protected Scene(Game1 _game, ContentManager _con)
        {
            con = _con;
            game = _game;
            spriteList = spriteDraw = new List<Sprite>();
        }
        protected void checkToRemoveSprite()
        {
            for (int i = 0; i < spriteDraw.Count; i++)
            {
                if (!spriteDraw[i].isVisible) { spriteDraw.RemoveAt(i); i--; }
            }
        }
        protected void LoadtoList()
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                spriteDraw.Add(spriteList[i]);
                spriteList.RemoveAt(i);
                i--;
            }
        }
        public abstract void Load();
        public abstract void Update(GameTime _gt);
        public abstract void Draw(SpriteBatch _sb, GameTime _gt);
        
    }
}

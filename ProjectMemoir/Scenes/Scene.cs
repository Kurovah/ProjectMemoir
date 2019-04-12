using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Scenes
{
    public abstract class Scene
    {
        protected Game1 game;
        protected ContentManager con;
        public List<Sprite> spriteList;
        public bool pause;
        public Vector2 roomSize;
        protected Scene(Game1 _game, ContentManager _con)
        {
            con = _con;
            game = _game;
        }
        protected void checkToRemoveSprite()
        {
            for(int i = 0; i < spriteList.Count; i++)
            {
                if (!spriteList[i].isVisible) {spriteList.RemoveAt(i); i--; }
            }
        }
        public abstract void Load();
        public abstract void Update(GameTime _gt);
        public abstract void Draw(SpriteBatch _sb, GameTime _gt);
        
    }
}

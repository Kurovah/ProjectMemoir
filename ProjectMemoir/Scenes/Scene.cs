using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
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
        public ContentManager con;
        public Cam cam;
        public List<Sprite> spriteList,spriteDraw;
        public bool pause;
        public Vector2 roomSize;
        public String id;
        public Texture2D background;
        public SoundManger soundManager;
        public List<VFX> vfxQ, vfxList;
        protected Scene(Game1 _game, ContentManager _con)
        {
            con = _con;
            soundManager = new SoundManger(con);
            game = _game;
            spriteList = spriteDraw = new List<Sprite>();
            vfxQ = vfxList = new List<VFX>();
        }
        protected void checkToRemoveSprite()
        {
            for (int i = 0; i < spriteDraw.Count; i++)
            {
                if (!spriteDraw[i].isVisible) { spriteDraw.RemoveAt(i); i--; }
            }
        }
        protected void checkToRemoveVFX()
        {
            for (int i = 0; i < vfxList.Count; i++)
            {
                if (!vfxList[i].isVisible) { vfxList.RemoveAt(i); i--; }
            }
        }
        protected void LoadtoList()
        {
            if(vfxQ.Count > 0)
            {
                for (int i = 0; i < vfxQ.Count; i++)
                {
                    vfxList.Add(vfxQ[i]);
                    vfxQ.RemoveAt(i);
                }
            }
        }
        public abstract void Load();
        public abstract void Update(GameTime _gt);
        public virtual void PostUpdate(GameTime _gt){
            LoadtoList();
        }
        public abstract void Draw(SpriteBatch _sb, GameTime _gt);
        
    }
}

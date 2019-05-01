using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites
{
    public class VFX:Sprite
    { 
        public VFX(ContentManager _con, Vector2 _pos, Scene _parentScene,String _texName, Vector2 _size, int _frameCount):base(_con, _pos, _parentScene)
        {
            anim = new Animation(_con.Load<Texture2D>(_texName),_size,_size,_pos,_frameCount,Color.White);
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            
            base.Update(_gt, _sl);
            if (anim.isFinished())
            {
                isVisible = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Components
{
    public class PauseMenu:Menu
    {
        public PauseMenu(ContentManager _con, List<string> _options, Vector2 _pos, Game1 _g, Scene _scene) :base(_con, _options,_pos,_g, _scene)
        {
           
        }
        public override void Selectoption(int OP)
        {
            if(OP == 0)
            {
                scene.pause = false;
            }
        }
    }
}

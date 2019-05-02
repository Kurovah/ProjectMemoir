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
    public class A07:Gamescene
    {


        public A07(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a7";
        }

        public override void Load()
        {
            

            //solids to collide with
            newSolid(0, 0, 1, 19);
            newSolid(1, 0, 19, 1);
            newSolid(1, 11, 10, 1);
            newSolid(19, 1, 1, 18);
            newSolid(0, 21, 20, 1);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 1, 33, "A2", new Vector2(18, 9));
            newSceneChanger(0, -1, 1, 12, "A18", new Vector2(1, 9));
            newSceneChanger(21, -1, 0, 33, "A8", new Vector2(1, 9));
        }

        
    }
}

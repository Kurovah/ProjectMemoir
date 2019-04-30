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
    public class A03:Gamescene
    {


        public A03(Game1 _game, ContentManager _con,Vector2 _playerpos) :base(_game, _con, _playerpos)
        {
            id = "a3";
        }

        public override void Load()
        {


            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 9, 1);
            newSolid(7, 9, 2, 2);
            newSolid(11, 9, 2, 2);
            newBreakableBlock(9, 9);
            newBreakableBlock(10, 9);
            newSolid(11, 11, 9, 1);
            newSolid(19, 1, 1, 8);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 0, 12, "A2", new Vector2(1, 9));
            newSceneChanger(-1, -1, 0, 12, "A4", new Vector2(18, 9));
            newSceneChanger(8, 12, 2, 1, "A24", new Vector2(2, 9));
        }

        
    }
}

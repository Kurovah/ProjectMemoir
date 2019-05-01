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
    public class A05:Gamescene
    {


        public A05(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a5";
        }

        public override void Load()
        {


            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 1, 1, 8);

            newSolid(8, 4, 3, 1);
            newSolid(5, 6, 1, 1);
            newSolid(13, 6, 1, 1);
            newSolid(1, 8, 2, 1);
            newSolid(17, 8, 2, 1);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 0, 12, "A4", new Vector2(1, 9));
            newSceneChanger(0, -1, 0, 12, "A6", new Vector2(18, 9));
            newPedestal(9, 2, "Down");
        }

        
    }
}

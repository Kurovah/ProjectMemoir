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
    public class A26:Gamescene
    {


        public A26(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a26";
        }

        public override void Load()
        {
            

            //solids to collide with
            newSolid(0,0,1,11);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            newSolid(1, 7, 4, 1);
            newSolid(15, 7, 4, 1);
            newSolid(7, 4, 6, 1);
            newSolid(7, 10, 6, 1);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 1, 12, "A24", new Vector2(2, 9));
            newSceneChanger(0, -1, 1, 12, "A27", new Vector2(2, 9));
        }

        
    }
}

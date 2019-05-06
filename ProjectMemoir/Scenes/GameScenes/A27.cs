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
    public class A27:Gamescene
    {


        public A27(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a27";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0 ,0, 1, 23);
            newSolid(1, 0, 34, 1);           
            newSolid(39, 0, 1, 24);

            newSolid(0, 23, 5, 1);
            newSolid(13, 23, 3, 1);
            newSolid(24, 23, 3, 1);
            newSolid(35, 23, 4, 1);

            newSolid(37, 4, 2, 1);
            newSolid(35, 7, 2, 1);
            newSolid(37, 10, 2, 1);
            newSolid(35, 13, 2, 1);
            newSolid(37, 16, 2, 1);
            newSolid(35, 19, 2, 1);
            newSolid(37, 22, 2, 1);

            newSolid(34, 1, 1, 18);
            newSolid(5, 19, 30, 1);
            newSolid(5, 4, 1, 15);
            newSolid(6, 4, 24, 1);
            newSolid(30, 4, 1, 13);
            newSolid(31, 8, 1, 1);
            newSolid(10, 16, 20, 1);

            newSolid(1, 14, 1, 1);
            newSolid(4, 6, 1, 1);

            newSolid(25, 13, 5, 3);
            newSolid(20, 14, 5, 2);
            newSolid(15, 15, 5, 1);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 40, 1, "A26", new Vector2(2, 2));
            newSceneChanger(0, 24, 40, 1, "A27", new Vector2(37, 19));
            newEndDoor(26, 9);

        }

        
    }
}

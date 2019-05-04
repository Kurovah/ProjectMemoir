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
    public class A13:Gamescene
    {


        public A13(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a13";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(0, 11, 1, 6);
            newSolid(1, 0, 40, 1);
            newSolid(0, 17, 35, 1);
            newSolid(40, 1, 1, 8);
            newSolid(35, 11, 6, 7);

            newSolid(38, 8, 2, 1);
            newSolid(8, 6, 27, 1);
            newSolid(1, 1, 5, 8);
            newSolid(5, 9, 27, 1);

            newSolid(35, 5, 1, 6);
            newSolid(21, 5, 2, 1);
            newSolid(8, 5, 1, 1);

            newSolid(1, 14, 2, 3);
            newSolid(10, 14, 3, 3);
            newSolid(21, 14, 3, 3);
            newSolid(32, 14, 3, 3);


            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(41, -1, 1, 20, "A12", new Vector2(1, 9));
            newSceneChanger(-1, -1, 1, 20, "A14", new Vector2(18, 9));
            newCharger(14, 5);
            newCharger(27, 5);
            newProwler(7, 16);
            newProwler(16, 16);
            newProwler(28, 16);
        }

        
    }
}

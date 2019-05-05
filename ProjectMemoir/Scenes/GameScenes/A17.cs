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
    public class A17:Gamescene
    {


        public A17(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a17";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 13);
            newSolid(1, 0, 39, 1);
            newSolid(4, 12, 36, 1);
            newSolid(39, 4, 1, 8);

            //Segment 1
            newSolid(8, 11, 1, 1);
            newSolid(21, 11, 3, 1);
            newSolid(34, 11, 1, 1);

            //Segment 2
            newSolid(1, 8, 35, 1);
            newSolid(6, 7, 1, 1);
            newSolid(19, 7, 3, 1);
            newSolid(33, 7, 1, 1);

            //Segment 3
            newSolid(5, 4, 34, 1);
            newSolid(8, 3, 1, 1);
            newSolid(20, 3, 3, 1);
            newSolid(35, 3, 1, 1);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(40, -1, 1, 20, "A18", new Vector2(2, 1));
            newSceneChanger(0, 13, 20, 1, "A16", new Vector2(8, 3));
            //Segment 1
            newProwler(14, 10);
            newProwler(27, 10);

            //Segment 2
            newProwler(26, 6);
            newCharger(12, 6);

            //Segment 3
            newProwler(14, 3);
            newCharger(27, 3);


        }

        
    }
}

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
    public class A20:Gamescene
    {


        public A20(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a20";
        }

        public override void Load()
        {
            //solids to collide with
            newSolid(0, 0, 1, 8);
            newSolid(1, 7, 4, 1);
            newSolid(1, 0, 89, 1);
            newSolid(0, 10, 60, 2);
            newSolid(63, 10, 27, 2);
            newSolid(89, 1, 1, 7);

            newSolid(14, 8, 2, 2);
            newSolid(14, 1, 2, 3);

            //Segment 1
            newSolid(20, 6, 3, 1);
            newSolid(28, 6, 3, 1);
            newSolid(36, 6, 3, 1);

            newSolid(42, 8, 2, 2);
            newSolid(42, 1, 2, 3);

            //Segment 2
            newSolid(56, 8, 2, 2);
            newSolid(56, 1, 2, 3);
            newSolid(65, 8, 2, 2);
            newSolid(65, 1, 2, 3);

            //Segment 3
            newSolid(70, 6, 9, 1);

            newSolid(82, 8, 2, 2);
            newSolid(82, 1, 2, 3);

            newSeal(60, 10, "Green");
            newSeal(61, 10, "Green");
            newSeal(62, 10, "Green");

            base.Load();
            background = con.Load<Texture2D>("backgrounds/Icymoutain_bk");
            at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 1, 20, "A22", new Vector2(18, 9));
            newSceneChanger(90, -1, 1, 20, "A21", new Vector2(2, 9));
            newSceneChanger(5, 12, 85, 20, "A19", new Vector2(9, 2));

            newSentry(7, 1);

            //Segment 1
            newSentry(16, 1);
            newSentry(24, 1);
            newSentry(32, 1);
            newSentry(40, 1);

            newProwler(24, 8);

            //Segment 2
            newSentry(49, 1);

            //Segment 3
            newSentry(67, 1);
            newSentry(80, 1);
            newCharger(73, 8);

        }

        
    }
}

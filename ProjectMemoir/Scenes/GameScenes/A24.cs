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
    public class A24:Gamescene
    {


        public A24(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a24";
        }

        public override void Load()
        {
            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 22);
            newSolid(1, 0, 7, 1);
            newSolid(12, 0, 8, 1);
            newSolid(0, 24, 15, 1);
            newSolid(19, 1, 1, 24);

            newSolid(1, 1, 7, 4);
            newSolid(8, 4, 7, 1);
            newBreakableBlock(15, 4);
            newBreakableBlock(16, 4);
            newBreakableBlock(17, 4);
            newBreakableBlock(18, 4);

            newSolid(1, 9, 2, 1);
            newSolid(7, 9, 12, 1);
            newBreakableBlock(3, 9);
            newBreakableBlock(4, 9);
            newBreakableBlock(5, 9);
            newBreakableBlock(6, 9);

            newSolid(1, 14, 14, 1);
            newBreakableBlock(15, 14);
            newBreakableBlock(16, 14);
            newBreakableBlock(17, 14);
            newBreakableBlock(18, 14);

            newSolid(5, 19, 14, 1);
            newBreakableBlock(1, 19);
            newBreakableBlock(2, 19);
            newBreakableBlock(3, 19);
            newBreakableBlock(4, 19);

            newBreakableBlock(15, 24);
            newBreakableBlock(16, 24);
            newBreakableBlock(17, 24);
            newBreakableBlock(18, 24);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 20, 1, "A3", new Vector2(9, 7));
            newSceneChanger(-1, -1, 1, 30, "A26", new Vector2(38, 9));
            newSceneChanger(0, 25, 20, 1, "A25", new Vector2(2, 2));

            newProwler(11, 8);
            newProwler(12, 23);
            newSentry(10, 10);
            newSentry(9, 15);
        }

        
    }
}

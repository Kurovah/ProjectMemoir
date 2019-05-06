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

            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 8, 1);
            newSolid(12, 11, 8, 1);
            newSolid(19, 1, 1, 8);

            newSolid(6, 9, 2, 2);
            newSolid(12, 9, 2, 2);
            newBreakableBlock(8, 9);
            newBreakableBlock(9, 9);
            newBreakableBlock(10, 9);
            newBreakableBlock(11, 9);
            newBreakableBlock(8, 10);
            newBreakableBlock(9, 10);
            newBreakableBlock(10, 10);
            newBreakableBlock(11, 10);
            newBreakableBlock(8, 11);
            newBreakableBlock(9, 11);
            newBreakableBlock(10, 11);
            newBreakableBlock(11, 11);


            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 0, 12, "A2", new Vector2(1, 9));
            newSceneChanger(-1, -1, 0, 12, "A4", new Vector2(18, 9));
            newSceneChanger(6, 12, 8, 1, "A24", new Vector2(9, 2));
        }

        
    }
}

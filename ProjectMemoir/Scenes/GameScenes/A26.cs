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

            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 12);
            newSolid(1, 0, 39, 1);
            newSolid(4, 11, 36, 1);
            newSolid(39, 1, 1, 8);

            newSolid(34, 4, 1, 7);
            newSolid(28, 1, 1, 7);
            newSolid(22, 5, 1, 6);
            newSolid(20, 5, 2, 3);

            newSolid(14, 1, 1, 6);
            newSolid(4, 4, 3, 7);
            newBreakableBlock(1, 4);
            newBreakableBlock(2, 4);
            newBreakableBlock(3, 4);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(41, -1, 1, 20, "A24", new Vector2(2, 22));
            newSceneChanger(-1, 13, 20, 1, "A27", new Vector2(2, 9));
            newCharger(23, 9);
            newCharger(7, 9);
            newSentry(20, 9);

        }

        
    }
}

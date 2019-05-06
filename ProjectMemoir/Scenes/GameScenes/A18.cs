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
    public class A18:Gamescene
    {


        public A18(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a18";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 3, 1, 8);
            newSolid(0, 0, 20, 1);
            newSolid(0, 11, 8, 1);
            newSolid(12, 11, 8, 1);
            newSolid(19, 1, 1, 10);
            newSolid(1, 3, 14, 1);
            newBreakableBlock(8, 11);
            newBreakableBlock(9, 11);
            newBreakableBlock(10, 11);
            newBreakableBlock(11, 11);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 1, 20, "A17", new Vector2(39, 3));
            newSceneChanger(0, 12, 20, 1, "A11", new Vector2(8, 2));
            newGriefTree(3, 6, "4");
        }

        
    }
}

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
    public class A07:Gamescene
    {


        public A07(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a7";
        }

        public override void Load()
        {
            soundManager.currentState = "hell";
            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 19);
            newSolid(1, 0, 19, 1);

            newSolid(14, 4, 5, 1);
            newSolid(7, 9, 6, 1);
            newSolid(1, 15, 6, 2);
            newSolid(13, 15, 6, 2);
            newSeal(7, 15, "Blue");
            newSeal(8, 15, "Blue");
            newSeal(9, 15, "Blue");
            newSeal(10, 15, "Blue");
            newSeal(11, 15, "Blue");
            newSeal(12, 15, "Blue");

            newSolid(19, 4, 1, 15);
            newSolid(0, 21, 20, 1);
            newSeal(0, 19, "Blue");

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 0, 33, "A2", new Vector2(18, 9));
            newSceneChanger(21, -1, 0, 10, "A19", new Vector2(1, 19));
            newSceneChanger(21, 10, 0, 10, "A8", new Vector2(1, 12));
        }

        
    }
}

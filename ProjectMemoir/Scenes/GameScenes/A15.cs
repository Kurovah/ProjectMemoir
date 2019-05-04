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
    public class A15:Gamescene
    {


        public A15(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a15";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 12);
            newSolid(1, 0, 7, 1);
            newSolid(12, 0, 8, 1);
            newSolid(3, 11, 16, 1);
            newSolid(19, 1, 1, 11);

            newSolid(6, 4, 1, 7);
            newSolid(7, 4, 8, 1);
            newSolid(15, 4, 1, 5);
            newSeal(8, -1, "Blue");
            newSeal(9, -1, "Blue");
            newSeal(10, -1, "Blue");
            newSeal(11, -1, "Blue");

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 20, 1, "A8", new Vector2(4, 9));
            newSceneChanger(0, 13, 20, 1, "A14", new Vector2(2, 2));
            newPedestal(8, 9, "Blue");
        }

        
    }
}

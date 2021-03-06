﻿using System;
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
    public class A14:Gamescene
    {


        public A14(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a14";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 11);
            newSolid(4, 0, 16, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 1, 1, 8);

            newSolid(1, 4, 6, 1);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 12, 1, "A15", new Vector2(3, 9));
            newSceneChanger(21, -1, 1, 12, "A13", new Vector2(1, 12));
            newPedestal(8, 9, "Up");
        }

        
    }
}

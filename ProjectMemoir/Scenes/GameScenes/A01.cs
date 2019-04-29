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
    public class A01:Gamescene
    {


        public A01(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a1";
        }

        public override void Load()
        {
            //the background
            background = con.Load<Texture2D>("backgrounds/VillageBK");

            //solids to collide with
            newSolid(0,0,1,11);
            newSolid(1, 0, 5, 1);
            newSolid(14,0,6,1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            newSolid(1, 7, 4, 1);
            newSolid(15, 7, 4, 1);
            newSolid(7, 4, 6, 1);
            newSolid(7, 10, 6, 1);

            base.Load();
            //you can change the current tileset like this
            //at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 20, 1, "A2", new Vector2(1,9));
        }

        
    }
}

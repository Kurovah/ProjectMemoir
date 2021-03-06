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
    public class A10:Gamescene
    {


        public A10(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a10";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 3, 1, 8);

            newSolid(1, 8, 1, 1);
            newSolid(4, 5, 3, 1);
            newSolid(14, 5, 5, 1);
            newSolid(17, 3, 2, 2);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 0, 20, "A9", new Vector2(28, 9));
            newSceneChanger(21, -1, 0, 20, "A11", new Vector2(1, 2));
            newPedestal(16, 9, "Side");
        }

        
    }
}

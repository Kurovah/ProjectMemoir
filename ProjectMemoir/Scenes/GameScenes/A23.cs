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
    public class A23:Gamescene
    {


        public A23(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a23";
        }

        public override void Load()
        {
            soundManager.currentState = "icy";
            //solids to collide with
            newSolid(0, 0, 4, 31);
            newSolid(4, 0, 4, 1);
            newSolid(12, 0, 8, 1);
            newSolid(0, 31, 8, 1);
            newSolid(12, 31, 8, 1);
            newSolid(16, 1, 4, 30);

            newSolid(7, 4, 6, 1);
            newSolid(4, 8, 4, 1);
            newSolid(12, 8, 4, 1);
            newBreakableBlock(8, 8);
            newBreakableBlock(9, 8);
            newBreakableBlock(10, 8);
            newBreakableBlock(11, 8);

            newSolid(8, 16, 8, 1);
            newBreakableBlock(4, 16);
            newBreakableBlock(5, 16);
            newBreakableBlock(6, 16);
            newBreakableBlock(7, 16);

            newSolid(4, 24, 8, 1);
            newBreakableBlock(12, 24);
            newBreakableBlock(13, 24);
            newBreakableBlock(14, 24);
            newBreakableBlock(15, 24);

            newBreakableBlock(8, 31);
            newBreakableBlock(9, 31);
            newBreakableBlock(10, 31);
            newBreakableBlock(11, 31);


            base.Load();
            background = con.Load<Texture2D>("backgrounds/Icymoutain_bk");
            at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(0, 32, 20, 1, "A2", new Vector2(8, 2));
            newSceneChanger(0, -1, 20, 1, "A22", new Vector2(8, 9));
            newSentry(4, 9);
            newSentry(14, 17);
            newSentry(4, 29);
            newSentry(4, 25);
        }

        
    }
}

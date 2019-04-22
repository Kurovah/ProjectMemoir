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
    public class Test1:Gamescene
    {
        //private Charger cha;
        //private Sentry sen;
        private Prowler pro;
       

        PauseMenu pmenu;
        Autotiler at;

        public Test1(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            
        }

        public override void Load()
        {
            

            //solids to collide with
            newSolid(0,0,1,11);
            newSolid(0, 0, 19, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 0, 1, 12);
            

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(20, 0, 1, 23, "s");
        }

        
    }
}

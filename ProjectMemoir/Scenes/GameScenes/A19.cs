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
    public class A19:Gamescene
    {


        public A19(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a19";
        }

        public override void Load()
        {

            soundManager.currentState = "icy";
            background = con.Load<Texture2D>("backgrounds/Icymoutain_bk");
            //solids to collide with
            newSolid(0, 0, 1, 19);
            newSolid(1, 0, 7, 1);
            newSolid(12, 0, 8, 1);
            newSolid(0, 21, 19, 1);
            newSolid(19, 1, 1, 21);
            newSeal(0, 19, "Green");

            newSolid(7, 18, 6, 1);
            newSolid(1, 11, 4, 1);
            newSolid(15, 11, 4, 1);
            newSolid(7, 4, 6, 1);


            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSentry(9, 10);
            newSceneChanger(-1, -1, 1, 30, "A7", new Vector2(18, 2));
            newSceneChanger(0, -1, 20, 1, "A20", new Vector2(58, 8));
        }

        
    }
}

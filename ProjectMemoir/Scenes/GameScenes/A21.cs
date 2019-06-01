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
    public class A21:Gamescene
    {


        public A21(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a21";
        }

        public override void Load()
        {

            if (soundManager.currentState != "icy") { soundManager.currentState = "icy"; }
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            newSolid(15, 8, 1, 3);
            newBreakableBlock(16, 8);
            newBreakableBlock(17, 8);
            newBreakableBlock(18, 8);

            base.Load();
            background = con.Load<Texture2D>("backgrounds/Icymoutain_bk");
            at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 1, 20, "A20", new Vector2(87, 8));
            newGriefTree(7, 6, "3");
            newPedestal(17, 9, "Green");
        }

        
    }
}

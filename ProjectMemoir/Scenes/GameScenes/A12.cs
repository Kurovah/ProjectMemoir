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
    public class A12:Gamescene
    {


        public A12(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a12";
        }

        public override void Load()
        {
            if (soundManager.currentState != "hell") { soundManager.currentState = "hell"; }
            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(4, 0, 16, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            newSolid(1, 6, 5, 1);
            newSolid(14, 6, 5, 1);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 1, 20, "A13", new Vector2(38, 9));
            newGriefTree(14, 1, "2");
        }

        
    }
}

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
    public class A08:Gamescene
    {


        public A08(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a8";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 6, 1, 6);
            newSolid(0, 0, 20, 6);
            newSolid(0, 14, 20, 1);
            newSolid(19, 6, 1, 6);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 0, 20, "A7", new Vector2(18, 19));
            newSceneChanger(21, -1, 0, 20, "A9", new Vector2(1, 9));
            //newSceneChanger(0, -1, 1, 12, "A15", new Vector2(1, 9));
            newGriefTree(7, 9, "1");
        }

        
    }
}

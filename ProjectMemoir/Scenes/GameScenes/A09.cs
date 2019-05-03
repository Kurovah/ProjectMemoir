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
    public class A09:Gamescene
    {


        public A09(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a9";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 26, 1);
            newSolid(0, 11, 30, 1);
            newSolid(29, 0, 1, 9);

            newSolid(1, 8, 1, 1);
            newSolid(6, 5, 1, 1);
            newSolid(11, 5, 1, 1);
            newSolid(16, 5, 1, 1);
            newSolid(21, 5, 1, 1);
            newSolid(26, 5, 3, 1);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, -1, 0, 20, "A8", new Vector2(18, 12));
            newSceneChanger(31, -1, 0, 20, "A10", new Vector2(1, 9));
            newSceneChanger(24, -1, 4, 1, "A10", new Vector2(1, 9));
            newCharger(15, 9);
        }

        
    }
}

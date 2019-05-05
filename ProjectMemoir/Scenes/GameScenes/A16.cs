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
    public class A16:Gamescene
    {


        public A16(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a16";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 0, 4, 11);
            newSolid(4, 0, 4, 1);
            newSolid(12, 0, 8, 1);
            newSolid(0, 11, 8, 1);
            newSolid(12, 11, 8, 1);
            newSolid(16, 1, 4, 10);

            newSolid(7, 5, 6, 1);

            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(0, 12, 20, 1, "A9", new Vector2(26, 2));
            newSceneChanger(0, -1, 20, 1, "A17", new Vector2(4, 9));
        }

        
    }
}

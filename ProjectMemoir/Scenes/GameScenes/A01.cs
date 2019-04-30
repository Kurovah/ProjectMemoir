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
            newSolid(0, 0, 1, 11);
            newSolid(1, 0, 7, 1);
            newSolid(12, 0, 8, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            newSolid(17, 7, 2, 2);
            newSolid(15, 9, 4, 2);
            newSolid(8, 5, 4, 1);

            base.Load();
            //you can change the current tileset like this
            //at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(7, -1, 7, 1, "A2", new Vector2(9,9));
        }

        
    }
}

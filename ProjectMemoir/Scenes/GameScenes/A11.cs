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
    public class A11:Gamescene
    {


        public A11(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a11";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/DesVillageBK");
            //solids to collide with
            newSolid(0, 4, 1, 18);
            newSolid(0, 0, 20, 1);
            newSolid(4, 21, 16, 1);
            newSolid(19, 1, 1, 20);

            newSolid(1, 4, 15, 1);
            newSolid(4, 8, 15, 1);
            newSolid(1, 13, 15, 1);


            base.Load();
            at.tex = con.Load<Texture2D>("tilesets/hellscape");
            //add anything that uses the player as a target after this
            newSceneChanger(-1, 22, 20, 1, "A12", new Vector2(1, 2));
            newSceneChanger(-1, -1, 1, 12, "A10", new Vector2(18, 1));
            newCharger(6, 19);
            newProwler(7, 7);
            newProwler(11, 12);
        }

        
    }
}

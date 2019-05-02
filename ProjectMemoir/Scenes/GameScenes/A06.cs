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
    public class A06:Gamescene
    {


        public A06(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a6";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 11);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 1, 1, 8);
           
            newSolid(1, 4, 3, 1);
            newSolid(6, 6, 3, 1);
            newSolid(11, 8, 3, 1);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 0, 12, "A5", new Vector2(1, 9));
            newPedestal(2, 2, "Red");
        }

        
    }
}

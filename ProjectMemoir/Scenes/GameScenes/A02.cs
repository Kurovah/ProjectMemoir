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
    public class A02:Gamescene
    {


        public A02(Game1 _game, ContentManager _con,Vector2 _playerpos) :base(_game, _con, _playerpos)
        {
            id = "a2";
        }

        public override void Load()
        {

            
            //solids to collide with
            newSolid(0, 0, 1, 9);
            newSolid(1, 0, 5, 1);
            newSolid(14, 0, 6, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 1, 1, 8);
            newSeal(19, 9, "Red");

            base.Load();
            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 0, 12, "A7", new Vector2(1, 9));
            newSceneChanger(-1, -1, 0, 12, "A3", new Vector2(18, 9));
            //newPedestal(7, 9, "Side");
            //newPedestal(2, 9, "Red");
        }

        
    }
}

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
    public class A22:Gamescene
    {


        public A22(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a22";
        }

        public override void Load()
        {

            
            //solids to collide with
            newSolid(0,0,1,11);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 9, 1);
            newSolid(11, 11, 9, 1);
            newSolid(19, 1, 1, 8);

            newSolid(1, 8, 4, 1);
            newSolid(15, 8, 4, 1);
            newSolid(7, 5, 6, 1);
            newBreakableBlock(9, 11);
            newBreakableBlock(10, 11);

            base.Load();
            background = con.Load<Texture2D>("backgrounds/Icymoutain_bk");
            at.tex = con.Load<Texture2D>("tilesets/Icetileset");
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 1, 20, "A20", new Vector2(1, 8));
            newSceneChanger(0, 12, 20, 1, "A23", new Vector2(8, 2));
            newPedestal(10, 3, "Down");

        }

        
    }
}

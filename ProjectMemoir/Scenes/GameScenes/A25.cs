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
    public class A25:Gamescene
    {


        public A25(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a25";
        }

        public override void Load()
        {

            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0, 0, 1, 11);
            newSolid(4, 0, 16, 1);
            newSolid(0, 11, 20, 1);
            newSolid(19, 4, 1, 7);

            newSolid(1, 4, 6, 7);
            newSolid(13, 4, 6, 7);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(21, -1, 1, 20, "A1", new Vector2(9, 9));
            newSceneChanger(-1, -1, 20, 1, "A24", new Vector2(17, 22));
            newGriefTree(8, 6, "5");
        }

        
    }
}

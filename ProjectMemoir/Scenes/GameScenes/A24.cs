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
    public class A24:Gamescene
    {


        public A24(Game1 _game, ContentManager _con,Vector2 _playerpos):base(_game, _con, _playerpos)
        {
            id = "a24";
        }

        public override void Load()
        {
            background = con.Load<Texture2D>("backgrounds/VillageBK");
            //solids to collide with
            newSolid(0,0,1,11);
            newSolid(1, 0, 19, 1);
            newSolid(0, 11, 19, 1);
            newSolid(19, 1, 1, 11);

            base.Load();
            //add anything that uses the player as a target after this
            newSceneChanger(0, -1, 1, 12, "A26", new Vector2(2, 9));
            newSceneChanger(0, -1, 1, 12, "A25", new Vector2(2, 9));
        }

        
    }
}

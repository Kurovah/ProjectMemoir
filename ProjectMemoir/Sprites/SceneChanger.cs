using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites
{
    class SceneChanger:Sprite
    {
        Player target;
        Game1 game;
        String sceneTo;
        Vector2 playerpos;
        ContentManager con;
        public SceneChanger(ContentManager _con, Vector2 _pos, Player _target, Game1 _game, String _sceneId, Vector2 _newPos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), new Vector2(32,60), new Vector2(32), _pos, 0, Color.Black);
            target = _target;
            game = _game;
            sceneTo = _sceneId;
            playerpos = _newPos;
            con = _con;
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (target.anim.desRect.Intersects(anim.desRect))
            {
                game.currentScene = new Test1(this.game, this.con, playerpos);
            }
        }
    }
}

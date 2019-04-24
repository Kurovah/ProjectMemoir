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
        public SceneChanger(ContentManager _con, Vector2 _pos, Vector2 _size, Player _target, Game1 _game, String _sceneId ,Vector2 _newPos):base(_con, _pos)
        {
            anim = new Animation(_con.Load<Texture2D>("forP"), _size, new Vector2(32), _pos, 0, Color.Black);
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
                switch (sceneTo) {
                    #region all the rooms are listed here (this is also rather unefficient)
                    case "A1":
                    game.nextScene = new A01(this.game, this.con, playerpos);
                        break;
                    case "A2":
                        game.nextScene = new A02(this.game, this.con, playerpos);
                        break;
                    case "A3":
                        game.nextScene = new A03(this.game, this.con, playerpos);
                        break;
                    case "A4":
                        game.nextScene = new A04(this.game, this.con, playerpos);
                        break;
                    case "A5":
                        game.nextScene = new A05(this.game, this.con, playerpos);
                        break;
                    case "A6":
                        game.nextScene = new A06(this.game, this.con, playerpos);
                        break;
                    case "A7":
                        game.nextScene = new A07(this.game, this.con, playerpos);
                        break;

                    case "A8":
                        game.nextScene = new A08(this.game, this.con, playerpos);
                        break;
                    case "A9":
                        game.nextScene = new A09(this.game, this.con, playerpos);
                        break;
                    case "A10":
                        game.nextScene = new A10(this.game, this.con, playerpos);
                        break;
                    case "A11":
                        game.nextScene = new A11(this.game, this.con, playerpos);
                        break;
                    case "A12":
                        game.nextScene = new A12(this.game, this.con, playerpos);
                        break;
                    case "A13":
                        game.nextScene = new A13(this.game, this.con, playerpos);
                        break;
                    case "A14":
                        game.nextScene = new A14(this.game, this.con, playerpos);
                        break;

                    case "A15":
                        game.nextScene = new A15(this.game, this.con, playerpos);
                        break;
                    case "A16":
                        game.nextScene = new A16(this.game, this.con, playerpos);
                        break;
                    case "A17":
                        game.nextScene = new A17(this.game, this.con, playerpos);
                        break;
                    case "A18":
                        game.nextScene = new A18(this.game, this.con, playerpos);
                        break;
                    case "A19":
                        game.nextScene = new A19(this.game, this.con, playerpos);
                        break;
                    case "A20":
                        game.nextScene = new A20(this.game, this.con, playerpos);
                        break;
                    case "A21":
                        game.nextScene = new A21(this.game, this.con, playerpos);
                        break;

                    case "A22":
                        game.nextScene = new A22(this.game, this.con, playerpos);
                        break;
                    case "A23":
                        game.nextScene = new A23(this.game, this.con, playerpos);
                        break;
                    case "A24":
                        game.nextScene = new A24(this.game, this.con, playerpos);
                        break;
                    case "A25":
                        game.nextScene = new A25(this.game, this.con, playerpos);
                        break;
                    case "A26":
                        game.nextScene = new A26(this.game, this.con, playerpos);
                        break;
                    case "A27":
                        game.nextScene = new A27(this.game, this.con, playerpos);
                        break;
                    case "A28":
                        game.nextScene = new A28(this.game, this.con, playerpos);
                        break;
                        #endregion

                }
            }
        }
    }
}

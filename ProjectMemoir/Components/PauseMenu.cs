using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Components
{
    public class PauseMenu:Menu
    {
        List<Vector2> mapSects;
        PlayerStats ps;
        Texture2D tex;
        int sectSize = 48;
        public PauseMenu(ContentManager _con, List<string> _options, Vector2 _pos, Game1 _g, Scene _scene) :base(_con, _options,_pos,_g, _scene)
        {
            ps = _g.ps;
            tex = _con.Load<Texture2D>("ForP");
            active = false;
            #region list of vectors for drawing the map sector
            mapSects = new List<Vector2>(){
                new Vector2(_pos.X+5 + sectSize*4,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*4,_pos.Y+5 +sectSize*3),
                new Vector2(_pos.X+5 + sectSize*3,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*2,_pos.Y+5 +sectSize*3),
                new Vector2(_pos.X+5 + sectSize*1,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*0,_pos.Y+5 +sectSize*3),
                new Vector2(_pos.X+5 + sectSize*5,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*6,_pos.Y+5 +sectSize*3),
                new Vector2(_pos.X+5 + sectSize*7,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*8,_pos.Y+5 +sectSize*3),
                new Vector2(_pos.X+5 + sectSize*9,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*9,_pos.Y+5 +sectSize*5),
                new Vector2(_pos.X+5 + sectSize*7,_pos.Y+5 +sectSize*5),
                new Vector2(_pos.X+5 + sectSize*6,_pos.Y+5 +sectSize*5),
                new Vector2(_pos.X+5 + sectSize*6,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*7,_pos.Y+5 +sectSize*2),
                new Vector2(_pos.X+5 + sectSize*7,_pos.Y+5 +sectSize*1),
                new Vector2(_pos.X+5 + sectSize*8,_pos.Y+5 +sectSize*1),
                new Vector2(_pos.X+5 + sectSize*6,_pos.Y+5 +sectSize*2),
                new Vector2(_pos.X+5 + sectSize*6,_pos.Y+5 +sectSize*0),
                new Vector2(_pos.X+5 + sectSize*7,_pos.Y+5 +sectSize*0),
                new Vector2(_pos.X+5 + sectSize*4,_pos.Y+5 +sectSize*0),
                new Vector2(_pos.X+5 + sectSize*4,_pos.Y+5 +sectSize*2),
                new Vector2(_pos.X+5 + sectSize*3,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*3,_pos.Y+5 +sectSize*5),
                new Vector2(_pos.X+5 + sectSize*3,_pos.Y+5 +sectSize*4),
                new Vector2(_pos.X+5 + sectSize*2,_pos.Y+5 +sectSize*5),
            };
            #endregion
        }
        public override void Update(GameTime _gt)
        {
            currentK = Keyboard.GetState();
            if (currentK.IsKeyDown(Keys.P) && !lastK.IsKeyDown(Keys.P)) { scene.pause = !scene.pause; active = !active; }//if P is "pressed" pause the game 
            lastK = currentK;
            base.Update(_gt);
        }
        public override void Selectoption(int OP)
        {
            if(OP == 0)
            {
                scene.pause = false;
            }
        }

        public override void Draw(SpriteBatch _sb)
        {
            //drawing the map
            for( int i = 0; i < ps.mapPeices.Count; i++)
            {
                //draw the map section if the corresponding section is true
                if (ps.mapPeices[ps.mapPeices.Keys.ElementAt(i)])
                {
                    _sb.Draw(tex, mapSects[i], new Rectangle(32, 32, 32, 32), Color.White);
                }
            }
            base.Draw(_sb);
        }
    }
}

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
        Texture2D tex,tex2,tex3;
        int sectSize = 48, scale = 2;
        public PauseMenu(ContentManager _con, List<string> _options, Vector2 _pos,Scene _scene) :base(_con, _options,_pos, _scene)
        {
            ps = scene.game.ps;
            tex = _con.Load<Texture2D>("pause_items");
            tex2 = _con.Load<Texture2D>("collect");
            tex3 = _con.Load<Texture2D>("forP");
            active = false;
            offset = new Vector2(432*2,10);
            #region list of vectors for drawing the map sector
            mapSects = new List<Vector2>(){
                new Vector2(_pos.X+20 + sectSize*4,_pos.Y+20 +sectSize*4),    //room 1
                new Vector2(_pos.X+20 + sectSize*4,_pos.Y+20 +sectSize*3),    //room 2
                new Vector2(_pos.X+20 + sectSize*3,_pos.Y+20 +sectSize*3),    //room 3
                new Vector2(_pos.X+20 + sectSize*2,_pos.Y+20 +sectSize*3),    //room 4
                new Vector2(_pos.X+20 + sectSize*1,_pos.Y+20 +sectSize*3),    //room 5
                new Vector2(_pos.X+20 + sectSize*0,_pos.Y+20 +sectSize*3),    //room 6
                new Vector2(_pos.X+20 + sectSize*5,_pos.Y+20 +sectSize*3),    //room 7
                new Vector2(_pos.X+20 + sectSize*6,_pos.Y+20 +sectSize*3),    //room 8
                new Vector2(_pos.X+20 + sectSize*7,_pos.Y+20 +sectSize*3),    //room 9
                new Vector2(_pos.X+20 + sectSize*8,_pos.Y+20 +sectSize*3),    //room 10
                new Vector2(_pos.X+20 + sectSize*9,_pos.Y+20 +sectSize*4),    //room 11
                new Vector2(_pos.X+20 + sectSize*9,_pos.Y+20 +sectSize*10),    //room 12
                new Vector2(_pos.X+20 + sectSize*7,_pos.Y+20 +sectSize*10),    //room 13
                new Vector2(_pos.X+20 + sectSize*6,_pos.Y+20 +sectSize*10),    //room 14
                new Vector2(_pos.X+20 + sectSize*6,_pos.Y+20 +sectSize*4),    //room 15
                new Vector2(_pos.X+20 + sectSize*7,_pos.Y+20 +sectSize*2),    //room 16
                new Vector2(_pos.X+20 + sectSize*7,_pos.Y+20 +sectSize*1),    //room 17
                new Vector2(_pos.X+20 + sectSize*8,_pos.Y+20 +sectSize*1),    //room 18
                new Vector2(_pos.X+20 + sectSize*6,_pos.Y+20 +sectSize*2),    //room 19
                new Vector2(_pos.X+20 + sectSize*6,_pos.Y+20 +sectSize*0),    //room 20
                new Vector2(_pos.X+20 + sectSize*7,_pos.Y+20 +sectSize*0),    //room 21
                new Vector2(_pos.X+20 + sectSize*4,_pos.Y+20 +sectSize*0),    //room 22
                new Vector2(_pos.X+20 + sectSize*4,_pos.Y+20 +sectSize*2),    //room 23
                new Vector2(_pos.X+20 + sectSize*3,_pos.Y+20 +sectSize*4),    //room 24
                new Vector2(_pos.X+20 + sectSize*3,_pos.Y+20 +sectSize*10),    //room 25
                new Vector2(_pos.X+20 + sectSize*3,_pos.Y+20 +sectSize*4),    //room 26
                new Vector2(_pos.X+20 + sectSize*2,_pos.Y+20 +sectSize*10),    //room 28
            };
            #endregion
        }
        public override void Update(GameTime _gt)
        {
            currentK = Keyboard.GetState();
            if (currentK.IsKeyDown(Keys.P) && !lastK.IsKeyDown(Keys.P)) { scene.pause = !scene.pause; active = !active; }//if P is "pressed" pause the game 
            pointer.position = offset + new Vector2(64, 10 + pos * 40);
            base.Update(_gt);
            lastK = currentK;
        }
        public override void Selectoption(int OP)
        {
            switch (OP) {
                case 0:
                scene.pause = false;
                    break;
                case 1:
                    scene.game.nextScene = new MainMenu(scene.game, scene.game.Content);
                    break;
            }
            
        }

        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
            //draw the menu
            int P = 0;
            foreach (String _s in options)
            {
                _sb.DrawString(txt, _s, startPos + new Vector2(960, 15+P * 40), Color.White);
                P++;
            }
            _sb.Draw(tex3,new Rectangle(20,20,864,480),new Rectangle(0,0,32,32),Color.Black *0.75f);
            _sb.Draw(tex3, new Rectangle(20, 536, 864, 128), new Rectangle(0, 0, 32, 32), Color.Black * 0.75f);
            _sb.Draw(tex3, new Rectangle(914, 100, 346, 564), new Rectangle(0, 0, 32, 32), Color.Black * 0.75f);
            //drawing the map
            for ( int i = 0; i < ps.mapPeices.Count; i++)
            {
                //draw the map section if the corresponding section is true
                if (ps.mapPeices[ps.mapPeices.Keys.ElementAt(i)])
                {
                    _sb.Draw(tex,mapSects[i]*2, new Rectangle(0, 0, 32, 32), Color.White);
                }
                if(ps.mapPeices.Keys.ElementAt(i) == scene.id)
                {
                    _sb.Draw(tex, mapSects[i] * scale, new Rectangle(32, 0, 32, 32), Color.White);
                }
            }

            //draw abilities in boxes
            for (int i = 0; i < ps.abilities.Count; i++)
            {
                _sb.Draw(tex, new Rectangle(90 + 108 * i, 568,64,64), new Rectangle(128, 0, 32, 32), Color.White);
                if (ps.abilities[ps.abilities.Keys.ElementAt(i)])
                {
                    _sb.Draw(tex2, new Rectangle(90 + 108 * i, 568, 64, 64), new Rectangle(32+32*i, 0, 32, 32), Color.White);
                }
            }

            //draw the purified trees
            for (int i = 0; i < ps.treesPurified.Count; i++)
            {
                _sb.Draw(tex, new Rectangle(904 + 356/2 -32 , 130+110 *i, 64, 64), new Rectangle(64, 0, 32, 32), Color.White);
                if (ps.treesPurified[ps.treesPurified.Keys.ElementAt(i)])
                {
                    _sb.Draw(tex2, new Rectangle(904 + 356 - 32, 110 + 96 * i, 64, 64), new Rectangle(96, 0, 32, 32), Color.White);
                }
            }
       
        }
    }
}

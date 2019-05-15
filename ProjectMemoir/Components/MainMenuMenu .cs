using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Components
{
    public class MainMenuMenu:Menu
    {
        PlayerStats ps;
        Scene s;
        Texture2D tex;
        bool instructionsShowing;
        public MainMenuMenu(ContentManager _con, List<string> _options, Vector2 _pos, Scene _scene) :base(_con, _options,_pos, _scene)
        {
            ps = scene.game.ps;
            s = _scene;
            offset = new Vector2((1280 / 2) - (txt.MeasureString(options[pos]).X / 2),720/2 + 10);
            tex = _con.Load<Texture2D>("instructions");
            instructionsShowing = false;
        }
        public override void Update(GameTime _gt)
        {
            if (!instructionsShowing)
            {
                active = true;
                pointer.position = offset + new Vector2(-32, pos * 30);
                base.Update(_gt);
                s.soundManager.Update(_gt);
            }
            else
            {
                if (input.JumpInput)
                {
                    instructionsShowing = false;
                }
            }
            
        }
        public override void Selectoption(int OP)
        {
            switch(OP)
            {
                case 0:
                    ps.Reset();
                    scene.game.nextScene = new A01(scene.game, scene.game.Content, new Vector2(2, 9));
                    s.soundManager.currentState = "plains";
                    break;
                case 1:
                    instructionsShowing = true;
                    break;
                case 2:
                    Process.Start("https://w1629904.wixsite.com/mysite");
                    break;
                case 3:
                    scene.game.Exit();
                    break;
                
            }
        }

        public override void Draw(SpriteBatch _sb)
        {
            base.Draw(_sb);
            int P = 0;
            foreach (String _s in options)
            {
                _sb.DrawString(txt, _s, startPos + new Vector2((1280/2) - (txt.MeasureString(_s).X/2), (720/2)+P * 30), Color.White);
                P++;
            }
            if (instructionsShowing)
            {
                _sb.Draw(tex,new Vector2(20),Color.White);
            }
        }
    }
}

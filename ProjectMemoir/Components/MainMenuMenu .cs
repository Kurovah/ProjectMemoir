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
        public MainMenuMenu(ContentManager _con, List<string> _options, Vector2 _pos, Scene _scene) :base(_con, _options,_pos, _scene)
        {
            ps = scene.game.ps;
            s = _scene;
            offset = new Vector2((1280 / 2) - (txt.MeasureString(options[pos]).X / 2),720/2 + 10);
        }
        public override void Update(GameTime _gt)
        {
            active = true;
            currentK = Keyboard.GetState();
            if (currentK.IsKeyDown(Keys.P) && !lastK.IsKeyDown(Keys.P)) { scene.pause = !scene.pause; active = !active; }//if P is "pressed" pause the game 
            pointer.position = offset + new Vector2(-32, pos * 30);
            base.Update(_gt);
            s.soundManager.Update(_gt);
            lastK = currentK;
        }
        public override void Selectoption(int OP)
        {
            switch(OP)
            {
                case 0:
                    ps.Reset();
                    scene.game.nextScene = new A01(scene.game, scene.game.Content, new Vector2(2, 9));
                    s.soundManager.currentState = SoundManger.Gamestate.plains;
                    break;
                case 1:
                    scene.game.Exit();
                    break;
                case 2:
                    Process.Start("https://w1629904.wixsite.com/mysite");
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
        }
    }
}

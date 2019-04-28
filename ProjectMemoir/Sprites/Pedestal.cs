using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMemoir.Components;
using ProjectMemoir.Scenes;

namespace ProjectMemoir.Sprites
{
    public class Pedestal:Sprite
    {
        String type;
        PlayerStats ps;
        Player player;
        Animation obj;
        Gamescene parentScene;
        double a;
        Vector2 position;
        public Pedestal(ContentManager _con, Vector2 _pos, PlayerStats _ps, String _type, Player _target, Gamescene _parentScene) : base(_con, _pos)
        {
            a = 0;
            type = _type;
            player = _target;
            ps = _ps;
            parentScene = _parentScene;
            position = _pos;
            obj = new Animation(_con.Load<Texture2D>("collect"), new Vector2(32), new Vector2(32), position, 0, Color.White);
            anim = new Animation(_con.Load<Texture2D>("collect"), new Vector2(32), new Vector2(32), position+new Vector2(0,32), 0, Color.White);
            obj.needsChange = anim.needsChange = false;

            //obj sprites
            switch (type)
            {
                case"Red":
                    obj.sourcePos.X = 32*6;
                    break;
                case "Green":
                    obj.sourcePos.X = 32 * 5;
                    break;
                case "Blue":
                    obj.sourcePos.X = 32 * 7;
                    break;
                case "Neutral":
                    obj.sourcePos.X = 32 * 1+1;
                    break;
                case "Up":
                    obj.sourcePos.X = 32 * 2;
                    break;
                case "Down":
                    obj.sourcePos.X = 32 * 3;
                    break;
                case "Side":
                    obj.sourcePos.X = 32 * 4;
                    break;
            }
            

            if (ps.abilities[type])
            {
                obj.alpha = 0;
            }
        }

        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            //making the obj "float"
            if(a < 720)
            {
                a+=0.1;
            } else
            {
                a = 0;
            }
            obj.position.Y = position.Y + 2*(float)Math.Sin(a);

            if (player.anim.desRect.Intersects(anim.desRect))
            {
                if (!ps.abilities[type])
                {
                    ps.abilities[type] = true;
                    obj.alpha = 0;
                    parentScene.pu.active = true;
                    switch (type) {
                        case "Up":
                        parentScene.pu.text = "You have obtained the secret technique: 'Flash Flip' (▲ + K)";
                            break;
                        case "Down":
                        parentScene.pu.text = "you have obtained the secret technique: 'Gaia Crash' (▼ + K)";
                            break;
                        case "Side":
                        parentScene.pu.text = "you have obtained the secret technique: 'Flow Rush' (►/◄ + K)";
                            break;
                        case "Neutral":
                            parentScene.pu.text = "you have obtained 'kunai' (K) ";
                            break;
                        case "Red":
                            parentScene.pu.text = "You have obtained the Red seal breaker, Red seals no longer impede your progress";
                            break;
                        case "Green":
                            parentScene.pu.text = "You have obtained the Green seal breaker, Green seals no longer impede your progress";
                            break;
                        case "Blue":
                            parentScene.pu.text = "You have obtained the Blue seal breaker, Blue seals no longer impede your progress";
                            break;

                    }
                }
                
            }

            
            obj.Update(_gt);
            base.Update(_gt, _sl);
        }

        public override void Draw(SpriteBatch _sb)
        {
            obj.Draw(_sb);
            base.Draw(_sb);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using ProjectMemoir.Scenes;
using ProjectMemoir.Components;

namespace ProjectMemoir.Sprites
{
    public class EndDoor:Sprite
    {
        Gamescene parentScene;
        PlayerStats ps;
        Player target;
        bool active = false;
        EndDoor(ContentManager _con, Vector2 _pos, Gamescene _parentScene) : base(_con,_pos,_parentScene)
        {
            parentScene = _parentScene;
            ps = _parentScene.ps;
            target = _parentScene.player;
            anim = new Animation(_con.Load<Texture2D>("end_door"),new Vector2(128),new Vector2(128),_pos,18,Color.White);
        }
        public override void Update(GameTime _gt, List<Sprite> _sl)
        {
            if (anim.desRect.Intersects(target.anim.desRect) && target.currentKS.IsKeyDown(Keys.K))
            {
                if (allSeedsCollected())
                {
                    active = true;
                } else
                {
                    parentScene.pu.active = true;
                    parentScene.pu.text = "you haven't fully cleansed yourself of grief";
                }
            }
            if (active)
            {
                base.Update(_gt, _sl);
                if (anim.isFinished())
                {
                    parentScene.game.nextScene = new Endscene(parentScene.game, parentScene);
                }
            }
        }
        private bool allSeedsCollected()
        {
            bool ret = true;
            for(int i = 0; i > ps.treesPurified.Count; i++)
            {
                if (!ps.treesPurified[ps.treesPurified.Keys.ElementAt(i)])
                {
                    ret = false;
                }
            }
            return ret;
        }
    }
}

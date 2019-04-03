using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectMemoir.Scenes
{
    public abstract class Scene
    {
        protected Game1 game;
        protected ContentManager con;
        protected Scene(Game1 _game, ContentManager _con)
        {
            con = _con;
            game = _game;
        }

        public abstract void Load();
        public abstract void Update(GameTime _gt);
        public abstract void Draw(SpriteBatch _sb, GameTime _gt);
        
    }
}

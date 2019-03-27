using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMemoir
{
    public class Player
    {
        private Texture2D _texture;
        public Vector2 _position;
        public Vector2 _velocity;
        bool hasJumped;


        public Player(Texture2D texture, Vector2 newPosition)
        {
            _texture = texture;
            _position = newPosition;
            hasJumped = true;
        }

        public void Update(GameTime _gameTime)
        {
            _position += _velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _velocity.X = 5f;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                _velocity.X = -5f;
            else
                _velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                _position.Y -= 12f; //jump height
                _velocity.Y = -8f; //jump speed
                hasJumped = true;
            }

            if(hasJumped == true)
            {
                float i = 1;
                _velocity.Y += 0.25f * i;   
            }

            if (_position.Y + _texture.Height >= 500)
                hasJumped = false;

            if (hasJumped == false)
                _velocity.Y = 0f;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ProjectMemoir
{
    public class GameObject
    {
        protected Texture2D iamge;
        public Vector2 position;
        public Color drawColor;
        public float slcae = 1f, rotation = 0f;
        public bool active = true;
        protected Vector2 center;

        public virtual void Initialize()
        {

        }

        public GameObject()
        {

        }

        public virtual void Load(ContentManager content)
        {

        }
    }
}

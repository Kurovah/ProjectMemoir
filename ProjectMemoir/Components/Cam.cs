using System;
using Microsoft.Xna.Framework;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Components
{
    public class Cam
    {
        public Sprite target;
        public Matrix trans;
        public Vector2 targetPos, currentPos, camMax,screenSize;
        float zoom = 2f;
        public Cam(Sprite _target, Vector2 _camMax, Vector2 _camSize)
        {
            target = _target;
            targetPos = new Vector2(target.anim.position.X , target.anim.position.Y );
            currentPos = new Vector2(target.anim.position.X , target.anim.position.Y );
            screenSize = _camSize;
            //note that the cam is at the BOTTOM LEFT of the screen so the min is room size + screen width and the max size is the room width
            camMax = _camMax;
        }
        private float Lerp(float x1, float x2, float i)
        {
            return x1*(1-i) + x2*i;
        }
        public void Update(GameTime gt)
        {
            //changing the target position
            targetPos = new Vector2(target.anim.position.X + target.anim.spriteSize.X/2, target.anim.position.Y + target.anim.spriteSize.Y / 2) ;
            double dis = Math.Sqrt((Math.Pow(Math.Abs(targetPos.X - currentPos.X), 2) + 
                Math.Pow(Math.Abs(targetPos.Y - currentPos.Y),2)));

            //moving the current position the match the player's position
            if (dis >= 2) { currentPos = new Vector2(Lerp(currentPos.X, targetPos.X, 0.1f), Lerp(currentPos.Y, targetPos.Y, 0.1f)) ; }

            //setting the camera bounds
            currentPos.X = MathHelper.Clamp(currentPos.X , screenSize.X / (2*zoom), camMax.X - screenSize.X / (2 * zoom));
            currentPos.Y = MathHelper.Clamp(currentPos.Y , screenSize.Y / (2*zoom), camMax.Y - screenSize.Y/(2*zoom));
            //new
            trans = Matrix.CreateTranslation(new Vector3(-currentPos.X, -currentPos.Y, 0)) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 0))*
                Matrix.CreateTranslation(new Vector3(screenSize.X/2, screenSize.Y/2, 0));
                

           
        }
    }
}

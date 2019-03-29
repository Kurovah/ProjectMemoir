using System;
using Microsoft.Xna.Framework;
using ProjectMemoir.Sprites;

namespace ProjectMemoir.Components
{
    public class Cam
    {
        public Sprite target;
        public Matrix trans;
        public Vector2 targetPos, currentPos, camBoundsH, camBoundsV;
        public Cam(Sprite _target, Vector2 _camBoundsH, Vector2 _camBoundsV)
        {
            target = _target;
            targetPos = new Vector2(target.anim.position.X, target.anim.position.Y);
            currentPos = new Vector2(target.anim.position.X, target.anim.position.Y);
            camBoundsH = _camBoundsH;
            camBoundsV = _camBoundsV;
        }
        private float Lerp(float x1, float x2, float i)
        {
            return x1*(1-i) + x2*i;
        }
        public void Update(GameTime gt)
        {
            //changing the target position
            targetPos = new Vector2(target.anim.position.X, target.anim.position.Y);
            double dis = Math.Sqrt((Math.Pow(Math.Abs(targetPos.X - currentPos.X), 2) + Math.Pow(Math.Abs(targetPos.Y - currentPos.Y),2)));
            //moving the current position the match the player's position
            if (dis >= 5) { currentPos = new Vector2(Lerp(currentPos.X, targetPos.X, 0.1f), Lerp(currentPos.Y, targetPos.Y, 0.1f)); }
            //setting the camera bounds
            currentPos.X = MathHelper.Clamp(currentPos.X, camBoundsH.X ,camBoundsH.Y);
            currentPos.Y = MathHelper.Clamp(currentPos.Y, camBoundsV.X, camBoundsV.Y);
            var position = Matrix.CreateTranslation(
        -currentPos.X - (target.anim.spriteSize.X / 2),
        -currentPos.Y - (target.anim.spriteSize.Y / 2),
        0);

            var offset = Matrix.CreateTranslation(
                320,
                180,
                0);

            trans = position * offset;
        }
    }
}

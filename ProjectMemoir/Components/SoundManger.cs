using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace ProjectMemoir.Components
{
   public  class SoundManger
    {
        public SoundEffect ChargerDash,kunaiToss,kunaiClink,playerJump, playerRun, playerGetHurt, playerFlashFlip, playerAquaDash, playerCrush;
        public Song mainMenu, gameOver, village, hellScape, icyMountain;
        public SoundManger(ContentManager _con)
        {
            playerJump = _con.Load<SoundEffect>("sounds/Jump");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace ProjectMemoir.Components
{
   public  class SoundManger
    {
        public enum Gamestate
        {
            mainmenu,
            gameoverscreen,
            icy,
            hell,
            plains,
            itemget,
            credits,
            none
        }
        public SoundEffect ChargerDash,kunaiToss,kunaiClink,playerJump, playerRun, playerGetHurt, playerFlashFlip, playerAquaDash, playerCrush;
        public Song mainMenu, gameOver, village, hellScape, icyMountain, itemGet;
        public Gamestate currentState, nextState;
        private bool musicPlaying;
        public SoundManger(ContentManager _con)
        {
            playerJump = _con.Load<SoundEffect>("sounds/Jump");
            itemGet = _con.Load<Song>("Music/music_itemget");
            currentState = nextState = Gamestate.none;
            musicPlaying = true;
        }

        public void Update(GameTime _gt)
        {
            switch (currentState)
            {
                case Gamestate.itemget:
                    if(musicPlaying && nextState != Gamestate.none)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                        currentState = nextState;
                        nextState = Gamestate.none;

                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Play(itemGet);
                        musicPlaying = true;
                        //r
                    }
                    break;
                case Gamestate.none:
                    if (musicPlaying && nextState != Gamestate.none)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                        currentState = nextState;
                        nextState = Gamestate.none;

                    }
                    break;
            }
            
        }
    }
}

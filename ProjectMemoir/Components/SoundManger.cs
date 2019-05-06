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
        public SoundEffect ChargerDash, kunaiToss, kunaiClink, playerJump, playerRun, playerGetHurt, playerFlashFlip, playerAquaDash, playerCrush, mainMenuSelect;
        public Song mainMenu, gameOver, village, hellScape, icyMountain, itemGet;
        public Gamestate currentState, nextState;
        private bool musicPlaying;
        public SoundManger(ContentManager _con)
        {
            playerJump = _con.Load<SoundEffect>("sounds/Jump");
            itemGet = _con.Load<Song>("Music/music_itemget");
            playerCrush = _con.Load<SoundEffect>("sounds/DownSmash");
            playerGetHurt = _con.Load<SoundEffect>("sounds/Hurt");
            playerRun = _con.Load<SoundEffect>("sounds/PlayerWalk");
            mainMenuSelect = _con.Load<SoundEffect>("sounds/Mainmenu");
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
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(itemGet);
                        musicPlaying = true;
                        //r
                    }
                    break;
                case Gamestate.none:
                    //MediaPlayer.Volume = 0.5f;
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

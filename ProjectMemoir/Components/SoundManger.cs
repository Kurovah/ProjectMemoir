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
            credits,
            none
        }
        public SoundEffect ChargerDash, kunaiToss, kunaiClink, playerJump, playerRun, playerGetHurt, playerFlashFlip, playerAquaDash, playerCrush, mainMenuSelect, itemGet;
        public Song mainMenu, gameOver, village, hellScape, icyMountain;
        public Gamestate currentState, lastState;
        public bool musicPlaying, gotItem, itemsongplaying;
        SoundEffectInstance Iinstance;
        public SoundManger(ContentManager _con)
        {
            //songs
            gameOver = _con.Load<Song>("Music/Game_over");
            village = _con.Load<Song>("Music/Village");
            hellScape = _con.Load<Song>("Music/Destroyed_Village");
            icyMountain = _con.Load<Song>("Music/Ice_Mountain");
            mainMenu = _con.Load<Song>("Music/Main_Theme");

            //sound effects
            itemGet = _con.Load<SoundEffect>("Music/music_itemget");
            playerJump = _con.Load<SoundEffect>("sounds/Jump");
            kunaiClink = _con.Load<SoundEffect>("sounds/KunaiClink");
            playerGetHurt = _con.Load<SoundEffect>("sounds/Hurt");
            playerRun = _con.Load<SoundEffect>("sounds/PlayerWalk");
            mainMenuSelect = _con.Load<SoundEffect>("sounds/Mainmenu");
            currentState = lastState = Gamestate.none;
            musicPlaying = false;
            gotItem = false;
            itemsongplaying = false;
            Iinstance = itemGet.CreateInstance();
            Iinstance.Volume = 0.1f;

        }

        public void Update(GameTime _gt)
        {
            if(gotItem)
            {
                if (!itemsongplaying) {
                    
                    Iinstance.Play();
                    itemsongplaying = true;
                    MediaPlayer.Pause();
                }

                if(Iinstance.State == SoundState.Stopped)
                {
                    MediaPlayer.Resume();
                    itemsongplaying = false;
                    gotItem = false;
                }
            }

            switch (currentState)
            { 
                #region icy music
                case Gamestate.icy:
                    if (musicPlaying  && lastState != currentState)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(icyMountain);
                        musicPlaying = true;
                    }
                    break;
                #endregion

                #region main menu
                case Gamestate.mainmenu:
                    if (musicPlaying  && lastState != currentState)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(mainMenu);
                        musicPlaying = true;
                    }
                    break;
                #endregion

                #region hell music
                case Gamestate.hell:
                    if (musicPlaying  && lastState != currentState)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(hellScape);
                        musicPlaying = true;
                    }
                    break;
                #endregion

                #region village
                case Gamestate.plains:
                    if (musicPlaying  && lastState != currentState)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(village);
                        musicPlaying = true;
                    }
                    break;
                #endregion

                #region Gameover
                case Gamestate.gameoverscreen:
                    if (musicPlaying  && lastState != currentState)
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                    }
                    if (!musicPlaying)
                    {
                        MediaPlayer.Volume = 0.1f;
                        MediaPlayer.Play(gameOver);
                        musicPlaying = true;
                    }
                    break;
                #endregion
                case Gamestate.none:
                    //MediaPlayer.Volume = 0.5f;
                    if (musicPlaying )
                    {
                        MediaPlayer.Stop();
                        musicPlaying = false;
                        lastState = Gamestate.none;

                    }
                    break;
            }
            lastState = currentState;
            
        }
    }
}

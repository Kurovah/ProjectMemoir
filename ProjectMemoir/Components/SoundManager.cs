using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace ProjectMemoir.Components
{
   public  class SoundManager
    {
        
        public SoundEffect ChargerDash, kunaiToss, kunaiClink, playerJump, playerRun, playerGetHurt, playerFlashFlip, playerAquaDash, playerCrush, BrickCrush,
                           mainMenuSelect, itemGet, griefTree, sentryShot, chaseState, stunState;
        public Song mainMenu, gameOver, village, hellScape, icyMountain;
        public String currentState, lastState;
        public bool musicPlaying, gotItem, itemsongplaying;
        SoundEffectInstance Iinstance;
        public SoundManager(ContentManager _con)
        {
            //songs
            gameOver = _con.Load<Song>("Music/Game_over");
            village = _con.Load<Song>("Music/Village");
            hellScape = _con.Load<Song>("Music/Destroyed_Village");
            icyMountain = _con.Load<Song>("Music/Ice_Mountain");
            mainMenu = _con.Load<Song>("Music/Main_Theme");
            itemGet = _con.Load<SoundEffect>("Music/music_itemget");

            //sound effects
            playerJump = _con.Load<SoundEffect>("sounds/Jump");
            playerFlashFlip = _con.Load<SoundEffect>("sounds/FlashFlip");
            playerCrush = _con.Load<SoundEffect>("sounds/DownSmash");
            BrickCrush = _con.Load<SoundEffect>("sounds/BrickBreak");
            playerGetHurt = _con.Load<SoundEffect>("sounds/Hurt");
            playerRun = _con.Load<SoundEffect>("sounds/PlayerWalk");
            playerAquaDash = _con.Load<SoundEffect>("sounds/AquaDash");           
            kunaiClink = _con.Load<SoundEffect>("sounds/KunaiClink");
            kunaiToss = _con.Load<SoundEffect>("sounds/KunaiToss");
            mainMenuSelect = _con.Load<SoundEffect>("sounds/Mainmenu");
            griefTree = _con.Load<SoundEffect>("sounds/GriefTree");
            sentryShot = _con.Load<SoundEffect>("sounds/SentryShot");
            chaseState = _con.Load<SoundEffect>("sounds/ChaseState");
            stunState = _con.Load<SoundEffect>("sounds/StunState");
            ChargerDash = _con.Load<SoundEffect>("sounds/ChargeStart");
            currentState = lastState = "none";
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
                case "icy":
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
                case "mainmenu":
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
                case "hell":
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
                case "plains":
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
                case "gameover":
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
            }

            lastState = currentState;
        }
    }
}

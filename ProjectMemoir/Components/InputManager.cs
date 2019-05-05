using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectMemoir.Components
{
    public class InputManager
    {
        GamePadState currentGPState, lastGPState;
        KeyboardState currentKeyState, lastKeyState;
        public bool LeftInput, RightInput, UpInput, DownInput, JumpInput, ActionInput, PauseInput, 
            LeftInputH, RightInputH, UpInputH, DownInputH;
        public InputManager()
        {
            LeftInput = RightInput = UpInput = DownInput = JumpInput = ActionInput= PauseInput = LeftInputH = RightInputH = UpInputH = DownInputH = false;
            currentGPState = lastGPState = GamePad.GetState(PlayerIndex.One);
        }
        public void Update()
        {
            if (currentGPState.IsConnected)
            {
                currentGPState = GamePad.GetState(PlayerIndex.One);

                DownInput = (currentGPState.DPad.Down == ButtonState.Pressed && lastGPState.DPad.Down != ButtonState.Pressed) ||
                    (currentGPState.ThumbSticks.Left.Y < -0.25f && !(lastGPState.ThumbSticks.Left.Y < -0.25f));
                DownInputH = (currentGPState.DPad.Down == ButtonState.Pressed || currentGPState.ThumbSticks.Left.Y < -0.25f);

                UpInput = ((currentGPState.DPad.Up == ButtonState.Pressed && lastGPState.DPad.Up != ButtonState.Pressed) ||
                    currentGPState.ThumbSticks.Left.Y > 0.25f && !(lastGPState.ThumbSticks.Left.Y > 0.25f));
                UpInputH = (currentGPState.DPad.Up == ButtonState.Pressed || currentGPState.ThumbSticks.Left.Y > 0.25f);

                LeftInput = ((currentGPState.DPad.Left == ButtonState.Pressed && lastGPState.DPad.Left != ButtonState.Pressed) ||
                    currentGPState.ThumbSticks.Left.X < -0.25f && !(lastGPState.ThumbSticks.Left.X < -0.25f));
                LeftInputH = (currentGPState.DPad.Left == ButtonState.Pressed || currentGPState.ThumbSticks.Left.X < -0.25f);

                RightInput = ((currentGPState.DPad.Right == ButtonState.Pressed && lastGPState.DPad.Right != ButtonState.Pressed) ||
                    currentGPState.ThumbSticks.Left.X > 0.25f && !(lastGPState.ThumbSticks.Left.X > 0.25f));
                RightInputH = (currentGPState.DPad.Right == ButtonState.Pressed || currentGPState.ThumbSticks.Left.X > 0.25f);

                ActionInput = (currentGPState.Buttons.X == ButtonState.Pressed && lastGPState.Buttons.X != ButtonState.Pressed);
                JumpInput = (currentGPState.Buttons.A == ButtonState.Pressed && lastGPState.Buttons.A != ButtonState.Pressed);
                PauseInput = (currentGPState.Buttons.Start == ButtonState.Pressed && lastGPState.Buttons.Start != ButtonState.Pressed);

                lastGPState = currentGPState;
            }
            else
            {

                #region currentkey press
                currentKeyState = Keyboard.GetState();
                DownInput = currentKeyState.IsKeyDown(Keys.S) && !lastKeyState.IsKeyDown(Keys.S);
                DownInputH = currentKeyState.IsKeyDown(Keys.S);

                UpInput = currentKeyState.IsKeyDown(Keys.W) && !lastKeyState.IsKeyDown(Keys.W);
                UpInputH = currentKeyState.IsKeyDown(Keys.W);

                LeftInput = currentKeyState.IsKeyDown(Keys.A) && !lastKeyState.IsKeyDown(Keys.A);
                LeftInputH = currentKeyState.IsKeyDown(Keys.A);

                RightInput = currentKeyState.IsKeyDown(Keys.D) && !lastKeyState.IsKeyDown(Keys.D);
                RightInputH = currentKeyState.IsKeyDown(Keys.D);

                ActionInput = currentKeyState.IsKeyDown(Keys.K) && !lastKeyState.IsKeyDown(Keys.K);
                JumpInput = currentKeyState.IsKeyDown(Keys.J) && !lastKeyState.IsKeyDown(Keys.J);
                PauseInput = currentKeyState.IsKeyDown(Keys.P) && !lastKeyState.IsKeyDown(Keys.P);

                lastKeyState = currentKeyState;
                #endregion
            }
        }
    }
}

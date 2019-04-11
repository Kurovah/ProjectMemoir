using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMemoir
{
    public static class Input 
    {
        private static KeyboardState keyboardState = Keyboard.GetState();
        private static KeyboardState lastKeyboardState;

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

    
        // Checks if key is currently pressed.
     
        public static bool IsKeyDown(Keys input)
        {
            return keyboardState.IsKeyDown(input);
        }

       
        /// Checks if key is currently up.
        
        public static bool IsKeyUp(Keys input)
        {
            return keyboardState.IsKeyUp(input);
        }

       
        /// Checks if key was just pressed.
       
        public static bool KeyPressed(Keys input)
        {
            if (keyboardState.IsKeyDown(input) == true && lastKeyboardState.IsKeyDown(input) == false)
                return true;
            else
                return false;
        }
    }
}

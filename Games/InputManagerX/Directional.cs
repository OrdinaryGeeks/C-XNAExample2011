using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework.Input;

namespace SmellOfRevenge2011
{
   
    static class Directional
    {
        // Helper bit masks for directions defined with the Buttons flags enum.
        public const Buttons None = 0;
        public const Buttons Up =  Buttons.RightThumbstickUp;
        public const Buttons Down =  Buttons.RightThumbstickDown;
        public const Buttons Left =  Buttons.RightThumbstickLeft;
        public const Buttons Right =  Buttons.RightThumbstickRight;
        public const Buttons UpLeft = Up | Left;
        public const Buttons UpRight = Up | Right;
        public const Buttons DownLeft = Down | Left;
        public const Buttons DownRight = Down | Right;
        public const Buttons Any = Up | Down | Left | Right;



        public static Buttons FromInput(GamePadState gamePad, KeyboardState keyboard)
        {
            Buttons direction = None;

            // Get vertical direction.
            if (gamePad.IsButtonDown(Buttons.DPadUp) ||
                gamePad.IsButtonDown(Buttons.RightThumbstickUp) ||
                keyboard.IsKeyDown(Keys.Up))
            {
                direction |= Up;
            }
            else if (gamePad.IsButtonDown(Buttons.DPadDown) ||
                gamePad.IsButtonDown(Buttons.RightThumbstickDown) ||
                keyboard.IsKeyDown(Keys.Down))
            {
                direction |= Down;
            }

            // Comebine with horizontal direction.
            if (gamePad.IsButtonDown(Buttons.DPadLeft) ||
                gamePad.IsButtonDown(Buttons.RightThumbstickLeft) ||
                keyboard.IsKeyDown(Keys.Left))
            {
                direction |= Left;
            }
            else if (gamePad.IsButtonDown(Buttons.DPadRight) ||
                gamePad.IsButtonDown(Buttons.RightThumbstickRight) ||
                keyboard.IsKeyDown(Keys.Right))
            {
                direction |= Right;
            }

            return direction;
        }

                        /// <summary>
        /// Gets the direction without non-direction buttons from a set of Buttons flags.
        /// </summary>
        public static Buttons FromButtons(Buttons buttons)
        {
            // Extract the direction from a full set of buttons using a bit mask.
            return buttons & Any;
        }
    }
}
    


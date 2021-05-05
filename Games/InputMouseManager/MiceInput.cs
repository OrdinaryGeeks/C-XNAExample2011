using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace SmellOfRevenge2011
{

    public enum MiceEnum
    {
        LeftClick,
        RightClick,
        None
    }

 



    static class MiceInput{
           //Helper bit masks for Keys defined with the Keys flags enum
        public const Keys LeftClick = Keys.OemComma;
        public const Keys RightClick = Keys.OemPeriod;
        public const Keys A = Keys.A;
        public const Keys S = Keys.S;
        public const Keys D = Keys.D;
        public const Keys F = Keys.F;

        public const Keys Any = LeftClick | RightClick | A | S | D | F;
        public static Keys FromInput(KeyboardState keyboard, MouseState mouse)
        {
            Keys keysAndMice = 0;
            if (keyboard.IsKeyDown(Keys.OemComma) || mouse.LeftButton == ButtonState.Pressed)
                keysAndMice |= LeftClick;

            if (keyboard.IsKeyDown(Keys.OemPeriod) || mouse.RightButton == ButtonState.Pressed)
                keysAndMice |= RightClick;

            if (keyboard.IsKeyDown(Keys.A))
                keysAndMice |= A;

            if (keyboard.IsKeyDown(Keys.S))
                keysAndMice |= S;

            if (keyboard.IsKeyDown(Keys.D))
                keysAndMice |= D;

            if (keyboard.IsKeyDown(Keys.F))
                keysAndMice |= F;

            return keysAndMice;

        }

        public static Keys FromKeys(Keys keys)
        {

            return keys & Any;

        }

}




}
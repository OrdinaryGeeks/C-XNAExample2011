using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SmellOfRevenge2011
{
    public class MoveMouse
    {

        public string Name;

        // The sequence of button presses required to activate this move.
        public Keys[] Sequence;

        // Set this to true if the input used to activate this move may
        // be reused as a component of longer moves.
        public bool IsSubMove;

        
        //Params lets you have variable arguments
        public MoveMouse(string name, params Keys[] sequence)
        {
            Name = name;
            Sequence = sequence;
        }


    }
}

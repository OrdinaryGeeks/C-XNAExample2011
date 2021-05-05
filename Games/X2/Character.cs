using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmellOfRevenge2011
{
    public class Character
    {
        protected int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
            }
        }
        protected int team;
        public int Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        protected int target;
        public int Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }
        /// <summary>
        /// Guard, Chased, TemporaryTrap 
        /// </summary>
        protected int state;
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public Character()
        {
            index = 0;
            team = 0;
            target = 0;
            state = 0;



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SmellOfRevenge2011
{
    public class InputManagerMouse
 {
        public PlayerIndex PlayerIndex { get; private set; }

        public GamePadState GamePadState { get; private set; }
        public KeyboardState KeyboardState { get; private set; }
        public MouseState MouseState { get; private set; }
        public MouseState oldMouseState { get; private set; }
        /// <summary>
        /// The last "real time" that new input was received. Slightly late button
        /// presses will not update this time; they are merged with previous input.
        /// </summary>
        public TimeSpan LastInputTime { get; private set; }

        /// <summary>
        /// The current sequence of pressed buttons.
        /// </summary>
        public List<Buttons> Buffer;

        //Left click is a comma right click is a period
        public List<Keys> Input;
        
        /// <summary>
        /// SEPTEMBER CHANGE FROM 500 TO 1000
        /// NSIIII was 500
        /// This is how long to wait for input before all input data is expired.
        /// This prevents the player from performing half of a move, waiting, then
        /// performing the rest of the move after they forgot about the first half.
        /// </summary>
        public readonly TimeSpan BufferTimeOut = TimeSpan.FromMilliseconds(1000);

        
        /// <summary>
        /// SEPTEMBER CHANGE FROM 100 to 900
        /// NSIII was 300
        /// This is the size of the "merge window" for combining button presses that
        /// occur at almsot the same time.
        /// If it is too small, players will find it difficult to perform moves which
        /// require pressing several buttons simultaneously.
        /// If it is too large, players will find it difficult to perform moves which
        /// require pressing several buttons in sequence.
        /// </summary>
        public readonly TimeSpan MergeInputTime = TimeSpan.FromMilliseconds(500);

        internal static readonly Dictionary<Keys, MiceEnum> MiceToKeys =
            new Dictionary<Keys, MiceEnum>
            {
                {Keys.OemComma, MiceEnum.LeftClick},
                {Keys.OemPeriod, MiceEnum.RightClick},
                {Keys.A, MiceEnum.None},
                {Keys.S, MiceEnum.None},
                {Keys.D, MiceEnum.None},
                {Keys.F, MiceEnum.None}
            };

        /// <summary>
        /// Provides the map of non-direction game pad buttons to keyboard keys.
        /// </summary>
        internal static readonly Dictionary<Buttons, Keys> NonDirectionButtons =
            new Dictionary<Buttons, Keys>
            {
                { Buttons.A, Keys.A },
                { Buttons.B, Keys.B },
                { Buttons.X, Keys.X },
                { Buttons.Y, Keys.Y },
                { Buttons.LeftTrigger, Keys.LeftControl},
                {Buttons.RightTrigger, Keys.RightControl},
                {Buttons.RightShoulder, Keys.RightAlt},
                {Buttons.LeftShoulder, Keys.LeftAlt}

                // Other available non-direction buttons:
                // Start, Back, LeftShoulder, LeftTrigger, LeftStick,
                // RightShoulder, RightTrigger, and RightStick.
            };


        public InputManagerMouse(PlayerIndex playerIndex, int bufferSize)
        {
            PlayerIndex = playerIndex;
            Buffer = new List<Buttons>(bufferSize);
            Input = new List<Keys>(bufferSize);
        }
        public bool isMousePresssed(MiceEnum myEnum)
        {
            if (myEnum == MiceEnum.LeftClick &&( MouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                return true;
            else if (myEnum == MiceEnum.RightClick && (MouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released))
                return true;
            else if (myEnum == MiceEnum.None)
                return false;
            else
                return false;

        }
        /// <summary>
        /// Gets the latest input and uses it to update the input history buffer.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Get latest input state.
           // GamePadState lastGamePadState = GamePadState;
            KeyboardState lastKeyboardState = KeyboardState;
            oldMouseState = MouseState;
           // GamePadState = GamePad.GetState(PlayerIndex);
#if WINDOWS
            if (PlayerIndex == PlayerIndex.One)
            {
                KeyboardState = Keyboard.GetState(PlayerIndex);
                MouseState = Mouse.GetState();
            }
#endif            

            // Expire old input.
            TimeSpan time = gameTime.TotalGameTime;
            TimeSpan timeSinceLast = time - LastInputTime;
            if (timeSinceLast > BufferTimeOut)
            {
            //    Buffer.Clear();
                Input.Clear();
            }

           // Console.WriteLine(timeSinceLast);
            // Get all of the non-direction buttons pressed.
            Keys keys = 0;
            foreach (var keyAndMouse in MiceToKeys)
            {
                Keys key = keyAndMouse.Key;
                MiceEnum mice = keyAndMouse.Value;

                if ((lastKeyboardState.IsKeyUp(key) &&
                    KeyboardState.IsKeyDown(key)) ||
                    (isMousePresssed(mice)))
                {
                    keys |= key;

                }

            }
 

            // It is very hard to press two buttons on exactly the same frame.
            // If they are close enough, consider them pressed at the same time.
            bool mergeInput = (Input.Count > 0 && timeSinceLast < MergeInputTime);
            // If there is a new mice input
            var Mice = MiceInput.FromInput(KeyboardState, MouseState);
            if (MiceInput.FromInput(lastKeyboardState, oldMouseState) != Mice)
            {
                // combine the direction with the buttons.
                keys |= Mice;

                // Don't merge two different directions. This avoids having impossible
                // directions such as Left+Up+Right. This also has the side effect that
                // the direction needs to be pressed at the same time or slightly before
                // the buttons for merging to work.
                mergeInput = false;
            }

            // If there was any new input on this update, add it to the buffer.
            if (keys != 0)
            {
                if (mergeInput)
                {
                    // Use a bitwise-or to merge with the previous input.
                    // LastInputTime isn't updated to prevent extending the merge window.
                    Input[Input.Count - 1] = Input[Input.Count - 1] | keys;                    
                }
                else
                {
                    // Append this input to the buffer, expiring old input if necessary.
                    if (Input.Count == Input.Capacity)
                    {
                        Input.RemoveAt(0);
                    }
                    Input.Add(keys);

                    // Record this the time of this input to begin the merge window.
                    LastInputTime = time;
                }
            }
        }
        public void ZeroOutLastInputTime()
        {
            LastInputTime = TimeSpan.Zero;


        }
        /// <summary>
        /// Determines if a move matches the current input history. Unless the move is
        /// a sub-move, the history is "consumed" to prevent it from matching twice.
        /// </summary>
        /// <returns>True if the move matches the input history.</returns>
        public bool Matches(MoveMouse move)
        {
            // If the move is longer than the buffer, it can't possibly match.
            if (Input.Count < move.Sequence.Length)
                return false;

            // Loop backwards to match against the most recent input.
            for (int i = 1; i <= move.Sequence.Length; ++i)
            {
                if (Input[Input.Count - i] != move.Sequence[move.Sequence.Length - i])
                {
                    return false;
                }
            }

            // Rnless this move is a component of a larger sequence,
            if (!move.IsSubMove)
            {
                // consume the used inputs.
                Buffer.Clear();
            }

            return true;
        }
    }
}

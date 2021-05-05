#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace SmellOfRevenge2011
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Welcome to the RuneMaster")
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            //MenuEntry newGameEntry = new MenuEntry("New Game");
           // MenuEntry loadGameEntry = new MenuEntry("Load Game");
            MenuEntry characterSelectMenuEntry = new MenuEntry("Character Select");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            MenuEntry newCharacter = new MenuEntry("Make a New Character");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;
            newCharacter.Selected += NewCharacterSelected;
            characterSelectMenuEntry.Selected += CharacterSelectMenuSelected;
            

           // newGameEntry.Selected += NewGameEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(newCharacter);
            //MenuEntries.Add(newGameEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(characterSelectMenuEntry);

            MenuEntries.Add(exitMenuEntry);
        }




        #endregion

        #region Handle Input
        void CharacterSelectMenuSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CharacterSelectScreen("Character Selected"), e.PlayerIndex);
        }

        void NewCharacterSelected(object sender, PlayerIndexEventArgs e)
        {
          //  ScreenManager.AddScreen(new NewCharacterScreen(), e.PlayerIndex);
        }
        void NewGameEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            
            //ScreenManager.AddScreen(new GamePlayScreen(0, 0), e.PlayerIndex);

        }

        void LoadGameEntrySelected(object sender, PlayerIndexEventArgs e)
        {



        }
        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
          //  LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                      //         new ());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}

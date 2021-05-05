#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
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
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Paused")
        {
            //GamePlayScreen.paused = true;
            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");
            MenuEntry customizeMenuEntry = new MenuEntry("Customize Buttons");
            MenuEntry inventoryMenuEntry = new MenuEntry("Inventory");
            MenuEntry mapEntry = new MenuEntry("Map");
            MenuEntry runeList = new MenuEntry("RuneList");
            
            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += resumeEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            customizeMenuEntry.Selected += customizeEntrySelected;
            inventoryMenuEntry.Selected += inventoryEntrySelected;
            mapEntry.Selected += mapEntrySelected;
            runeList.Selected += runeListEntrySelected;
            // Add entries to the menu.
            MenuEntries.Add(mapEntry);
            MenuEntries.Add(runeList);
            MenuEntries.Add(customizeMenuEntry);
            MenuEntries.Add(inventoryMenuEntry);
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
            
        }


        #endregion

        #region Handle Input
        void runeListEntrySelected(object sender, PlayerIndexEventArgs e)
        {

           // ScreenManager.AddScreen(new runeListScreen(true), ControllingPlayer);

        }
        void mapEntrySelected(object sender, PlayerIndexEventArgs e)
        {


          //  ScreenManager.AddScreen(new MapScreen(true), ControllingPlayer);



        }
        void resumeEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            ScreenManager.paused = false;
            
          //  GamePlayScreen.paused = false;
            ExitScreen();


        }
        void customizeEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            ScreenManager.AddScreen(new MappingScreen(), ControllingPlayer);



        }
        void inventoryEntrySelected(object sender, PlayerIndexEventArgs e)
        {
         //   ScreenManager.AddScreen(new InventoryScreen(), ControllingPlayer);

        }

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the pause menu screen. This darkens down the gameplay screen
        /// that is underneath us, and then chains to the base MenuScreen.Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            base.Draw(gameTime);
        }


        #endregion
    }
}

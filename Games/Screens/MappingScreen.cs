using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace SmellOfRevenge2011
{
      class MappingScreen :GameScreen{

        #region Fields

        List<MenuEntry> menuEntries = new List<MenuEntry>();
        List<MenuEntry> poolEntries = new List<MenuEntry>();
        int poolSelectedEntry = 0;
        int selectedEntry = 0;
        string menuTitle;
         
        #endregion

        #region Properties


        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        protected int SelectedEntry
        {
            get
            {
                return selectedEntry;
            }
        }
        protected string MenuTitle
        {
            get
            {
                return menuTitle;
            }
        }

        #endregion

         MenuEntry slot1;
         MenuEntry slot2;
         MenuEntry slot3;
         MenuEntry slot4;
         MenuEntry slot5;
         MenuEntry slot6;
         MenuEntry slot7;
         MenuEntry slot8;
         MenuEntry accept;
         MenuEntry cancel;

         MenuEntry downToRight;
         MenuEntry downToLeft;
         MenuEntry leftDownRIght;


         List<string> runes;
         List<string> assignments;


        public MappingScreen() 
    {

       menuTitle = "MappingScreen";

            runes = new List<string>();
            assignments = new List<string>();



            runes.Add("Water 1");
            runes.Add("Fire 1");
            runes.Add("Earth 1");
            runes.Add("Grass 1");
            runes.Add("Wind 1");

            //runes.Add("Embue Weapon Forward Water");
            //runes.Add("Embue Weapon Forward Fire");
            //runes.Add("Embue Weapon Forward 

            //runes.Add("Rock 1");

            assignments.Add("Water 1");
            assignments.Add("Water 1");
            assignments.Add("Water 1");
            assignments.Add("Water 1");
            assignments.Add("");
            assignments.Add("");
            assignments.Add("");
            assignments.Add("");


       

        TransitionOnTime = TimeSpan.FromSeconds(0.5);
        TransitionOffTime = TimeSpan.FromSeconds(0.5);

        slot1 = new MenuEntry("slot1");
        slot2 = new MenuEntry("slot2");
        slot3 = new MenuEntry("slot3");
        slot4 = new MenuEntry("slot4");
        slot5 = new MenuEntry("slot5");
        slot6 = new MenuEntry("slot6");
        slot7 = new MenuEntry("slot7");
        slot8 = new MenuEntry("slot8");
        accept = new MenuEntry("Accept");
        cancel = new MenuEntry("Cancel");


        slot1.Selected += slot1Selected;
        slot2.Selected += slot2Selected;
        slot3.Selected += slot3Selected;
        slot4.Selected += slot4Selected;
        slot5.Selected += slot5Selected;
        slot6.Selected += slot6Selected;
        slot7.Selected += slot7Selected;
        slot8.Selected += slot8Selected;
        accept.Selected += acceptSelected;
        cancel.Selected += cancelSelected;





        MenuEntries.Add(slot1);
        MenuEntries.Add(slot2);
        MenuEntries.Add(slot3);
        MenuEntries.Add(slot4);
        MenuEntries.Add(slot5);
        MenuEntries.Add(slot6);
        MenuEntries.Add(slot7);
        MenuEntries.Add(slot8);
            MenuEntries.Add(accept);
            MenuEntries.Add(cancel);




    }

        public override void HandleInput(InputState input)
        {
            if(selectedEntry < MenuEntries.Count -2)
            assignments[selectedEntry] = runes[poolSelectedEntry];
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            if (input.IsMenuLeft(ControllingPlayer))
            {
                poolSelectedEntry--;

                if (poolSelectedEntry < 0)
                    poolSelectedEntry = runes.Count - 1;


            }
            if (input.IsMenuRight(ControllingPlayer))
            {
                poolSelectedEntry++;

                if (poolSelectedEntry >= runes.Count)
                    poolSelectedEntry = 0;


            }


            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }
        }
        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            menuEntries[selectedEntry].OnSelectEntry(playerIndex);
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        void acceptSelected(object sender, PlayerIndexEventArgs e)
        {
            //GamePlayScreen.player.runeBindings = assignments;
            //GamePlayScreen.paused = true;
            for (int i = 0; i < 8; i++)
                ScreenManager.runes[i] = assignments[i];
            ExitScreen();


        }
        void cancelSelected(object sender, PlayerIndexEventArgs e)
        {
           // GamePlayScreen.paused = true;

            ExitScreen();

        }
        void slot8Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 7;
            // poolSelectedEntry = 0;

            //downS = runes[poolSelectedEntry];




        }
        void slot1Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 0;
           // poolSelectedEntry = 0;

            //downS = runes[poolSelectedEntry];




        }
        void slot2Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 1;

            //upS = runes[poolSelectedEntry];


        }
        void slot3Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 2;




        }
        void slot4Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 3;



        }
        void slot5Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 4;



        }
        void slot6Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 5;



        }
        void slot7Selected(object sender, PlayerIndexEventArgs e)
        {
            selectedEntry = 6;



        }
        #region Update and Draw


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }


        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            Vector2 position = new Vector2(100, 150);
            Vector2 poolPosition = new Vector2(250, 150);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);
                bool isPoolSelected = IsActive && (i == poolSelectedEntry);



                // Draw the selected entry in yellow, otherwise white.
                Color color = isSelected ? Color.Yellow : Color.White;

                if (isSelected)
                    color = isPoolSelected ? Color.Red : Color.Yellow;

                // Pulsate the size of the selected menu entry.
                double time = gameTime.TotalGameTime.TotalSeconds;

                float pulsate = (float)Math.Sin(time * 6) + 1;

                float scale = 0.0f;

                if (isSelected)
                    scale = 1 + pulsate * 0.05f * 1.0f;
                else
                    scale = 1 + pulsate * 0.05f * 0.0f;

                // Modify the alpha to fade text out during transitions.
                color = new Color(color.R, color.G, color.B, TransitionAlpha);

                // Draw text, centered on the middle of each line.
                //ScreenManager screenManager = screen.ScreenManager;
                //SpriteBatch spriteBatch = screenManager.SpriteBatch;
                //SpriteFont font = screenManager.Font;

                Vector2 origin = new Vector2(0, font.LineSpacing / 2);

                spriteBatch.DrawString(font, menuEntry.Text, position, color, 0,
                                       origin, scale, SpriteEffects.None, 0);
                if(menuEntry.Text != "Accept" && menuEntry.Text != "Cancel"
                    && i != 4 && i != 5 && i!= 6)
                         spriteBatch.DrawString(font, assignments[i], position  + new Vector2(350.0f, 0.0f), color, 0,
                                           origin, scale, SpriteEffects.None, 0);
                if(i == 4 || i == 5 || i == 6)
                    spriteBatch.DrawString(font, "For Level 2 Runes", position + new Vector2(350.0f, 0.0f), color, 0,
                                      origin, scale, SpriteEffects.None, 0);
                //menuEntry.Draw(this, position, isSelected, gameTime);
                
                position.Y += menuEntry.GetHeight(this);
            }

            // Draw the menu title.
            Vector2 titlePosition = new Vector2(426, 80);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha);
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }


        #endregion





    }
}
